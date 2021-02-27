using Microsoft.AspNetCore.Http;
using honzanoll.Storage.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace honzanoll.Storage.NetCore.Abstractions
{
    public interface IStorageProvider<TType, TStorageType> : Storage.Abstractions.IStorageProvider<TType, TStorageType>
        where TType : IStorageProviderSettings
        where TStorageType : Enum
    {
        #region Public methods

        Task<Models.File> StoreFileAsync(TStorageType storageType, IFormFile file);

        #endregion
    }
}
