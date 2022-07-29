using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace ApplicationService.Common;

public class AppLogger : IAppLogger
{
    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppLogger(ILogger logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public void MethodEntry(object logEventInfo, MethodBase methodBase)
    {
        _logger.Information(
            "{LogType} {ClassName} {AssemblyName} {MethodName} {LogData} {LogHeader}",
            LogType.MethodEntry,
            methodBase.DeclaringType?.Name,
            methodBase?.DeclaringType?.Assembly.GetName().Name,
            methodBase?.Name,
            JsonSerializer.Serialize(logEventInfo),
            GetHeaderJson(_httpContextAccessor?.HttpContext?.Request.Headers));
    }

    public void MethodExit(object logEventInfo, MethodBase methodBase, double methodExecutionDuration, string messageCode)
    {
        _logger.Information(
            "{LogType} {ClassName} {AssemblyName} {MethodName} {MethodExecutionDuration} {MessageCode} {LogData} {LogHeader}",
            LogType.MethodEntry,
            methodBase.DeclaringType?.Name,
            methodBase?.DeclaringType?.Assembly.GetName().Name,
            methodBase?.Name,
            methodExecutionDuration,
            messageCode,
            JsonSerializer.Serialize(logEventInfo),
            GetHeaderJson(_httpContextAccessor?.HttpContext?.Request.Headers));
    }

    public void Trace(string message, MethodBase methodBase)
    {
        _logger.Information(
            "{LogType} {ClassName} {AssemblyName} {MethodName} {LogData} {LogHeader}",
            LogType.MethodEntry,
            methodBase.DeclaringType?.Name,
            methodBase?.DeclaringType?.Assembly.GetName().Name,
            methodBase?.Name,
            message,
            GetHeaderJson(_httpContextAccessor?.HttpContext?.Request.Headers));
    }

    public void Exception(Exception exception, MethodBase methodBase, string extraMessage = "")
    {
        var isCustomException = exception.GetType().Name?.Contains("BusinessRuleException");

        _logger.Error(
            "{LogType} {ClassName} {AssemblyName} {MethodName} {Exception} {Message} {ExtraMessage} {IsCustomException} {LogHeader}",
            LogType.MethodEntry,
            methodBase.DeclaringType?.Name,
            methodBase?.DeclaringType?.Assembly.GetName().Name,
            methodBase?.Name,
            JsonSerializer.Serialize(exception),
            exception.Message,
            extraMessage,
            isCustomException,
            GetHeaderJson(_httpContextAccessor?.HttpContext?.Request.Headers));
    }

    private static string GetHeaderJson(IHeaderDictionary requestHeaders)
    {
        var result = "";

        if (requestHeaders.Count > 0)
        {
            result += "{";
            if (requestHeaders.ContainsKey("Authorization"))
            {
                var jwtValue = requestHeaders["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtValue);
                foreach (var claim in token.Claims)
                {
                    result += "\"" + claim.Type + "\":\"" + claim.Value + "\",";
                }
            }
            foreach (var requestHeader in requestHeaders)
            {
                result += "\"" + requestHeader.Key + "\":\"" + requestHeader.Value + "\",";
            }
            result = result.Remove(result.Length - 1) + "}";
        }

        return result;
    }
}
