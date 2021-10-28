using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using System.Security.Claims;

namespace MMT_Back.Controllers
{
    public abstract class FriendshipController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/friend/{id}", async ([FromServices] DatabaseContext dbContext, int id) => 
            { 
                return await dbContext.Friend.Where(a => a.Approved && (a.RequestedById == id || a.RequestedToId == id)).FirstOrDefaultAsync();
            });


            app.MapPost("/friend/add/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async ([FromServices] DatabaseContext dbContext, int id, ClaimsPrincipal claimUser) =>
            {

                var userId = Int32.Parse(claimUser.FindFirstValue("id"));
                var friend = Friend.AddFriendRequest(await dbContext.Users.FindAsync(userId), id);
                var existingFriendship = await dbContext.Friend.FirstOrDefaultAsync(f => f.RequestedById == id && f.RequestedToId == userId);
                if (existingFriendship != null)
                {
                    //Accept friend request
                    existingFriendship.AcceptFriendRequest();
                    friend.AcceptFriendRequest();
                }
                
                return Results.Ok(await dbContext.SaveChangesAsync());

            });
        }
    }
}
