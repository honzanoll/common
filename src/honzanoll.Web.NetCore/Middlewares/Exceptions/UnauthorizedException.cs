using System;

namespace honzanoll.Web.Middlewares.Exceptions
{
    /// <summary>
    /// Unauthorized action exception
    /// </summary>
    public class UnauthorizedException : MiddlewareException
    {
        #region Constructors

        public UnauthorizedException() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
