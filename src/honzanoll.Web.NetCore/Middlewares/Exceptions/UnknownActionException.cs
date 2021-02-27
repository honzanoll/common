using System;

namespace honzanoll.Web.Middlewares.Exceptions
{
    /// <summary>
    /// Unknown action exception
    /// </summary>
    public class UnknownActionException : MiddlewareException
    {
        #region Constructors

        public UnknownActionException() { }

        public UnknownActionException(string message) : base(message) { }

        public UnknownActionException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
