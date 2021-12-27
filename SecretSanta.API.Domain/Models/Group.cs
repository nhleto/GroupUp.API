using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace SecretSanta.API.Domain.Models
{
    [FirestoreData]
    public class Group : Base
    {
        [FirestoreProperty]
        public IEnumerable<User> Users { get; set; }
    }
}