using SquareApi.BusinessDomain;
using SquareApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ISquareDetector, SquareDetector>();
builder.Services.AddSingleton<ISquareService, SquareService>();

// To run repositories in memory use following service registrations:
builder.Services.AddSingleton<IPointRepository, InMemoryPointRepository>();
builder.Services.AddSingleton<ISquareRepository, InMemorySquareRepository>();

// To run repositories in Redis use following service registrations:
//builder.Services.AddSingleton<IRedisClientProvider>(new RedisClientProvider("127.0.0.1", 6379));
//builder.Services.AddSingleton<IPointRepository, RedisPointRepository>();
//builder.Services.AddSingleton<ISquareRepository, RedisSquareRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
