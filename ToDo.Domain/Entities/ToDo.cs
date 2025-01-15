using FluentValidation.Results;
using ToDo.Domain.Events;
using ToDo.Domain.Shared;
using ToDo.Domain.Validations;

namespace ToDo.Domain.Entities
{
    public class ToDo : Entity
    {
        public string Desc { get; protected set; }
        public bool Completed { get; protected set; }

        protected ToDo(Guid id, string desc) : base(id)
        {
            Id = id;
            Desc = desc;
        }

        protected ToDo() : base(Guid.NewGuid())
        {

        }

        public ToDo WithDesc(string desc)
        {
            Desc = desc;
            return this;
        }

        public ToDo Complete()
        {
            Completed = true;
            Events.Add(new ToDoCompletedDomainEvent(Id));
            return this;
        }

        public static ToDo Create(string desc)
        {
            var toDo = new ToDo(Guid.NewGuid(), desc);
            return toDo;
        }

        public override ValidationResult Validation()
        {
            return new ToDoValidation().Validate(this);
        }
    }
}
