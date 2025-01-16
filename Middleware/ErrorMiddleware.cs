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
            _=>HttpStatusCode.InternalServerError
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode =   (int)statusCode;

        await context.Response.WriteAsJsonAsync(exception.Message);
    }
}