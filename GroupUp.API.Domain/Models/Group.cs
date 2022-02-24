using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace GroupUp.API.Domain.Models
{
    [FirestoreData]
    public class Group : Base
    {
        [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public IEnumerable<User> Users { get; set; }
    }
}
