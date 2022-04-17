using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using GroupUp.API.Domain;
using GroupUp.API.Domain.Interfaces;
using GroupUp.API.Domain.Models;

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

        
        public async Task<WriteResult> Update(User record)
        {
            var recordRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var result = await recordRef.SetAsync(record, SetOptions.MergeAll);
            return result;
        }

        // Need to write FirebaseFunction that takes user Id on create an inserts Id into DB
        public async Task BatchUpdate(IEnumerable<User> users, Group group)
        {
            var batch = _fireStoreDb.StartBatch();
            users = users.ToList();
            
            var result = users.Select(x =>
                _fireStoreDb.Collection(CollectionName).Document(x.Id)).ToList();

            var data = result.Select(x => new Dictionary<string, object>
            {
                {
                    "GroupId", group.Id
                }
            });

            var zippedList = result.Zip(data).ToList();

            foreach (var joinedList in zippedList)
            {
                var (first, second) = joinedList;
                batch.Update(first, second);
            }

            await batch.CommitAsync();
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

            return null;
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
