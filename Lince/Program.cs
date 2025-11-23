using Lince.Infrastructure.Persistence.Repositories;
using Lince.UseCase;
using Lince.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Lince;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var swaggerConfig = builder
            .Configuration
            .GetSection("Swagger")
            .Get<SwaggerConfig>();
        
        // Adiciona serviços ao container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = swaggerConfig.Title,
                    Version = "v1",
                    Description = swaggerConfig.Description,
                    Contact = swaggerConfig.Contact
                });
                
                swagger.EnableAnnotations();

                foreach (var server in swaggerConfig.Servers)
                {
                    swagger.AddServer(new OpenApiServer
                    {
                        Url   = server.Url,
                        Description = server.Name
                    });
                }
            }
        );
        
        builder.Services.AddDbContext<LinceContext>(options =>
            options.UseOracle(builder.Configuration.GetConnectionString("OracleDb"))
        );
        
        builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
        builder.Services.AddScoped<ICameraRepository, CameraRepository>();
        builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
        builder.Services.AddScoped<IOperadorRepository, OperadorRepository>();
        builder.Services.AddScoped<ISetorRepository, SetorRepository>();
        builder.Services.AddScoped<ISupervisorRepository, SupervisorRepository>();
        
        builder.Services.AddScoped<ISupervisorUseCase, SupervisorUseCase>();
        builder.Services.AddScoped<IEquipeUseCase, EquipeUseCase>();
        builder.Services.AddScoped<IOperadorUseCase, OperadorUseCase>();
        builder.Services.AddScoped<ISetorUseCase, SetorUseCase>();
        builder.Services.AddScoped<ICameraUseCase, CameraUseCase>();
        builder.Services.AddScoped<IAlertaUseCase, AlertaUseCase>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
                {
                    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "Lince.API v1");
                    ui.RoutePrefix = string.Empty;
                }            
            );
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}