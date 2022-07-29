using System;
using System.Net;

namespace Domain.Common;

[Serializable]
public class BusinessRuleException : Exception
{
    public string Code { get; set; }
    public string Message { get; set; }
    public string UserMessage { get; set; }
    public HttpStatusCode? HttpResponseCode { get; private set; }

    protected BusinessRuleException()
    {
    }

    public BusinessRuleException(
        string code,
        string message,
        string userMessage,
        HttpStatusCode? httpResponseCode = null)
        : base(message)
    {
        this.Code = code;
        this.Message = message;
        this.UserMessage = userMessage;
        this.HttpResponseCode = httpResponseCode;
    }

    protected BusinessRuleException(string message)
        : base(message)
    {
    }
}
