using CursoMinimal.API.DBContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:MealDbConnectionString"]));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
