using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Handlers;
using BookStore.Core.Services;
using MediatR;

namespace BookStore.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRegistrations(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserLogService, UserLogService>();

            //Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services.AddScoped<IRequestHandler<UserCreatedEvent>, UserCreatedHandler>();
            services.AddScoped<IRequestHandler<UserRoleUpdatedEvent>, UserRoleUpdatedHandler>();
        }
    }
}
