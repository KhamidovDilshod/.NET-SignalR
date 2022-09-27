using Microsoft.AspNetCore.SignalR;
using Realtime.API.Services;

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
        builder.Services.AddSignalR();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapHub<LocationHub>("/location");
        app.MapControllers();

        app.Run();
    }
}