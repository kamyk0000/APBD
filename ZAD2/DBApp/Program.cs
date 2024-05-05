using DBApp.Repositories;
using DBApp.Services;

namespace DBApp;

public class Program
{
    public static void Main(string[] args)
    {
        //Test.Main2();
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        builder.Services.AddScoped<IWarehouseService, WarehouseService>();

        var app = builder.Build();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}