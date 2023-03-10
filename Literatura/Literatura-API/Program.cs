using Literatura_API.Interfaces;
using Literatura_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPbtRepository, PbtRepository>();
builder.Services.AddScoped<ISpiewniczekRepository, SpiewniczekRepository>();
builder.Services.AddScoped<IZLiederRepository, ZLiederRepository>();
builder.Services.AddScoped<IMannaRepository, MannaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
