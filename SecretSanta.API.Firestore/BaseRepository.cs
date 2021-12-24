using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using SecretSanta.API.Models;

namespace SecretSanta.API.Firestore
{
    public class BaseRepository
    {
        private readonly string _collectionName;
        private readonly FirestoreDb _fireStoreDb;
        
        public BaseRepository(string collectionName)
        {
            var env = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            var config = JsonConvert.DeserializeObject<FirestoreConfig>
                (Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") ?? string.Empty);
            _fireStoreDb = FirestoreDb.Create(config.ProjectId);
            _collectionName = collectionName;
        }

        public async Task<T> Add<T>(T record) where T : Base
        {
            var colRef = _fireStoreDb.Collection(_collectionName);
            var doc = await colRef.AddAsync(record);
            record.Id = doc.Id;
            return record;
        }

        public async Task<bool> Update<T>(T record) where T : Base
        {
            var recordRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            var result = await recordRef.SetAsync(record, SetOptions.MergeAll);
            return true;
        }

        public async Task<bool> Delete<T>(T record) where T : Base
        {
            var recordRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            var result = await recordRef.DeleteAsync();
            return true;
        }

        public async Task<T> Get<T>(T record) where T : Base
        {
            var docRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var usr = snapshot.ConvertTo<T>();
                usr.Id = snapshot.Id;
                return usr;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : Base
        {
            var query = _fireStoreDb.Collection(_collectionName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }

        public async Task<List<T>> QueryRecords<T>(Query query) where T : Base
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }
    }
}