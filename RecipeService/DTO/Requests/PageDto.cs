#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace RecipeService.DTO.Requests
{
    public record PageDto
    {
        [Range(1,int.MaxValue)]
        public int? StartIndex {  get; set; }
        [Range(1, 2048)]
        public int? PageSize { get;set; }
    }
}
