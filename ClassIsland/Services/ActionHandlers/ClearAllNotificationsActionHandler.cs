using System.Threading;
using System.Threading.Tasks;
using ClassIsland.Core.Abstractions.Services; // For IActionService
using ClassIsland.Services; // For INotificationHostService (assuming it's here)
using Microsoft.Extensions.Hosting; // For IHostedService

namespace ClassIsland.Services.ActionHandlers;

public class ClearAllNotificationsActionHandler : IHostedService
{
    private readonly INotificationHostService _notificationHostService;

    public ClearAllNotificationsActionHandler(IActionService actionService, INotificationHostService notificationHostService)
    {
        _notificationHostService = notificationHostService;
        // The GUID for the action ID will be defined in App.xaml.cs
        // For example: "classisland.notifications.clearAll"
        actionService.RegisterActionHandler("classisland.notifications.clearAll", HandleClearAllNotifications);
    }

    private void HandleClearAllNotifications(object? settings, string guid)
    {
        _notificationHostService.CancelAllNotifications();
        // Optionally, log that this action was triggered via in-app automation
        // App.GetService<ILogger<ClearAllNotificationsActionHandler>>().LogInformation("ClearAllNotifications action triggered via in-app automation.");
        // However, to use GetService, this class would need to be modified or have ILogger injected.
        // For now, direct logging is omitted here but can be added if NotificationHostService's own logging isn't sufficient.
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
