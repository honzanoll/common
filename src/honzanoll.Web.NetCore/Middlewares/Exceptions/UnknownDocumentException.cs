using System;

namespace honzanoll.Web.Middlewares.Exceptions
{
    /// <summary>
    /// Unknown document exception
    /// </summary>
    public class UnknownDocumentException : MiddlewareException
    {
        #region Constructors

        public UnknownDocumentException() { }

        public UnknownDocumentException(string message) : base(message) { }

        public UnknownDocumentException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
