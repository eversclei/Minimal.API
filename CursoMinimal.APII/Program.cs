using AutoMapper;
using CursoMinimal.API.DBContexts;
using CursoMinimal.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:MealDbConnectionString"]));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/meals", async Task<Results<NoContent, Ok<IEnumerable<MealModel>>>> (MealDbContext dbContext, IMapper mapper, [FromQuery(Name = "name")] string? mealName) =>
{
    var mealsEntity = await dbContext.Meals.Where(x => mealName == null || x.Name.ToLower().Contains(mealName.ToLower())).ToListAsync();

    if (mealsEntity == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mapper.Map <IEnumerable<MealModel>>(mealsEntity));
});


app.MapGet("/meal/{mealsId:int}/ingredients", async Task<Results<NoContent, Ok<List<IngredientModel>>>> (MealDbContext dbContext, IMapper mapper, int mealsId) =>
{
    var ingredients = mapper.Map<IEnumerable<IngredientModel>>
        (
            (
                await dbContext.Meals
                        .Include(x => x.Ingredients)
                        .FirstOrDefaultAsync(x => x.Id == mealsId)
            )?.Ingredients
        ).ToList();

    if (ingredients.Count == 0 || ingredients == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(ingredients);
});

app.MapGet("/meal/{mealsId:int}", async Task<Results<NoContent, Ok<MealModel>>> (MealDbContext dbContext, IMapper mapper, int mealsId) =>
{
    var mealsEntity = mapper.Map<MealModel>(await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId));
    if (mealsEntity == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mealsEntity);
});

app.Run();