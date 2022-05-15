using StatefulWebComparer.Formatter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(configure =>
{
    configure.InputFormatters.Add(new CustomTypeFormatter());
});

builder.Services.AddSession(); // add session persistence
builder.Services.AddDistributedMemoryCache(); // add cache

var app = builder.Build();

app.UseAuthorization();
app.UseSession(); // use session persistence

app.MapControllers();

app.Run();
