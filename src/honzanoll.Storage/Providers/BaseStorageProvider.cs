using Microsoft.Extensions.Options;
using honzanoll.Storage.Abstractions.Models;
using honzanoll.Storage.Models.Settings;

namespace honzanoll.Storage.Providers
{
    /// <summary>
    /// Base storage provider implementing specific settings
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public abstract class BaseStorageProvider<TType>
        where TType : IStorageProviderSettings
    {
        #region Protected methods

        /// <summary>
        /// Storage provider settings
        /// </summary>
        protected readonly TType storageSettings;

        #endregion

        #region Constructors

        public BaseStorageProvider(IOptions<StorageSettings> storageSettingsOptions)
        {
            storageSettings = (TType)typeof(StorageSettings).GetProperty(typeof(TType).Name).GetValue(storageSettingsOptions.Value);
        }

        #endregion
    }
}
