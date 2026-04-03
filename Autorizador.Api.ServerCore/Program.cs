using Autorizador.Api.ServerCore;
using Autorizador.Aplicacion;
using Autorizador.Aplicacion.Servicios;
using Autorizador.Core.Utils;
using Autorizador.Infraestructura.Contenedor;
using Microsoft.Extensions.Options;

// Add services to the container.
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();

var configuracionVariables = config.GetSection("Configuracion").Get<Configuracion>();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    // Add services to the container.

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers();

    builder.Services.AddHttpClient(Options.DefaultName).ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                true
        }
    );

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CORSPolicy",
            builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());
    });
    builder.Services.Configure<Configuracion>(builder.Configuration.GetSection("Configuracion"));
    builder.Services.AddConfiguracionInfraestructura();

    builder.Services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
    builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

    builder.Services.AddConfiguracionCQRS();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CORSPolicy");

    app.UseAuthorization();

    app.MapControllers();

    Console.WriteLine($"Inicio del servicio: {Api.NOMBRE} {Api.VERSION}");

    app.Run();
}
catch (Exception exception)
{

    // Se asegura de mostrar el mensaje de error en la inicialización del sistema. 
    Console.WriteLine(exception.ToString());
    // NLog: catch setup errors
    Thread.Sleep(5000);
    throw;
}