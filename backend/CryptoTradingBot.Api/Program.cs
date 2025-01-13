using CryptoTradingBot.Api.Services.Binance;
using CryptoTradingBot.Api.Strategies;
using CryptoTradingBot.Api.BackTesting;
using CryptoTradingBot.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register services
builder.Services.AddSingleton<IBinanceApiService, BinanceApiService>();
builder.Services.AddSingleton<ITradingStrategy, SimpleMovingAverageStrategy>();
builder.Services.AddSingleton<BackTestingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// Add custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
