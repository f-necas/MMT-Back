using Microsoft.EntityFrameworkCore;
using MMT_Back;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using MMT_Back.EntityModels;
using MMT_Back.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
builder.Services.AddCors();
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer();

builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

/// <summary>
/// Hello World.
/// </summary>
app.MapGet("/", () => "Hello World!");

PlaceController.addMapping(app);
UserController.addMapping(app);
InvitationController.addMapping(app);
FriendshipController.addMapping(app);
EventController.addMapping(app);

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api v1");
	c.RoutePrefix = string.Empty;
});

app.UseCors(builder =>
{
	builder
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
});

app.Run();