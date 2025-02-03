using BookStore.Core.Abstractions.Configurations;
using BookStore.Infrastructure.Data;
using BookStore.Web.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddRegistrations();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            var connectionString = builder.Configuration.GetConnectionString("BookStoreDB") ?? throw new InvalidOperationException("Connection string 'BookStoreDB' not found.");
            builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connectionString));

            //Add Configurations
            builder.Services.Configure<LogCleanupConfig>(builder.Configuration.GetSection("LogCleanupConfig"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
                dbContext.Database.Migrate(); 
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
