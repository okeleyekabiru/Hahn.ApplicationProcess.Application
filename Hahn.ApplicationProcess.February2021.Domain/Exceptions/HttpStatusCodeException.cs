using System;
using Newtonsoft.Json.Linq;

namespace Hahn.ApplicationProcess.February2021.Domain.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCodeException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCodeException(int statusCode, string message, string returnCode, string returnStatus) : base(message)
        {
            StatusCode = statusCode;
            ReturnCode = returnCode;
            ReturnStatus = returnStatus;
            
        }

        public HttpStatusCodeException(int statusCode, Exception inner) : this(statusCode, inner.ToString())
        {
        }

        public HttpStatusCodeException(int statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/json";
        }

        public int StatusCode { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnStatus { get; set; }
       
        public string ContentType { get; set; } = @"application/json";
    }
}