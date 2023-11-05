using AutoMapper;
using RecipeService.DTO;
using RecipeService.DTO.Requests;
using RecipeService.Models;

namespace RecipeService.Mappings
{
    public class DomainToDtoProfile:Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<IngredientDto, Ingredient>().ReverseMap();
            CreateMap<RecipeDto,Recipe>().ReverseMap();
            
            CreateMap<RecipeUpdateDto,Recipe>().ForAllMembers( opts=>opts.Condition((src,destination,srcMember)=>  srcMember is not null));
            CreateMap<RecipeCreateDto, Recipe>();
        }
    }
}
