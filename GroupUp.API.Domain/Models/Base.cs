using Google.Cloud.Firestore;

namespace GroupUp.API.Domain.Models
{
    public class Base
    {
        [FirestoreProperty]
        public string Id { get; set; }
    }
}