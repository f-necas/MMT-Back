using Microsoft.EntityFrameworkCore;
using MMT_Back;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using MMT_Back.EntityModels;
using MMT_Back.Controllers;

var builder = WebApplication.CreateBuilder(args);




var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? "Data Source=todos.db";

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
	Description = "MMT web api implementation using Minimal Api in Asp.Net Core",
	Title = "MMT Api",
	Version = "v1",

}));


var app = builder.Build();
app.UseSwagger();

/// <summary>
/// Hello World.
/// </summary>
app.MapGet("/", () => "Hello World!");

app.MapGet("/user/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
{
	return await dbContext.Users.FindAsync(id) is User user ? Results.Ok(user) : Results.NotFound();
});

PlaceController.addMapping(app);

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api v1");
	c.RoutePrefix = string.Empty;
});
app.Run();