using Ambev.OrderFlow.Application.Notifications;
using MediatR;
using System.Text.Json;

namespace Ambev.OrderFlow.API.Middlewares
{
    public class ExceptionNotificationMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionNotificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await mediator.Publish(new ErrorNotification("Erro inesperado", ex.ToString()));

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = "Ocorreu um erro interno.",
                    detalhe = ex.Message
                }));
            }
        }
    }
}
