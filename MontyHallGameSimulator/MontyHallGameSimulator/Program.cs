using MassTransit;
using MontyHallGameSimulator.Providers;
using System.Reflection;
using MontyHallSimulatorDataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISimulationProvider,  SimulationProvider>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMontyHallSimulatorDataAccess(builder.Configuration);
var montyHallSerivceUrl = builder.Configuration["Services:MontyHallGame"] ?? throw new Exception("Monty Hall service not configured.");
builder.Services.AddHttpClient<ISimulationProvider, SimulationProvider>(opt => opt.BaseAddress = new Uri(montyHallSerivceUrl));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.Host(builder.Configuration["rabbit:host"], builder.Configuration["rabbit:virtualhost"], h =>
        {
            h.Username(builder.Configuration["rabbit:username"]);
            h.Password(builder.Configuration["rabbit:password"]);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//removing development check for demo
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
