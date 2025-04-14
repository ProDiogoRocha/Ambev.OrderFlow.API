using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ambev.OrderFlow.Application.EventsHandlers
{
    public class LogEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : INotification
    {
        private static readonly ActivitySource ActivitySource = new("Application.Events");
        private readonly ILogger<LogEventHandler<TEvent>> _logger;

        public LogEventHandler(ILogger<LogEventHandler<TEvent>> logger)
        {
            _logger = logger;
        }

        public Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            using var activity = ActivitySource.StartActivity($"Handle {typeof(TEvent).Name}");

            _logger.LogInformation("Event handled: {EventName}", typeof(TEvent).Name);

            activity?.SetTag("event.name", typeof(TEvent).Name);
            activity?.SetTag("event.payload", notification.ToString());

            return Task.CompletedTask;
        }
    }
}
