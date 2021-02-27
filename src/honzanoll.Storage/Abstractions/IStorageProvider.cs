using honzanoll.Storage.Abstractions.Models;
using honzanoll.Storage.Models;
using System;
using System.Threading.Tasks;

namespace honzanoll.Storage.Abstractions
{
    /// <summary>
    /// Common storage provider
    /// </summary>
    /// <typeparam name="TType">Storage provider</typeparam>
    public interface IStorageProvider<TType, TStorageType>
        where TType : IStorageProviderSettings
        where TStorageType : Enum
    {
        #region Public methods

        /// <summary>
        /// Store file
        /// </summary>
        /// <param name="storageType">Type of file to store</param>
        /// <param name="file">File to store</param>
        /// <param name="fileName">File name</param>
        /// <returns>Stored file object</returns>
        Task<File> StoreFileAsync(TStorageType storageType, byte[] file, string fileName);

        /// <summary>
        /// Load file from store
        /// </summary>
        /// <param name="file">Stored file object</param>
        /// <returns>Stored file</returns>
        Task<byte[]> GetFileAsync(File file);

        /// <summary>
        /// Delete file from subject
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task DeleteFile(File file);

        #endregion
    }
}
