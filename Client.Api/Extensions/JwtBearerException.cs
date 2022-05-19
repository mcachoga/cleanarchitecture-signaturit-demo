using System;

namespace Signaturit.Api.Extensions
{
    public class JwtBearerException : ApplicationException
    {
        public Exception ex;

        public JwtBearerException(int status, string message, Exception ex = null)
            : base($"{status} ({message}) problem")
        {
            this.ex = ex;
        }
    }
}