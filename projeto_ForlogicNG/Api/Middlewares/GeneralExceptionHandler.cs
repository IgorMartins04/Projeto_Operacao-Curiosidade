using System.Net;
using System.Text.Json;

namespace ProjetoForlogic.Middlewares
{
    public class HandlerExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerExceptionsMiddleware> _logger;

        public HandlerExceptionsMiddleware(RequestDelegate next, ILogger<HandlerExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma exceção não tratada: {Message}", ex.Message);
                await HandlerExceptionAsync(context, ex);
            }
        }

        private static Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorDatails = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title = "Erro interno no servidor.",
                status = (int)HttpStatusCode.InternalServerError,
                datail = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                instance = context.Request.Path.ToString()
            };

#if DEBUG
            errorDatails = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title = "Erro interno no servidor.",
                status = (int)HttpStatusCode.InternalServerError,
                datail = exception.Message,
                instance = context.Request.Path.ToString()
            };
#endif

            var responseJson = JsonSerializer.Serialize(errorDatails);
            return context.Response.WriteAsync(responseJson);
        }
    }
}
