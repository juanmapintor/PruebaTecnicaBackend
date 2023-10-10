using PruebaTecnica.Repository;
using PruebaTecnica.Services;
using PruebaTecnica.SimpleAuthorization;

namespace PruebaTecnica
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PruebaTecnicaContext>();
            builder.Services.AddScoped<IUserService, UserService>(); 
            builder.Services.AddScoped<IPruebaTecnicaService, PruebaTecnicaService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<SimpleAuthorizationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}