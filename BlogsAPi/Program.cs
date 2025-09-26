using BlogsAPi.Interfaces;
using BlogsAPi.Models;
using BlogsAPi.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<BlogsSettings>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSingleton<MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("BlogsApi")));

builder.Services.AddScoped<IBlogs, BlogsService>();

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