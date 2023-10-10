using DatDotnetConduit.Infrasturcture.Extensions;
using DatDotnetConduit.Infrasturcture.Middlewares;
using DatDotnetConduit.Infrasturcture.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureMigration();
builder.Services.ConfigureAuthenticate(builder.Configuration);
builder.Services.ConfigureCurrentUser();
builder.Services.ConfigureMediator();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
