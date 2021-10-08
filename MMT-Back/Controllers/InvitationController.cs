using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using System.Security.Claims;

namespace MMT_Back.Controllers
{
    public class InvitationController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/myinvitations", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async ([FromServices] DatabaseContext dbContext, ClaimsPrincipal user) =>
            {
                return await dbContext.Invitation.Where(x => x.InvitedUser.Id == Int32.Parse(user.FindFirstValue("id"))).ToListAsync();
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
