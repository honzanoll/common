using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using honzanoll.Storage.Models.Settings;
using honzanoll.Storage.NetCore.Abstractions;
using honzanoll.Storage.NetCore.Providers;
using System;

namespace honzanoll.Storage.NetCore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        #region Public methods

        /// <summary>
        /// Register local storage provider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Application configuration</param>
        public static void AddLocalStorage<TStorageType>(this IServiceCollection services, IConfiguration configuration) where TStorageType : Enum
        {
            services.Configure<StorageSettings>(configuration.GetSection(nameof(StorageSettings)));

            services.AddTransient<IStorageProvider<LocalStorage, TStorageType>, LocalStorageProvider<TStorageType>>();
        }

        #endregion
    }
}
