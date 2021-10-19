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
                    sentRequests = _.SentFriendRequests.Where(f => f.FriendRequestFlag != Enum.FriendRequestFlag.Approved).Select(e => new { e.RequestedTo.Id, e.RequestedTo.UserName }).ToList(),
                    receivedRequests = _.ReceievedFriendRequests.Where(f => f.FriendRequestFlag != Enum.FriendRequestFlag.Approved).Select(e => new { e.RequestedBy.Id, e.RequestedBy.UserName }).ToList(),
                }).ToListAsync();
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

            app.MapGet("/user/friends/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                var tmp = await dbContext.Friend.Where(_ => _.FriendRequestFlag == Enum.FriendRequestFlag.Approved && _.RequestedById == id).Select(_ => new
                {
                    id = _.RequestedToId,
                    _.RequestedTo.UserName
                }).ToListAsync();
                tmp.AddRange(await dbContext.Friend.Where(_ => _.FriendRequestFlag == Enum.FriendRequestFlag.Approved && _.RequestedToId == id).Select(_ => new
                {
                    id = _.RequestedById,
                    _.RequestedBy.UserName
                }).ToListAsync());
                return tmp;
            });
        }
    }
}
