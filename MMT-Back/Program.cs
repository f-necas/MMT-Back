using Microsoft.EntityFrameworkCore;
using MMT_Back;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? "Data Source=todos.db";

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
	Description = "MMT web api implementation using Minimal Api in Asp.Net Core",
	Title = "MMT Api",
	Version = "v1",

}));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/user/{id}", async (int id) =>
{
	// TODO
});

app.Run();