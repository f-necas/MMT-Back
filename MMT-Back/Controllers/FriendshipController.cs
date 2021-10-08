using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;

namespace MMT_Back.Controllers
{
    public abstract class FriendshipController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/friend/{id}", async ([FromServices] DatabaseContext dbContext, int id) => 
            { 
                return await dbContext.Friend.Where(a => a.Approved && (a.).FirstOrDefaultAsync();
            });

            app.MapPost("/friend/add/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                Friend.AddFriendRequest(new User(), 2);
                await dbContext.SaveChangesAsync();
            });
        }
    }
}
