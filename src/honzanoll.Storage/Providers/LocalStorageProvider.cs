using Microsoft.Extensions.Options;
using honzanoll.Storage.Abstractions;
using honzanoll.Storage.Exceptions;
using honzanoll.Storage.Models.Settings;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace honzanoll.Storage.Providers
{
    /// <summary>
    /// Local storage provider - storing localy on application server
    /// </summary>
    public class LocalStorageProvider<TStorageType> :
        BaseStorageProvider<LocalStorage>,
        IStorageProvider<LocalStorage, TStorageType> where TStorageType : Enum
    {
        #region Constructors

        public LocalStorageProvider(IOptions<StorageSettings> storageSettingsOptions) : base(storageSettingsOptions) { }

        #endregion

        #region Public methods

        /// <summary>
        /// Store file
        /// </summary>
        /// <param name="storageType">Type of file to store</param>
        /// <param name="file">File to store</param>
        /// <param name="fileName">File name</param>
        /// <returns>Stored file object</returns>
        public async Task<Models.File> StoreFileAsync(TStorageType storageType, byte[] file, string fileName)
        {
            Models.File fileData = new Models.File();
            fileData.Path = GetFilePath(storageType) + Path.GetExtension(fileName).ToLowerInvariant();
            fileData.FileName = fileName;

            using (FileStream stream = new FileStream(fileData.Path, FileMode.Create))
            {
                await stream.WriteAsync(file);
            }

            fileData.Checksum = GenerateChecksum(File.ReadAllBytes(fileData.Path));

            return fileData;
        }

        /// <summary>
        /// Load file from store
        /// </summary>
        /// <param name="file">Stored file object</param>
        /// <returns>Stored file</returns>
        public async Task<byte[]> GetFileAsync(Models.File file)
        {
            return await ValidateFile(file);
        }

        /// <summary>
        /// Delete file from subject
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task DeleteFile(Models.File file)
        {
            await ValidateFile(file);

            File.Delete(file.Path);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Generate path to store file
        /// </summary>
        /// <param name="storageType">Type of file to store</param>
        /// <returns>Generated file path</returns>
        protected string GetFilePath(TStorageType storageType)
        {
            DateTime currentDate = DateTime.Now.Date;
            string basePath;
            string path = typeof(LocalStorage).GetProperty(storageType.ToString())?.GetValue(storageSettings)?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(path))
                basePath = Path.Combine(new string[] { storageSettings.AbsolutePath, path, currentDate.Year.ToString(), currentDate.Month.ToString(), currentDate.Day.ToString() });
            else
                basePath = Path.Combine(new string[] { storageSettings.AbsolutePath, storageSettings.General, storageType.ToString(), currentDate.Year.ToString(), currentDate.Month.ToString(), currentDate.Day.ToString() });

            Directory.CreateDirectory(basePath);
            return Path.Combine(basePath, Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Generate checksum of file
        /// </summary>
        /// <param name="buffer">The file buffer</param>
        /// <returns>The file checksum</returns>
        protected string GenerateChecksum(byte[] buffer)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(buffer);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Read file and validate checksum
        /// </summary>
        /// <param name="file">Stored file object</param>
        /// <returns>Stored file data</returns>
        protected async Task<byte[]> ValidateFile(Models.File file)
        {
            if (!File.Exists(file.Path))
                throw new FileNotExistsException(file.Path);

            byte[] buffer = await File.ReadAllBytesAsync(file.Path);

            if (!GenerateChecksum(buffer).Equals(file.Checksum))
                throw new InvalidChecksumException(file.Path);

            return buffer;
        }

        #endregion
    }
}
