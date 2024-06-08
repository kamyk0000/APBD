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
        //Trips
        //builder.Services.AddScoped<ITripRepository, TripRepository>();
        //builder.Services.AddScoped<ITripService, TripService>();
        //Clients
        //builder.Services.AddScoped<IClientRepository, ClientRepository>();
        //builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        
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