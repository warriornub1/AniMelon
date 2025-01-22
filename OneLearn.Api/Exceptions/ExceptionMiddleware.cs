using Microsoft.Identity.Client;
using Newtonsoft.Json;
using OneLearn.Api.Exceptions;
using OneLearn.Application.Response;
using System.Net;

namespace OneLearn.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionHandlerFactory _handleFactory;

        public ExceptionMiddleware(RequestDelegate next, ExceptionHandlerFactory handlerFactory)
        {
            _next = next;
            _handleFactory = handlerFactory;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        var existingBody = context.Response.Body;
        //        var newBody = new MemoryStream();
        //        context.Response.Body = newBody;

        //        try
        //        {
        //            await _next(context);
        //            if (context.Response.StatusCode == 200 && !IsFileResponse(context))
        //            {
        //                var newResponse = await FormatResponse(context.Response);
        //                newBody.Seek(0, SeekOrigin.Begin);

        //                context.Response.Clear();
        //                await context.Response.WriteAsync(newResponse);
        //            }
        //        }
        //        finally
        //        {
        //            newBody.Seek(0, SeekOrigin.Begin);
        //            await newBody.CopyToAsync(existingBody);
        //            context.Response.Body = existingBody;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var handler = _handlerFactory.GetHandler(ex);
        //        if (handler != null)
        //        {
        //            await handler.HandleAsync(context, ex);
        //        }
        //        else
        //        {
        //            _logger.LogError(ex, ex.Message);
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //            var errorResponse = new APIResponse();
        //            errorResponse.isSuccess = false;
        //            errorResponse.AddError(ErrorCodes.InternalServerError, ErrorMessages.InternalServerError);

        //            await context.Response.WriteAsJsonAsync(errorResponse);
        //        }
        //    }
        //}

        //private async Task<string> FormatResponse(HttpResponse response)
        //{
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    var content = await new StreamReader(response.Body).ReadToEndAsync();
        //    var customResponse = new APIResponse
        //    {
        //        isSuccess = response.StatusCode == 200
        //    };

        //    try
        //    {
        //        customResponse.result = JsonConvert.DeserializeObject(content);
        //    }
        //    catch (JsonReaderException)
        //    {
        //        customResponse.result = content;
        //    }

        //    var json = JsonConvert.SerializeObject(customResponse);

        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    return $"{json}";
        //}

        //private bool IsFileResponse(HttpContext context)
        //{
        //    var response = context.Response;
        //    if(response.ContentType != null && response.ContentType.StartsWith("application/"))
        //    {
        //        var contentDisposiition = context.Response.Headers["Content-Diposition"].ToString();
        //        if (!string.IsNullOrEmpty(contentDisposiition) && contentDisposiition.Contains("attachment"))
        //            return true;

        //        return false;
        //    }
        //}
    }
}
