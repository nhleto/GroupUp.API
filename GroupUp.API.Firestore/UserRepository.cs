using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using GroupUp.API.Domain;
using GroupUp.API.Domain.DTO;
using GroupUp.API.Domain.Interfaces;
using GroupUp.API.Domain.Models;
using GroupUp.API.Firestore.Utility;

namespace GroupUp.API.Firestore
{
    public class UserRepository : IUserRepository
    {
        private const string CollectionName = "Users";
        private readonly FirestoreDb _fireStoreDb;
        private readonly IMapper _mapper;

        public UserRepository(FirestoreConfig config, IMapper mapper)
        {
            _mapper = mapper;
            _fireStoreDb = FirestoreDb.Create(config.ProjectId);
        }

        public async Task<UserDto> SignUp(User record)
        {
            try
            {
                // This mapper isn't something that should be in the client...
                var mappedUser = UserMapper.AppendEmailToUsername(record);
                var newUser = await FirebaseAuth.DefaultInstance.CreateUserAsync(_mapper.Map<UserRecordArgs>(mappedUser));
                return _mapper.Map<UserDto>(newUser);
            }
            catch (Exception e)
            {
                throw new Exception("Failure to CreateUserAsync: " + e);
            }
        }
        
        public async Task<bool> Update(User record)
        {
            var recordRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var result = await recordRef.SetAsync(record, SetOptions.MergeAll);
            return true;
        }

        public async Task<bool> Delete(User record)
        {
            var recordRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var result = await recordRef.DeleteAsync();
            return true;
        }

        public async Task<User> Get(User record)
        {
            var docRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var usr = snapshot.ConvertTo<User>();
                usr.Id = snapshot.Id;
                return usr;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var query = _fireStoreDb.Collection(CollectionName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<User>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<User>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }

        public async Task<List<User>> QueryRecords(Query query)
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<User>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<User>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }
    }
}
