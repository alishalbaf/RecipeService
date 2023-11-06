#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace RecipeService.DTO.Requests
{
    public class PageDto
    {
        [Range(1,int.MaxValue)]
        public int? StartIndex {  get; set; }
        [Range(1, int.MaxValue)]
        public int? PageSize { get;set; }
    }
}
