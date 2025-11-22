using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class FreelancerNotificationHandler : INotificationHandler<ProjectCretedNotification>
    {
        public Task Handle(ProjectCretedNotification notification, CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
        }
    }
}
