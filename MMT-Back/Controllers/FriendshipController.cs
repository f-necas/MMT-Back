using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using System.Security.Claims;
using MMT_Back.DTO;
using MMT_Back.Enum;

namespace MMT_Back.Controllers
{
    public abstract class FriendshipController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/friend/{id}",
                async ([FromServices] DatabaseContext dbContext, int id) =>
                {
                    return await dbContext.Friend
                        .Where(a => a.Approved && (a.RequestedById == id || a.RequestedToId == id))
                        .FirstOrDefaultAsync();
                });


            /*app.MapPost("/friend/add/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async ([FromServices] DatabaseContext dbContext, int id, ClaimsPrincipal claimUser) =>
                {
                    var userId = int.Parse(claimUser.FindFirstValue("id"));
                    var friend = Friend.AddFriendRequest(await dbContext.Users.FindAsync(userId), id);
                    var existingFriendship =
                        await dbContext.Friend.FirstOrDefaultAsync(f =>
                            f.RequestedById == id && f.RequestedToId == userId);
                    if (existingFriendship != null)
                    {
                        //Accept friend request
                        existingFriendship.AcceptFriendRequest();
                        friend.AcceptFriendRequest();
                    }

                    return Results.Ok(await dbContext.SaveChangesAsync());
                });*/

            /*app.MapPost("/friend/refuse/{id}",
                [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                ([FromServices] DatabaseContext dbContext, int id, ClaimsPrincipal claimUser) =>
                {
                    var userId = int.Parse(claimUser.FindFirstValue("id"));
                    var result = dbContext.Friend
                        .SingleOrDefaultAsync(x => x.RequestedById == id && x.RequestedToId == userId).Result;
                    if (result != null)
                    {
                        dbContext.Friend.Remove(result);
                        dbContext.SaveChanges();
                    }

                    return Results.Ok();
                });*/

            app.MapPost("/friend/interact",
                [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                async ([FromServices] DatabaseContext dbContext, FriendshipInteraction friendshipInteraction, ClaimsPrincipal claimUser) =>
                {
                    var userId = int.Parse(claimUser.FindFirstValue("id"));
                    var existingFriendship =
                        await dbContext.Friend.FirstOrDefaultAsync(f =>
                            f.RequestedById == friendshipInteraction.userId && f.RequestedToId == userId);

                    Friend friend;
                    switch (friendshipInteraction.action)
                    {
                        case FriendRequestFlag.Approved :
                            friend = Friend.AddFriendRequest(await dbContext.Users.FindAsync(userId), friendshipInteraction.userId);
                            
                            if (existingFriendship != null)
                            {
                                //Accept friend request
                                existingFriendship.AcceptFriendRequest();
                                friend.AcceptFriendRequest();
                            }

                            break;

                        case FriendRequestFlag.Rejected :
                            if (existingFriendship != null)
                            {
                                dbContext.Friend.Remove(existingFriendship);
                                
                            }

                            break;

                        case FriendRequestFlag.Blocked :
                            friend = Friend.AddFriendRequest(await dbContext.Users.FindAsync(userId), friendshipInteraction.userId);
                            friend.FriendRequestFlag = FriendRequestFlag.Blocked;

                            if (existingFriendship != null)
                            {
                                existingFriendship.FriendRequestFlag = FriendRequestFlag.Rejected;
                            }
                            break;

                    }
                    return Results.Ok(await dbContext.SaveChangesAsync());

                });
        }
    }
}