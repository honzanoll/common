using honzanoll.Storage.Abstractions.Models;

namespace honzanoll.Storage.Models.Settings
{
    /// <summary>
    /// Local storage provider settings
    /// </summary>
    public class LocalStorage : IStorageProviderSettings
    {
        #region Properties

        /// <summary>
        /// Absolute storage base path
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Default storage path
        /// </summary>
        public string General { get; set; }

        #endregion
    }
}
