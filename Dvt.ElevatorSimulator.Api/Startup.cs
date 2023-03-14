using Dvt.ElevatorSimulator.Infrastructure.Interfaces;
using Dvt.ElevatorSimulator.Infrastructure.Services;
using Dvt.ElevatorSimulator.Infrastructure.Strategies;
using Microsoft.OpenApi.Models;

namespace Dvt.ElevatorSimulator.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Application services
        services
            .AddHostedService<SimulatorMovementService>()
            .AddSingleton<IElevatorControlSystem, ElevatorControlSystem>()
            .AddTransient<ISelectionStrategy, ClosestElevatorSelectionStrategy>()
            .AddSingleton<ISimulator, Simulator>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });
    }
    
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    
        app.UseRouting();
    
        app.UseAuthorization();
    
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}