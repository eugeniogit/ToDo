using MediatR;
using ToDo.Domain.Events;

namespace ToDo.Application.Handlers
{
    public class ToDoCompetedDomainEventHandler : INotificationHandler<ToDoCompletedDomainEvent>
    {
        public Task Handle(ToDoCompletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
