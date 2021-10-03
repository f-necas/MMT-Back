using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;

namespace MMT_Back.Controllers
{
       
    public abstract class PlaceController
    {
        public static void addMapping(WebApplication app)
        {
            /// <summary>
            /// Get all places available.
            /// </summary>
            app.MapGet("/place", async ([FromServices] DatabaseContext dbContext) =>
            {
                return await dbContext.Place.ToListAsync();
            });

            app.MapGet("/place/{id}", async ([FromServices] DatabaseContext dbContext, int id) =>
            {
                return await dbContext.Place.FindAsync(id) is Place place ? Results.Ok(place) : Results.NotFound();
            });

            app.MapPost("/place", async ([FromServices] DatabaseContext dbContext, Place placeItem) =>
            {
                dbContext.Place.Add(placeItem);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/todoitems/{placeItem.Id}", placeItem);
            });

            app.MapPut("/place/{id}", async ([FromServices] DatabaseContext dbContext, int id, Place inputPlaceItem) =>
            {
                var placeItem = await dbContext.Place.FindAsync(id);
                if (placeItem == null)
                {
                    return Results.NotFound();
                }

                placeItem.Name = inputPlaceItem.Name;
                placeItem.Address = inputPlaceItem.Address;
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
