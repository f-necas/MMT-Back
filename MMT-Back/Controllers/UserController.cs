using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;

namespace MMT_Back.Controllers
{
    public abstract class UserController
    {
        public static void addMapping(WebApplication app)
        {

            app.MapGet("/user", async ([FromServices] DatabaseContext dbContext) =>
            {
                return await dbContext.Users.ToListAsync();
            });

            app.MapPost("/user", async ([FromServices] DatabaseContext dbContext, User useritem) =>
            {
                dbContext.Users.Add(useritem);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/user/{useritem.Id}", useritem);
            });

            app.MapGet("/user/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                return await dbContext.Users.FindAsync(id) is User user ? Results.Ok(user) : Results.NotFound();
            });
        }
    }
}
