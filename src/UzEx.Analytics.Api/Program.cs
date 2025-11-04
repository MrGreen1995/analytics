using Serilog;
using UzEx.Analytics.Api.Extensions;
using UzEx.Analytics.Application;
using UzEx.Analytics.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    
    //app.ApplyMigrations();
}

//
app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();
//

app.UseHttpsRedirection();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();