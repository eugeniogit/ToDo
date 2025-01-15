using ToDo.WebApi.DTOs;

namespace ToDo.WebApi.Validations
{
    public static class Extensions
    {
        public static ToDoDTO ToDTO(this Domain.Entities.ToDo source)
        {
            return new ToDoDTO(source.Desc, source.Completed);
        }
    }
}
