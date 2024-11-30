using CursoMinimal.API.DBContexts;
using CursoMinimal.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:MealDbConnectionString"]));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder().AddPolicy("RequireAdminFromBrazil", policy => policy.RequireRole("admin").RequireClaim("country", "Brazil"));

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthMeals",
        new()
        {
            Name = "Authorization",
            Description = "Token baseado em authenticacao e autorização",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        });
    options.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TokenAuthMeals"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.RegisterMealsEndpoints();
app.RegisterIngredientsEndpoints();

app.Run();