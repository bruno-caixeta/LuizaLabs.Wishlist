using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LuizaLabs.Wishlist.App.ResponseModels
{
    public class ErrorInfo
    {
        public ErrorInfo()
        {
            errorId = Guid.NewGuid();
        }

        public ErrorInfo(HttpStatusCode statusCode, string message)
        {
            errorId = Guid.NewGuid();
            this.statusCode = statusCode;
            this.message = message;
        }

        public ErrorInfo(HttpStatusCode statusCode, string message, string details)
        {
            errorId = Guid.NewGuid();
            this.statusCode = statusCode;
            this.message = message;
            this.details = details;
        }

        public Guid errorId { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }
        public string details { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            return builder.Append("ErrorId: ").Append(errorId).AppendLine()
                .Append("StatusCode: ").Append(statusCode).AppendLine()
                .Append("Message: ").Append(message).AppendLine()
                .Append("Details: ").Append(details).AppendLine().ToString();
        }
    }
}
