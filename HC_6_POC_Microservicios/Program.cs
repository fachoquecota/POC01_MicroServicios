using HC_6_POC_Microservicios.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(new HttpClient());
builder.Services.AddSingleton("https://localhost:7190/api/Process");
builder.Services.AddSingleton("https://localhost:7044/api/Process");
builder.Services.AddSingleton("http://34.199.24.55:8058/registrarencolamientoservice");
builder.Services.AddSingleton("https://e-connect.enotriasa.com/e-regulado");


//builder.Services.AddSingleton<Process_01>();
//builder.Services.AddHealthChecks()
//    .AddCheck("Service 1", () =>
//        HealthCheckResult.Healthy("Service 1 is working as expected."), new[] { "database"})
//    .AddCheck("Service 2", () =>
//        HealthCheckResult.Unhealthy("Service 2 isn't running"))
//    .AddCheck("Service 3", () =>
//        HealthCheckResult.Degraded("Service 3 took longer  than expected"), new[] { "database","mysql"});
builder.Services.AddHealthChecks()
    .AddCheck<Process_01>("Lateny PROCESS 01")
    ;
builder.Services.AddHealthChecks()
    .AddCheck<Process_02>("Lateny PROCESS 02")
    ;
builder.Services.AddHealthChecks()
    .AddCheck<NotificacionesInstantaneasQA>("Lateny QA NOTIFICACIONES INSTANTANEAS")
    ;
builder.Services.AddHealthChecks()
    .AddCheck<NotificacionesInstantaneasPROD>("Lateny PROD NOTIFICACIONES INSTANTANEAS")
    ;
builder.Services.AddHealthChecks()
    .AddCheck<FirmaDigital>("Lateny FIRMA DIGITAL NOTIFICACIONES INSTANTANEAS")
    ;
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecksUI();
//app.UseHealthChecks("/health");
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
//app.MapHealthChecks("/healthOverView", new HealthCheckOptions()
//{
//    Predicate = _ => false
//});
app.MapHealthChecks("/health/databases", new HealthCheckOptions()
{
    Predicate = ser => ser.Tags.Contains("database"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
