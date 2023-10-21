using FluentValidation.Results;

namespace CRM.Service.NotificatorConfig;

public class Notificator : INotificator
{
    public Notificator()
    {
        _notifications = new List<string>();
    }

    private bool _notFound;
    private readonly List<string> _notifications;

    public void Handle(string message)
    {
        _notifications.Add(message);
    }

    public void Handle(List<ValidationFailure> failures)
    {
        failures.ForEach(err => Handle(err.ErrorMessage));
    }

    public void HandleNotFound()
    {
        if (HasNotification)
            throw new InvalidOperationException("Cannot call HandleNotFoundResource when there are notifications!");

        _notFound = true;
    }

    public IEnumerable<string> GetNotifications() => _notifications;

    public bool HasNotification => _notifications.Any();
    public bool IsNotFound => _notFound;
}