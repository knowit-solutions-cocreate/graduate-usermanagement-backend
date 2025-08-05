using Backend;
using Backend.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? prodConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");
string? devConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string connectionString = prodConnectionString ?? devConnectionString ?? "No connection string";
Console.WriteLine("ConnectionString: " + connectionString);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy => {
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();