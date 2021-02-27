using System;

namespace honzanoll.Web.Middlewares.Exceptions
{
    /// <summary>
    /// Missing parameter exception
    /// </summary>
    public class MissingParameterException : MiddlewareException
    {
        #region Constructors

        public MissingParameterException() { }

        public MissingParameterException(string message) : base(message) { }

        public MissingParameterException(string message, Exception innerException) : base(message, innerException) { }

        #endregion
    }
}
