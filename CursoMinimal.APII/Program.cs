using CursoMinimal.API.DBContexts;
using CursoMinimal.API.EndpointHandlers;
using CursoMinimal.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:MealDbConnectionString"]));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.RegisterMealsEndpoints();
app.RegisterIngredientsEndpoints();

app.Run();