using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add StorageService to the container. It keeps a list of all users with all binary left/right data
builder.Services.AddSingleton<DiffStorageService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
