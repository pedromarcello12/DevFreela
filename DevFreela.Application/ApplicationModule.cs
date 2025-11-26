using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices()
                    .AddHandlers()
                    .AddValidators();
            // Add application services here
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IUserService, UserService>();
            // Register application services here
            return services;
        }
        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();
            // Register command and query handlers here
            return services;
        }
        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
            return services;
        }
    }

}
