using APIConfigs;
using APIConfigs.Policies;
using Application;
using Controllers.Middlewares;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistence(configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddApiVersioning();
builder.Services.AddMicroservicesCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(ConsumePolicy.FrontPolicy.ToString());

app.UseAuthorization();

app.MapControllers();

app.Run();
