namespace honzanoll.Storage.Models
{
    /// <summary>
    /// Stored file
    /// </summary>
    public class File
    {
        #region Properties

        /// <summary>
        /// File path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File checksum
        /// </summary>
        public string Checksum { get; set; }

        #endregion
    }
}
