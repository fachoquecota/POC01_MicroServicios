using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Process_04.Dependency;
using Process_04.Repository;
using Process_04.Service;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddSingleton<IDependency, Dependency>();
builder.Services.AddTransient<IRepository>(provider =>
    new Repository(provider.GetService<IConfiguration>().GetValue<string>("ConnectionString")));
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
