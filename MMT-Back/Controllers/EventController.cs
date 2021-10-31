using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using MMT_Back.Models;
using System.Net.Http.Formatting;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Internal;

namespace MMT_Back.Controllers
{
    public abstract class EventController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/event",
                async ([FromServices] DatabaseContext dbContext) =>
                {
                    return await dbContext.UserEvents.Include(_ => _.RequesterUser).Include(_ => _.EventPlace)
                        .ToListAsync();
                });

            app.MapGet("/event/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                return await dbContext.UserEvents
                    .Select(_ => new {userevent = _, username = _.RequesterUser.UserName}).FirstOrDefaultAsync();
            });

            app.MapGet("/event/{id}/map", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                var coordinates = dbContext.UserEvents.Where(_ => _.Id==id)
                    .Join(dbContext.Place, ue => ue.EventPlaceId, p => p.Id,
                        ((userevent, place) => place.Coordinate)).FirstOrDefaultAsync();
                return Results.Redirect("https://maps.google.com/?q=" + coordinates.Result.X + "," + coordinates.Result.Y, preserveMethod: true);
                //return Results.Redirect("https://www.openstreetmap.org/#map=8/" + coordinates.Result.X + "/" + coordinates.Result.Y, preserveMethod: true);
            });

            app.MapGet("/event/{id}/invited",
                async ([FromServices] DatabaseContext dbContext, int id) =>
                {
                    return await dbContext.Invitation.Where(x => x.UserEventId == id).ToListAsync();
                });

            app.MapPost("/event", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (
                [FromServices] DatabaseContext dbContext, NewEventRequestDTO request,
                ClaimsPrincipal claimUser) =>
            {
                UserEvent eventItem = request.eventItem;
                eventItem.RequesterUserId = Int32.Parse(claimUser.FindFirstValue("id"));
                IEnumerable<int> users = request.users;
                dbContext.UserEvents.Add(eventItem);
                await dbContext.SaveChangesAsync();

                Invitation invitationItem;
                foreach (int user in users)
                {
                    invitationItem = new Invitation();
                    invitationItem.StatusCode = "SENT";
                    invitationItem.InvitedUserId = user;
                    invitationItem.UserEventId = eventItem.Id;
                    dbContext.Invitation.Add(invitationItem);
                    //TODO better insert
                    await dbContext.SaveChangesAsync();
                }


                return Results.Created($"/event/{eventItem.Id}", eventItem);
            });

            app.MapPut("/event/{id}",
                async ([FromServices] DatabaseContext dbContext, int id, UserEvent inputEventItem) =>
                {
                    var eventItem = await dbContext.UserEvents.FindAsync(id);
                    if (eventItem == null)
                    {
                        return Results.NotFound();
                    }

                    eventItem.EventPlace = inputEventItem.EventPlace;
                    eventItem.EventDate = inputEventItem.EventDate;
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                });
        }
    }
}