using System;
using System.Collections.Generic;

namespace Domain.Common;

public static class ApplicationMessage
{
    private const string ApiCode = "CLN";
    private const string CommonUserErrorMessage =
        "Your transaction cannot be performed at this time. Please try again later.";

    public static string UnhandledError = "-1".Code();
    public static readonly string Success = "0".Code();
    public static string TimeoutOccurred = "1".Code();
    public static string UnExpectedHttpResponseReceived = "2".Code();
    public static string InvalidParameter = "3".Code();


    private static readonly Dictionary<string, string> ErrorMessages =
        new Dictionary<string, string>()
        {
            {UnhandledError, "Unhandled exception."},
            {Success, "Operation Successful."},
            {TimeoutOccurred, "Timeout Error."},
            {UnExpectedHttpResponseReceived, "A response was received with an unexpected httpCode."},
            {InvalidParameter, "Invalid parameter."}

        };

    private static readonly Dictionary<string, string> UserMessages =
        new Dictionary<string, string>()
        {
            {UnhandledError, CommonUserErrorMessage},
            {Success, "Operation Successful."},
            {TimeoutOccurred, CommonUserErrorMessage},
            {UnExpectedHttpResponseReceived, CommonUserErrorMessage},
            {InvalidParameter, CommonUserErrorMessage}

        };


    private static string Code(this string code)
    {
        return $"{ApiCode}{code}";
    }

    public static string Message(this string code)
    {
        ErrorMessages.TryGetValue(code, out var errorMessage);
        return errorMessage;
    }

    public static string UserMessage(this string code)
    {
        UserMessages.TryGetValue(code, out var errorMessage);
        return errorMessage;
    }
}

