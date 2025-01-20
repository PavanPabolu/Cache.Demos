var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//To read data from Database
//builder.Services.AddDbContext<AdventureWorksDbContext>(options =>
//        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


//Distributed Caching - Setting Up Redis Server On Windows
//https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504

//PM> install-package Microsoft.Extensions.Caching.StackExchangeRedis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheServerUrl"];
});



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
