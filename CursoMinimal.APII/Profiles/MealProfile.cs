using AutoMapper;
using CursoMinimal.API.Entities;
using CursoMinimal.API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CursoMinimal.API.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, MealModel>().ReverseMap();
            CreateMap<Ingredient, IngredientModel>()
                .ForMember(
                    m => m.MealId,
                    o => o.MapFrom(s => s.Meals.First().Id)
                );
        }
        
    }
}
