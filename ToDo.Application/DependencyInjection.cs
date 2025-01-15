using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Services;
using ToDo.Domain.Services;

namespace ToDo.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IToDoService, ToDoService>();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
			return services;

		}
	}
}
