using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using MMT_Back.Models;
using System.Net.Http.Formatting;

namespace MMT_Back.Controllers
{
    public abstract class EventController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/event", async ([FromServices] DatabaseContext dbContext) =>
            {
                return await dbContext.UserEvents.Include(_ => _.RequesterUser).Include(_ => _.EventPlace).ToListAsync() ;
            });

            app.MapGet("/event/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                return await dbContext.UserEvents.FindAsync(id) is UserEvent userEvent ? Results.Ok(userEvent) : Results.NotFound();
            });

            app.MapPost("/event", async ([FromServices] DatabaseContext dbContext, NewEventRequest request) =>
            {
                UserEvent eventItem = request.eventItem;
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

            app.MapPut("/event/{id}", async ([FromServices] DatabaseContext dbContext, int id, UserEvent inputEventItem) =>
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
