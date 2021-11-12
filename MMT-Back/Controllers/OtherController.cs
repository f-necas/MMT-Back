using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using MMT_Back.Models;

namespace MMT_Back.Controllers
{
    public abstract class OtherController
    {
        public static void addMapping(WebApplication app)
        {
            app.MapGet("/", () =>
            {
                return "Hello Unknown";
            });

            app.MapGet("/me", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] (ClaimsPrincipal user) =>
            {
                return "Hello " + user.FindFirstValue(ClaimTypes.Name) + ", your id is : " + user.FindFirstValue("id");
            });

            app.MapPost("/Login", [AllowAnonymous] async ([FromServices] DatabaseContext dbContext, User user) =>
            {
                AuthenticationHelper authenticationHelper = new AuthenticationHelper(builder.Configuration);
                var dbUser = await dbContext.Users.Where(a => a.UserName == user.UserName && a.Password == user.Password).FirstOrDefaultAsync();
                if (dbUser != null)
                    return Results.Ok(authenticationHelper.GenerateJWT(dbUser));
                else
                    return Results.Unauthorized();
            });
        }
    }
}
