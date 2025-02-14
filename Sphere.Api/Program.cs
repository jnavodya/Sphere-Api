using Sphere.Application.Interface;
using Sphere.Application.Services;
using Sphere.Domain.Repositories;
using Sphere.Infrasturcture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register repositories and services
builder.Services.AddScoped<ITimerRepository, TimerRepository>();
builder.Services.AddScoped<ITimerService, TimerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
