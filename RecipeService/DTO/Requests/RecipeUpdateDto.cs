#pragma warning disable CS1591
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RecipeService.DTO.Requests
{
    public class RecipeUpdateDto
    {
        
        [FromQuery]
        [JsonIgnore]
        public int Id { get; set; } 

        [Required(ErrorMessage = "The Name is Required")]
        public string? Name { get; set; }

        
        public string? Instructions { get; set; }

        
        [MinLength(1)]
        public List<IngredientDto>? Ingredients { get; set; }
    }
}
