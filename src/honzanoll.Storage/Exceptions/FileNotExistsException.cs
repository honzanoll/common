using System;

namespace honzanoll.Storage.Exceptions
{
    /// <summary>
    /// File not found in storage exception
    /// </summary>
    public class FileNotExistsException : StorageProviderException
    {
        #region Constructors

        public FileNotExistsException() { }

        public FileNotExistsException(string message) : base(message) { }

        public FileNotExistsException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
