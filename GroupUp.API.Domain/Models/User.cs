using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace GroupUp.API.Domain.Models
{
    [FirestoreData]
    public class User : Base
    {
        [FirestoreProperty]
        public string DisplayName { get; set; }
        
        [FirestoreProperty]
        public IEnumerable<string> WishList { get; set; }
        
        [FirestoreProperty]
        public IEnumerable<string> GroupIds { get; set; }
    }
}
