using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

namespace SecretSanta.API.Firestore
{
    public class GroupRepository : IGroupRepository
    {
        private const string CollectionName = "Groups";
        private readonly FirestoreDb _fireStoreDb;

        public GroupRepository(IOptions<FirestoreConfig> options)
        {
            _fireStoreDb = FirestoreDb.Create(options.Value.ProjectId);
        }

        public async Task<Group> Add(Group record)
        {
            var colRef = _fireStoreDb.Collection(CollectionName);
            var doc = await colRef.AddAsync(record);
            record.Id = doc.Id;
            return record;
        }

        public async Task<bool> Update(Group record)
        {
            var recordRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var result = await recordRef.SetAsync(record, SetOptions.MergeAll);
            return true;
        }

        public async Task<bool> Delete(Group record)
        {
            var recordRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var result = await recordRef.DeleteAsync();
            return true;
        }

        public async Task<Group> Get(Group record)
        {
            var docRef = _fireStoreDb.Collection(CollectionName)
                .Document(record.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var group = snapshot.ConvertTo<Group>();
                group.Id = snapshot.Id;
                return group;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            var query = _fireStoreDb.Collection(CollectionName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<Group>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<Group>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }

        public async Task<IEnumerable<Group>> QueryRecords(Query query)
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<Group>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    var city = documentSnapshot.ToDictionary();
                    var json = JsonConvert.SerializeObject(city);
                    var newItem = JsonConvert.DeserializeObject<Group>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }
    }
}