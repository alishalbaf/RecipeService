using Bogus;
using RecipeService.DTO;
using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Test
{
    public static class GlobalData
    {
        public static readonly List<RecipeDto>  recipes =GetFakeData();
        public static List<RecipeDto> GetFakeData()
        {
            int id = 1;

            var testIngerdient = new Faker<IngredientDto>()
                .RuleFor(i => i.Unit, u => u.Lorem.Word())
                .RuleFor(i => i.Value, v => v.Random.Decimal(0.1M, 10))
                .RuleFor(i => i.Name, u => u.Name.JobTitle());
            var testRecipes = new Faker<RecipeDto>()
                .RuleFor(r => r.Id, f => id++)
                .RuleFor(r => r.Name, f => f.Name.FullName())
                .RuleFor(r => r.Instructions, f => f.Lorem.Paragraph(1))
                .RuleFor(r => r.Ingredients, f => testIngerdient.Generate(f.Random.Int(2, 5)).ToList());
            return testRecipes.Generate(Random.Shared.Next(5, 10));
        }
    }
   
}
