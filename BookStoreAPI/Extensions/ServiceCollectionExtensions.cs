using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Services;

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

        }
    }
}
