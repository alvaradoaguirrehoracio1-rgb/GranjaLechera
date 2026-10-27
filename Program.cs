using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MicroservicioGranja.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de controladores y prevención de bucles en JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 2. Conexión a la base de datos PostgreSQL
builder.Services.AddDbContext<GranjaDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// 3. Configuración de OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers = [new() { Url = "/" }];
        return Task.CompletedTask;
    });
});

// 4. Configuración permisiva de CORS para DevTunnels, Live Server (127.0.0.1 y localhost) y Nevtunes
builder.Services.AddCors(options =>
{
    options.AddPolicy("myApp", policyBuilder =>
    {
        policyBuilder.SetIsOriginAllowed(_ => true) // Permite cualquier origen dinámicamente (resuelve bloqueo de DevTunnels)
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
    });
});

// Registrar el servicio HttpClient para realizar peticiones HTTP externas
builder.Services.AddHttpClient();

var app = builder.Build();

// -------------------------------------------------------------
// PIPELINE DE PETICIONES HTTP
// -------------------------------------------------------------

// Habilitar CORS al inicio del pipeline
app.UseCors("myApp");

// Habilitar OpenAPI y Scalar SIEMPRE (incluso en Nevtunes o Entorno de Producción)
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("API Microservicio Granja & Fábrica")
           .WithTheme(ScalarTheme.Moon);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();