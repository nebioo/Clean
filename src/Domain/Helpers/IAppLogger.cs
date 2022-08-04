using System;
using System.Reflection;

namespace ApplicationService.Common;

public interface IAppLogger
{
    void MethodEntry(object logEventInfo, MethodBase methodBase);
    void MethodExit(object logEventInfo, MethodBase methodBase, double methodExecutionDuration, string messageCode);
    void Trace(string message, MethodBase methodBase);
    void Exception(Exception exception, MethodBase methodBase, string extraMessage = "");
}
