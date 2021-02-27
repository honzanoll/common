namespace honzanoll.Storage.Models.Settings
{
    /// <summary>
    /// Base storage provider settings
    /// </summary>
    public class StorageSettings
    {
        #region Properties

        /// <summary>
        /// Local storage provider settings
        /// </summary>
        public LocalStorage LocalStorage { get; set; }

        #endregion
    }
}
