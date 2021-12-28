using FirebaseAdmin.Auth;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Firestore.Utility
{
    public static class UserMapper
    {
        public static UserRecordArgs AppendEmailToUsername(User user)
        {
            return new UserRecordArgs
            {
                DisplayName = user.DisplayName,
                Email = user.DisplayName + "@example.com",
                Password = user.Password
            };
        }
    }
}