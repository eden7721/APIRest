var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontEnd", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("PermitirFrontEnd");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
