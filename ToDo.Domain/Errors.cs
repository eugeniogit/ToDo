using FluentResults;

namespace ToDo.Domain
{
    public struct Errors
    {
        public static IError ToDoNotFound => new Error(nameof(ToDoNotFound));
    }
}
