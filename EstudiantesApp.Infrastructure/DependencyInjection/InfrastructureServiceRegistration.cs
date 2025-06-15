using EstudiantesApp.Application.Interfaces;
using EstudiantesApp.Application.Services;
using EstudiantesApp.Infrastructure.Data;
using EstudiantesApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EstudiantesApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            services.AddScoped<IMateriaRepository, MateriaRepository>();
            services.AddScoped<IMateriaEstudianteRepository, MateriaEstudianteRepository>();
            services.AddScoped<IInscripcionService, InscripcionService>();

            return services;
        }
    }

}
