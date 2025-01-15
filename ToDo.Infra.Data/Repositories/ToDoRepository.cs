using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Repositories;

namespace ToDo.Infra.Data.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TodoDbContext _context;
        public ToDoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.ToDo> AddAsync(Domain.Entities.ToDo toDo)
        {
            var entity = await _context.AddAsync(toDo);
            return entity.Entity;
        }

        public void Delete(Domain.Entities.ToDo toDo)
        {
            _context.Remove(toDo);

        }

        public void Update(Domain.Entities.ToDo toDo)
        {
            _context.Update(toDo);
        }

        public async Task<Domain.Entities.ToDo?> GetAsync(Guid id)
        {
            return await _context.Set<Domain.Entities.ToDo>().FindAsync(id);
        }

        public IQueryable<Domain.Entities.ToDo> List()
        {
            return _context.Set<Domain.Entities.ToDo>().AsNoTracking();
        }
    }
}
