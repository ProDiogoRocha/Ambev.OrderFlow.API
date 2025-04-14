using MediatR;

namespace Ambev.OrderFlow.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public string Message { get; }
        public string? StackTrace { get; }

        public ErrorNotification(string message, string? stackTrace = null)
        {
            Message = message;
            StackTrace = stackTrace;
        }

        public override string ToString()
        {
            return $"Error: {Message}{(string.IsNullOrEmpty(StackTrace) ? "" : $", StackTrace: {StackTrace}")}";
        }
    }
}
