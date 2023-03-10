using System.Diagnostics;
using System.Reflection.Metadata;
using Kinetic.Sdk;
using Kinetic.Sdk.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(factory => KineticSdk.Setup(
    new KineticSdkConfig(
        index:  Kinetic.Example.Constants.Index,
        endpoint: Kinetic.Example.Constants.Endpoint,
        environment: Kinetic.Example.Constants.Environment,
        logger: (factory.GetService<ILogger>() ?? NullLogger.Instance)
    )));

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