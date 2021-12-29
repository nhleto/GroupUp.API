using FirebaseAdmin.Auth;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Firestore.Utility
{
    public static class UserMapper
    {
        public static User AppendEmailToUsername(User user)
        {
            return new User
            {
                DisplayName = user.DisplayName,
                Email = user.DisplayName + "@example.com",
                Password = user.Password
            };
        }
    }
}