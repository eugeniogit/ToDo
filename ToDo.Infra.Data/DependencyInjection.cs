using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDo.Domain;
using ToDo.Domain.Repositories;
using ToDo.Infra.Data.Repositories;

namespace ToDo.Infra.Data
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("ToDo"); ;

			services.AddDbContext<TodoDbContext>(options => options.UseNpgsql(connectionString));
			services.AddScoped<IToDoUnitOfWork, TodoDbContext>();
			services.AddScoped<IToDoRepository, ToDoRepository>();

			return services;

		}

	}
}
