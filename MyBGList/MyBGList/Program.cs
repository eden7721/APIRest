using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using MyBGList;
using MyBGList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => //CORS middleware
{
    options.AddDefaultPolicy(cfg =>
    {
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]);
        cfg.AllowAnyMethod();
        cfg.AllowAnyHeader();
    });
    options.AddPolicy(name: "AnyOrigin", 
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyMethod();
            cfg.AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
{
    app.UseDeveloperExceptionPage();
}
else app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors();
//app.UseCors("AnyOrigin"); usamos el cors que denominamos "AnyOrigin" mas arriba

app.UseAuthorization();
//Minimal API
/*
app.MapGet("/BoardGames", () => new[]
    {
        new BoardGame() {
            Id = 1,
            Name = "Axis & Allies",
            Year = 1981
        },
        new BoardGame() {
            Id = 2,
            Name = "Citadels",
            Year = 2000
        },
        new BoardGame() {
            Id = 3,
            Name = "Terraforming Mars",
            Year = 2016
        }
    }
);*/
app.MapGet("/error", 
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]
() => Results.Problem()); //RequireCors("AnyOrigin")
app.MapGet("/error/test", 
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)]
() => { throw new Exception("test"); }); //.RequireCors("AnyOrigin")

app.MapGet("/cod/test",
    [EnableCors("AnyOrigin")]
    [ResponseCache(NoStore = true)] () =>
    
        Results.Text("<script>" +
            "window.alert('Your client supports JavaScript!" +
            "\\r\\n\\r\\n" +
            $"Server Time (UTC): {DateTime.UtcNow.ToString("o")}" +
            "\\r\\n" +
            "Client time (UTC): ' + new Date().toISOString());" + 
            "</script>" +
            "<noscript>Your client does not support Javascript</noscript>",
            "text/html"
            ));
/*Results.Text("<script>" +
"window.alert('Your client supports JavaScript!" +
"\\r\\n\\r\\n" +
$"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
"\\r\\n" +
"Client time (UTC): ' + new Date().toISOString());" +
"</script>" +
"<noscript>Your client does not support JavaScript</noscript>",
"text/html"));*/


app.MapControllers();

app.Run();
