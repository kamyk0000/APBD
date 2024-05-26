using DBApp.Context;
using DBApp.Repositories;
using DBApp.Services;
using Microsoft.EntityFrameworkCore;

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
        
        //builder.Services.AddScoped<IPrescriptionRepository, IPrescriptionRepository>();
        //builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

        builder.Services.AddDbContext<MyDb>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
        );

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