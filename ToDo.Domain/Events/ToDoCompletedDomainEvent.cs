using MediatR;
using ToDo.Domain.Shared;

namespace ToDo.Domain.Events
{
    public record ToDoCompletedDomainEvent(Guid id) : IDomainEvent, INotification
    {
    }
}
