using System.Net;
using System.Text.Json;

public class ErrorMiddleware{
    private readonly RequestDelegate next;
    public ErrorMiddleware(RequestDelegate _next){
        next = _next;
    }

    public async Task InvokeAsync(HttpContext context){
        try{
            await next(context);
        }catch(Exception ex){
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception){
        var statusCode = exception switch{
            KeyNotFoundException =>HttpStatusCode.NotFound,
            UnauthorizedAccessException=>HttpStatusCode.Unauthorized,
            InvalidDataException=>HttpStatusCode.Conflict,
            NullReferenceException=>HttpStatusCode.NotFound,
            InvalidOperationException=>HttpStatusCode.BadRequest,
            _=>HttpStatusCode.InternalServerError
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode =   (int)statusCode;
        var message = new{
            status = "error",
            message = exception.Message
        };
        await context.Response.WriteAsJsonAsync(message);
    }
}