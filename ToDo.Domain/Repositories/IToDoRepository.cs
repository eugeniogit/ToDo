namespace ToDo.Domain.Repositories
{
    public interface IToDoRepository
    {
        Task<Entities.ToDo> AddAsync(Entities.ToDo toDo);
        Task<Entities.ToDo?> GetAsync(Guid id);
        IQueryable<Entities.ToDo> List();
        void Update(Entities.ToDo toDo);
        void Delete(Entities.ToDo toDo);
    }
}
