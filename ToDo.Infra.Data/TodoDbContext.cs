using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Shared;
using System.Reflection;
using ToDo.Domain;
using ToDo.Domain.Repositories;
using ToDo.Infra.Data.Repositories;

namespace ToDo.Infra.Data
{
    public class TodoDbContext : DbContext, IToDoUnitOfWork
    {
        private readonly IMediator _mediator;

        public TodoDbContext(DbContextOptions<TodoDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;

        }
        public IToDoRepository ToDo => new ToDoRepository(this);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Ignore("Events");
                }
            }
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .Where(m => m.Events.Any())
                .SelectMany(m => m.Events)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var @event in events)
            {
                await _mediator.Publish(@event, cancellationToken);
            }

            return result;

        }

    }
}
