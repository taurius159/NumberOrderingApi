using System.Numerics;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<INumbersRepository, TxtNumbersRepository>(provider =>
{
    return new TxtNumbersRepository("Data", provider.GetRequiredService<ILogger<TxtNumbersRepository>>());
});
builder.Services.AddTransient<ISortingService, BubbleSortService>();
builder.Services.AddTransient<ISortingService, QuickSortService>();
builder.Services.AddTransient<ISortingService, MergeSortService>();

builder.Services.AddTransient<ISortPerformerService, SortPerformerService>();

builder.Services.AddTransient<INumberOrderingService, NumberOrderingService>();
builder.Services.AddTransient<INumberValidationService, NumberValidationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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
