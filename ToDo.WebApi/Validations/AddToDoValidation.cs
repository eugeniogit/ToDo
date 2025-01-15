using FluentValidation;
using ToDo.WebApi.Commands;

namespace ToDo.WebApi.Validations
{
    public class AddToDoValidation : AbstractValidator<AddToDo>
    {
        public AddToDoValidation()
        {
            RuleFor(x => x.Desc)
                .NotNull()
                .WithMessage("Desc is required");
        }
    }
}
