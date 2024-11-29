using AutoMapper;
using CursoMinimal.API.DBContexts;
using CursoMinimal.API.Entities;
using CursoMinimal.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:MealDbConnectionString"]));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

var mealsEndpoints = app.MapGroup("/meals");
var mealsParamEndpoints = mealsEndpoints.MapGroup("/{mealsId:int}");
var ingredientsEndpoints = mealsEndpoints.MapGroup("/ingredients");

mealsEndpoints.MapGet("", async Task<Results<NoContent, Ok<IEnumerable<MealModel>>>> (MealDbContext dbContext, IMapper mapper, [FromQuery(Name = "name")] string? mealName) =>
{
    var mealsEntity = await dbContext.Meals.Where(x => mealName == null || x.Name.ToLower().Contains(mealName.ToLower())).ToListAsync();

    if (mealsEntity == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mapper.Map<IEnumerable<MealModel>>(mealsEntity));
});

mealsEndpoints.MapPost("", async Task<CreatedAtRoute<MealModel>> (MealDbContext dbContext, IMapper mapper, [FromBody] RequestMealModel? requestMeal) =>
{
    var mealEntity = mapper.Map<Meal>(requestMeal);
    dbContext.Add(mealEntity);
    await dbContext.SaveChangesAsync();

    var mealReturn = mapper.Map<MealModel>(mealEntity);

    return TypedResults.CreatedAtRoute(mealReturn, "GetMeals", new { mealsId = mealReturn.Id });
});


mealsParamEndpoints.MapGet("", async Task<Results<NoContent, Ok<MealModel>>> (MealDbContext dbContext, IMapper mapper, int mealsId) =>
{
    var mealsEntity = mapper.Map<MealModel>(await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId));
    if (mealsEntity == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mealsEntity);
}).WithName("GetMeals");

mealsParamEndpoints.MapPut("", async Task<Results<NotFound, CreatedAtRoute<MealModel>>> (MealDbContext dbContext, IMapper mapper, int mealsId, [FromBody] RequestMealModel? requestMeal) =>
{
    var mealEntity = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId);
    if (mealEntity == null)
        return TypedResults.NotFound();

    mapper.Map(requestMeal, mealEntity);
    
    await dbContext.SaveChangesAsync();

    var mealReturn = mapper.Map<MealModel>(mealEntity);

    return TypedResults.CreatedAtRoute(mealReturn, "GetMeals", new { mealsId = mealReturn.Id });
});

mealsParamEndpoints.MapDelete("", async Task<Results<NotFound, NoContent>> (MealDbContext dbContext, IMapper mapper, int mealsId) =>
{
    var mealEntity = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == mealsId);
    if (mealEntity == null)
        return TypedResults.NotFound();

    dbContext.Remove(mealEntity);
    await dbContext.SaveChangesAsync();


    return TypedResults.NoContent();
});

mealsParamEndpoints.MapGet("/ingredients", async Task<Results<NoContent, Ok<List<IngredientModel>>>> (MealDbContext dbContext, IMapper mapper, int mealsId) =>
{
    var ingredients = mapper.Map<IEnumerable<IngredientModel>>
        (
            (
                await dbContext.Meals.Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == mealsId)
            )?.Ingredients
        ).ToList();

    if (ingredients.Count == 0 || ingredients == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(ingredients);
});

app.Run();