using FluentValidation.Results;

namespace ToDo.Domain.Shared
{
	public abstract class Entity
	{
        public Entity(Guid id)
        {
			Id = id;
			Events = new List<IDomainEvent>();
		}

        public Guid Id { get; protected set; }
        public ICollection<IDomainEvent> Events { get; set; }
		public abstract ValidationResult Validation();

	}
}
