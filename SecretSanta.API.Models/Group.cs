using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace SecretSanta.API.Models
{
    [FirestoreData]
    public class Group
    {
        [FirestoreProperty]
        public IEnumerable<User> Users { get; set; }
    }
}