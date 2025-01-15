using FluentResults;
using ToDo.Domain;
using ToDo.Domain.Services;

namespace ToDo.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoUnitOfWork _uow;

        public ToDoService(IToDoUnitOfWork uow)
        {
            _uow = uow;
        }

        async Task<Result> IToDoService.AddAsync(Domain.Entities.ToDo toDo)
        {
            var validation = toDo.Validation();

            if (!validation.IsValid)
            {
                return Result.Fail(validation.Errors.Select(m => m.ErrorMessage));
            }

            await _uow.ToDo.AddAsync(toDo);
            await _uow.SaveChangesAsync();

            return Result.Ok();

        }

        IEnumerable<Domain.Entities.ToDo> IToDoService.List()
        {
            return _uow.ToDo.List();
        }

        Task<Domain.Entities.ToDo?> IToDoService.GetAsync(string id)
        {
            return _uow.ToDo.GetAsync(new Guid(id));
        }

        async Task<Result> IToDoService.UpdateAsync(string id, string desc)
        {
            var existing = await _uow.ToDo.GetAsync(new Guid(id));

            if (existing == null)
            {
                return Result.Fail(Errors.ToDoNotFound);
            }

            existing.WithDesc(desc);

            _uow.ToDo.Update(existing);
            await _uow.SaveChangesAsync();

            return Result.Ok();

        }

        async Task<Result> IToDoService.CompleteAsync(string id)
        {
            var existing = await _uow.ToDo.GetAsync(new Guid(id));

            if (existing == null)
            {
                return Result.Fail(Errors.ToDoNotFound);
            }

            existing.Complete();

            _uow.ToDo.Update(existing);
            await _uow.SaveChangesAsync();

            return Result.Ok();

        }

        async Task<Result> IToDoService.DeleteAsync(string id)
        {
            var existing = await _uow.ToDo.GetAsync(new Guid(id));

            if (existing == null)
            {
                return Result.Fail(Errors.ToDoNotFound);
            }

            _uow.ToDo.Delete(existing);
            await _uow.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
