using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Services;
using Tutorial10.Core.Models;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClinicDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IClinicService, ClinicService>();


builder.Services.AddControllers();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();