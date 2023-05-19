using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Core.Services;
using Logistic.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRepository<Vehicle>, InMemoryRepository<Vehicle>>();
builder.Services.AddScoped<IEntityService<Vehicle>, VehicleService>();

builder.Services.AddSingleton<IRepository<Warehouse>, InMemoryRepository<Warehouse>>();
builder.Services.AddScoped<IEntityService<Warehouse>, WarehouseService>();

builder.Services.AddScoped<IReportRepository<Vehicle>, JsonRepository<Vehicle>>();
builder.Services.AddScoped<IReportRepository<Vehicle>, XmlRepository<Vehicle>>();
builder.Services.AddScoped<IReportService<Vehicle>, ReportService<Vehicle>>();

builder.Services.AddScoped<IReportRepository<Warehouse>, JsonRepository<Warehouse>>();
builder.Services.AddScoped<IReportRepository<Warehouse>, XmlRepository<Warehouse>>();
builder.Services.AddScoped<IReportService<Warehouse>, ReportService<Warehouse>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
