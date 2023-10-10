using System.Collections;
using System.Net;

namespace DatDotnetConduit.Infrasturcture.Exceptions
{
    public class RestException : Exception
    {
        public static string STATUS_CODE = "statusCode";
        public HttpStatusCode StatusCode { get; set; }
        public RestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public override IDictionary Data => new Dictionary<string, int>()
        {
            {STATUS_CODE, (int)StatusCode }
        };
    }
}