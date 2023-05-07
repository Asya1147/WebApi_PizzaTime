using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi_PizzaTime.Data;
using WebApi_PizzaTime.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApi_PizzaTimeContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApi_PizzaTimeContext") ?? throw new InvalidOperationException("Connection string 'WebApi_PizzaTimeContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
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
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
