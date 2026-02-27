using MyBGList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGet("/error", () => Results.Problem());
app.MapGet("/error/test", () => { throw new Exception("test"); });

app.MapControllers();

app.Run();
