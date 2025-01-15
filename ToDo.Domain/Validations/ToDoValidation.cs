using FluentValidation;

namespace ToDo.Domain.Validations
{
    public class ToDoValidation : AbstractValidator<Entities.ToDo>
    {
        public ToDoValidation()
        {
            RuleFor(x => x.Desc)
                .NotNull()
                .WithMessage("Desc is required");
        }
    }
}
