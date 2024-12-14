using Microsoft.EntityFrameworkCore;
using PersonApi.Models;
using PersonApi.Data;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();

// Configuración de la base de datos en memoria con el nombre "users"

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseInMemoryDatabase("users"));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



// Configuracion de logging para la consola
builder.Logging.ClearProviders(); // eliminar proveedores de logs predeterminados
builder.Logging.AddConsole(); // agregar proveedor de la consola

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
