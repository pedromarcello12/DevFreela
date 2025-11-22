using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCretedNotification>
    {
        public Task Handle(ProjectCretedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
