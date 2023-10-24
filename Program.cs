using FeedAPI.Models;
using FeedAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    // ª`¤Jservice
    builder.Services.AddScoped<IFeedService, FeedService>();
    builder.Services.AddDbContext<FeedingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FeedContext")));

}
// Add services to the container.

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
