using Microsoft.EntityFrameworkCore;
using MMT_Back;
using MMT_Back.Controllers;
using MMT_Back.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? "Data Source=todos.db";

//builder.Services.AddEndpointsApiExplorer();
builder
    .AddSwagger()
    .AddAuthentication();

builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()));


var app = builder.Build();
app
    .UseAuthentication()
    .UseAuthorization()
    .UseSwagger();


OtherController.addMapping(app);
PlaceController.addMapping(app);
UserController.addMapping(app);
InvitationController.addMapping(app);
FriendshipController.addMapping(app);
EventController.addMapping(app);


app.UseCors(builder =>
{
	builder
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
});

app.Run();