using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.DTO;
using MMT_Back.EntityModels;

namespace MMT_Back.Controllers
{
    public abstract class UserController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/user", async ([FromServices] DatabaseContext dbContext) =>
            {
                return await dbContext.Users.Select(_ => new
                {
                    _.Id,
                    _.UserName,
                    sentRequests = _.SentFriendRequests
                        .Where(f => f.FriendRequestFlag != Enum.FriendRequestFlag.Approved)
                        .Select(e => new {e.RequestedTo.Id, e.RequestedTo.UserName}).ToList(),
                    receivedRequests = _.ReceievedFriendRequests
                        .Where(f => f.FriendRequestFlag != Enum.FriendRequestFlag.Approved)
                        .Select(e => new {e.RequestedBy.Id, e.RequestedBy.UserName}).ToList(),
                }).ToListAsync();
            });

            app.MapPost("/user", async ([FromServices] DatabaseContext dbContext, User useritem) =>
            {
                dbContext.Users.Add(useritem);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/user/{useritem.Id}", useritem);
            });

            app.MapGet("/user/{id}",
                async ([FromServices] DatabaseContext dbContext, int id) =>
                {
                    return await dbContext.Users
                            .Include(_ => _.UserEvents)
                            .Where(_ => _.Id == id).FirstAsync()
                        is User user
                        ? Results.Ok(user)
                        : Results.NotFound();
                });


            app.MapGet("/user/friends/{id}",
                async ([FromServices] DatabaseContext dbContext, int id, ClaimsPrincipal claimUser) =>
                {
                    var userId = Int32.Parse(claimUser.FindFirstValue("id"));

                    var tmp = await dbContext.Friend
                        .Join(dbContext.Friend, j1 => j1.RequestedById, j2 => j2.RequestedToId,
                            (j1, j2) => new {f1 = j1, f2 = j2})
                        .Where(_ => _.f1.FriendRequestFlag == Enum.FriendRequestFlag.Approved &&
                                    _.f1.RequestedById == id && _.f1.RequestedToId == userId)
                        .Where(_ => _.f2.FriendRequestFlag == Enum.FriendRequestFlag.Approved &&
                                    _.f2.RequestedById == userId && _.f2.RequestedToId == id)
                        .Select(_ => new
                        {
                            id = _.f1.RequestedById,
                            _.f1.RequestedBy.UserName
                        }).ToListAsync();
                    return tmp;
                });
        }
    }
}