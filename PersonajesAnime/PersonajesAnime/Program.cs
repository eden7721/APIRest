using MongoDB.Bson;
using MongoDB.Driver;
using PersonajesAnime.Models;
using PersonajesAnime.Respositories;
using PersonajesAnime.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<PersonajesRepository>();
builder.Services.AddScoped<PersonajeService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontEnd", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("PermitirFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
