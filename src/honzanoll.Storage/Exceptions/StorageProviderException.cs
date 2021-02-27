using System;

namespace honzanoll.Storage.Exceptions
{
    /// <summary>
    /// Base storage provider exception
    /// </summary>
    public class StorageProviderException : Exception
    {
        #region Constructors

        public StorageProviderException() { }

        public StorageProviderException(string message) : base(message) { }

        public StorageProviderException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
