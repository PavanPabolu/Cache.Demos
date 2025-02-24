
using Microsoft.Extensions.Caching.Memory;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMemoryCache();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //1. Without caching
        app.MapGet("/products", () => Results.Ok(GetProductsFromDB()));

        //2. Memory Caching
        app.MapGet("/products2", (IMemoryCache _cache) =>
        {
            string cachekey = "mykey";

            if (!_cache.TryGetValue(cachekey, out List<Product> products))
            {
                products = GetProductsFromDB();
                _cache.Set(cachekey, products, TimeSpan.FromSeconds(10));
            }

            return Results.Ok(products);
        });

        app.Run();
    }

    internal record Product(int Id, string Name, int Price);

    internal static List<Product> GetProductsFromDB()
    {
        Thread.Sleep(2000);
        return
        [
            new Product(1, "Laptop", 999),
            new Product(2, "HeadPhones", 199),
            new Product(3, "SmartPhone", 699),
        ];
    }

}
//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
