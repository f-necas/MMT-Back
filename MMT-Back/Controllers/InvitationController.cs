using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;

namespace MMT_Back.Controllers
{
    public class InvitationController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/myinvitations/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                return await dbContext.Invitation.Where(x => x.InvitedUser.Id == id).FirstOrDefaultAsync();
            });

            app.MapPost("/invitation", async ([FromServices] DatabaseContext dbContext, Invitation invitationItem, IEnumerable<int> users) =>
            {
                foreach (int user in users)
                {
                    invitationItem.InvitedUser.Id = user;
                    dbContext.Invitation.Add(invitationItem);
                    //TODO better insert
                    await dbContext.SaveChangesAsync();
                }
                
                return Results.Created($"/event/{invitationItem.Id}", invitationItem);
            });

        }
    }
}
