using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

namespace SecretSanta.API.Firestore
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreConfig _config;
        private const string CollectionName = "Users";
        private readonly FirestoreDb _fireStoreDb;

        public UserRepository(IOptions<FirestoreConfig> options, FirestoreConfig config)
        {
            _config = config;
            _fireStoreDb = FirestoreDb.Create(config.ProjectId);
        }

        public async Task<User> Add(User record)
        {
            var userRecord = new UserRecordArgs
            {
                DisplayName = record.DisplayName
            };
            
            var newUser = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecord);
            record.Id = newUser.Uid;
            return record;
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