using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using QuickLoanAPI.Application.Helpers;
using QuickLoanAPI.Domain.Helpers;
using System.Net;
using System.Net.Mail;

namespace QuickLoanAPI.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      string messageError = "";
      string messageException = "";
      int httpStatusCode = 0;
      string ipAddress = httpContext.GetIpAddess();
      int port = httpContext.Connection.RemotePort;
      string ipStr = ipAddress != null ? $"{ipAddress}:{port}" : "unknown";
      string requestPath = httpContext.Request.Path;
      httpContext.Request.EnableBuffering();

      try
      {
        await _next(httpContext);

        var logEventInfo = new LogEventInfo(NLog.LogLevel.Debug, _logger.Name, messageException);
        logEventInfo.Properties["message"] = $"request at path:{requestPath} from ip: {ipStr}";
        _logger.Log(logEventInfo);
      }
      catch (ClientException ex)
      {
        messageError = ex.Message;
        messageException = ex.Message;
        httpStatusCode = (int)HttpStatusCode.BadRequest;

        var exeptionHelper = new ExeptionHelper()
        {
          HttpContext = httpContext,
          HttpStatusCode = httpStatusCode,
          MessageError = messageError,
          MessageException = messageException,
          RequestPath = requestPath,
          Ex = ex,
          IpStr = ipStr
        };

        await ExeptionsLog(exeptionHelper);
      }
      catch (Exception ex)
      {
        messageError = "Uknown Error, please contact support for more information";
        messageException = ex.Message;
        httpStatusCode = (int)HttpStatusCode.InternalServerError;
        var exeptionHelper = new ExeptionHelper()
        {
          HttpContext = httpContext,
          HttpStatusCode = httpStatusCode,
          MessageError = messageError,
          MessageException = messageException,
          RequestPath = requestPath,
          Ex = ex,
          IpStr = ipStr
        };

        await ExeptionsLog(exeptionHelper);
      }
    }
    private async Task ExeptionsLog(ExeptionHelper exeption)
    {
      await HandleExceptionAsync(exeption.HttpContext, exeption.MessageError, exeption.MessageException, exeption.HttpStatusCode);

      var headersJson = JsonConvert.SerializeObject(exeption.HttpContext.Request.Headers);
      var bodyJson = await ReadRequestBodyAsync(exeption.HttpContext);

      var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, _logger.Name, exeption.MessageException);
      logEventInfo.Properties["requestPath"] = exeption.RequestPath;
      logEventInfo.Properties["requestHeader"] = headersJson;
      logEventInfo.Properties["message"] = $"Error Message: {exeption.Ex.Message}; Additional Info: request at Path Method:{exeption.HttpContext.Request.Method} - {exeption.RequestPath} from ip: {exeption.IpStr} ";
      logEventInfo.Properties["httpStatusCode"] = exeption.HttpStatusCode.ToString();
      logEventInfo.Properties["bodyJson"] = bodyJson;
      logEventInfo.Properties["stackTrace"] = exeption.Ex.ToString();
      _logger.Log(logEventInfo);
    }

    private async Task HandleExceptionAsync(HttpContext context, string errorMessage, string exceptionMessage, int statusCode)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = statusCode;
      string message;
#if DEBUG
      message = exceptionMessage;
#else
      message = errorMessage;
#endif
      var messageJson = JsonConvert.SerializeObject(new { ErrorMessage = message });
      await context.Response.WriteAsync(messageJson);
    }

    public async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
      string body = null;
      if (context.Request.Body.CanSeek)
      {
        context.Request.Body.Position = 0;
      }

      using (var reader = new StreamReader(context.Request.Body))
      {
        body = await reader.ReadToEndAsync();
      }

      if (context.Request.Body.CanSeek)
      {
        context.Request.Body.Position = 0;
      }

      return body;
    }

  }


  public static class ContextExtension
  {
    public static string GetIpAddess(this HttpContext context)
    {
      const string defaultValue = "0.0.0.0";

      string remoteAdd = defaultValue;

      HttpRequest request;
      try
      {
        // store a local ref to the request object:
        request = context.Request;

        //
        // check proxy forwarder indicators first:
        string forwarderForIp = request.Headers["X-Forwarded-For"];
        if (!string.IsNullOrEmpty(forwarderForIp))
        {
          int firstCommaIndex = forwarderForIp.IndexOf(',');
          if (firstCommaIndex > 0)
          {
            // X-Forwarded-For can return multiple ip's, so
            // just get the first one.
            // ex: X-Forwarded-For: client, proxy1, proxy2
            forwarderForIp = forwarderForIp.Substring(0, firstCommaIndex).Trim();
          }

          if (!forwarderForIp.Equals("unknown",
              StringComparison.InvariantCultureIgnoreCase))
          {
            return forwarderForIp;
          }
        }
        string remoteAddress = request.Headers["REMOTE_ADDR"];
        if (!string.IsNullOrEmpty(forwarderForIp))
        {
          return remoteAddress;
        }

        // get the client ip from server from server vars:
        remoteAdd = context.Connection.RemoteIpAddress.ToString(); //request.UserHostAddress.ToString();

        return !string.IsNullOrEmpty(remoteAdd) ? remoteAdd : defaultValue;
      }
      catch (Exception)
      {
        return defaultValue;
      }
    }
  }

  public record ExeptionHelper
  {
    public HttpContext HttpContext { get; set; }
    public string MessageError { get; set; }
    public string MessageException { get; set; }
    public int HttpStatusCode { get; set; }
    public string RequestPath { get; set; }
    public Exception Ex { get; set; }
    public string IpStr { get; set; }
  }
}
