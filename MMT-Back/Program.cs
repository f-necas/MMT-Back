using Microsoft.EntityFrameworkCore;
using MMT_Back;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using MMT_Back.EntityModels;
using MMT_Back.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using MMT_Back.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;

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
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudiance"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

/// <summary>
/// Hello World.
/// </summary>

app.MapGet("/", () =>
{
    return "Hello Unknown";
});

app.MapGet("/me", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] (ClaimsPrincipal user) =>
{
    return "Hello " + user.FindFirstValue(ClaimTypes.Name) + ", your id is : " + user.FindFirstValue("id");
});

app.MapPost("/Login", [AllowAnonymous] async([FromServices]DatabaseContext dbContext, User user) =>
{
    AuthenticationHelper authenticationHelper = new AuthenticationHelper(builder.Configuration);
    var dbUser = await dbContext.Users.Where(a => a.UserName == user.UserName && a.Password == user.Password).FirstOrDefaultAsync();
    if (dbUser != null)
        return Results.Ok(authenticationHelper.GenerateJWT(dbUser));
    else
        return Results.Unauthorized();
});

PlaceController.addMapping(app);
UserController.addMapping(app);
InvitationController.addMapping(app);
FriendshipController.addMapping(app);
EventController.addMapping(app);

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api v1");
	c.RoutePrefix = "api";
});

app.UseCors(builder =>
{
	builder
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
});

app.Run();