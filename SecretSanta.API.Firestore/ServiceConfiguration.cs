using System;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

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
        }
    }
}