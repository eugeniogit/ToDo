using FluentResults;

namespace ToDo.Domain.Services
{
    public interface IToDoService
    {
        Task<Result> AddAsync(Entities.ToDo toDo);
        IEnumerable<Entities.ToDo> List();
        Task<Entities.ToDo?> GetAsync(string id);
        Task<Result> CompleteAsync(string id);
        Task<Result> UpdateAsync(string id, string desc);
        Task<Result> DeleteAsync(string id);
    }
}
