using FirebaseAdmin.Auth;
using GroupUp.API.Domain.Models;

namespace GroupUp.API.Firestore.Utility
{
    public static class UserMapper
    {
        public static User AppendEmailToUsername(User user)
        {
            return new User
            {
                DisplayName = user.DisplayName,
                // Email = user.DisplayName + "@example.com",
                // Password = user.Password
            };
        }
    }
}
