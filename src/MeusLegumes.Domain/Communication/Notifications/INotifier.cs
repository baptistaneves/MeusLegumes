namespace MeusLegumes.Domain.Communication.Notifications;

public interface INotifier
{
    bool HasNotifications();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}
