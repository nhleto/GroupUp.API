using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace SecretSanta.API.Models
{
    [FirestoreData]
    public class User : Base
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public IEnumerable<string> WishList { get; set; }
        [FirestoreProperty]
        public IEnumerable<Group> Groups { get; set; }
    }
}