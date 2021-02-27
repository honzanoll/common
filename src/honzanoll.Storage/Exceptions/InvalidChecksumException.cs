using System;

namespace honzanoll.Storage.Exceptions
{
    /// <summary>
    /// Stored file with invalid checksum exception
    /// </summary>
    public class InvalidChecksumException : StorageProviderException
    {
        #region Constructors

        public InvalidChecksumException() { }

        public InvalidChecksumException(string message) : base(message) { }

        public InvalidChecksumException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
