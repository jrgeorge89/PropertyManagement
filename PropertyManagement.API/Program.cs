using Microsoft.OpenApi.Models;
using PropertyManagement.API.Middlewares;
using PropertyManagement.Application.Interfaces;
using PropertyManagement.Application.Services;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Configuration;
using PropertyManagement.Infrastructure.Data;
using PropertyManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

// Registrar servicios
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Property Management API",
        Version = "v1",
        Description = "API para la gestión de propiedades inmobiliarias",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "dev@propertymanagement.com"
        }
    });
});

var app = builder.Build();

// Configurar pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property Management API v1");
        c.RoutePrefix = string.Empty; // Para mostrar la UI de Swagger en la raíz
    });
}

// Middleware personalizado de manejo de excepciones
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
