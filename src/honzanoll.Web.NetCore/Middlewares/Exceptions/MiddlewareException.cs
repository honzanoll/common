using System;

namespace honzanoll.Web.Middlewares.Exceptions
{
    /// <summary>
    /// Common middleware exception
    /// </summary>
    public class MiddlewareException : Exception
    {
        #region Constructors

        public MiddlewareException() { }

        public MiddlewareException(string message) : base(message) { }

        public MiddlewareException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
