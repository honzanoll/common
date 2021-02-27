using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using honzanoll.Storage.Models.Settings;
using honzanoll.Storage.NetCore.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace honzanoll.Storage.NetCore.Providers
{
    public class LocalStorageProvider<TStorageType> :
        Storage.Providers.LocalStorageProvider<TStorageType>, IStorageProvider<LocalStorage, TStorageType>
        where TStorageType : Enum
    {
        #region Constructors

        public LocalStorageProvider(IOptions<StorageSettings> storageSettingsOptions) : base(storageSettingsOptions) { }

        #endregion

        #region Public methods

        public async Task<Models.File> StoreFileAsync(TStorageType storageType, IFormFile file)
        {
            Models.File fileData = new Models.File();
            fileData.Path = GetFilePath(storageType) + Path.GetExtension(file.FileName).ToLowerInvariant();
            fileData.FileName = file.FileName;

            using (FileStream stream = new FileStream(fileData.Path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            fileData.Checksum = GenerateChecksum(File.ReadAllBytes(fileData.Path));

            return fileData;
        }

        #endregion
    }
}
