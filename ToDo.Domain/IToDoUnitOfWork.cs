using ToDo.Domain.Repositories;
using ToDo.Domain.ToDo;

namespace ToDo.Domain
{
    public interface IToDoUnitOfWork : IUnitOfWork
    {
        IToDoRepository ToDo { get; }
    }
}
