using System;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.API.Models;

namespace SecretSanta.API.Firestore
{
    public static class ServiceConfiguration
    {
        public static void AddFirestore(this IServiceCollection services, FirestoreConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            var baseRepo = new BaseRepository("User", config);
            var userRepo = new UserRepository(baseRepo);
            services.AddSingleton(userRepo);
        }
    }
}