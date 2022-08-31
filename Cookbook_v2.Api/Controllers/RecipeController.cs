using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_v2.Api.Controllers
{
    [CookbookAuthorize]
    [ApiController]
    [Route( "api/[controller]" )]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController( IRecipeService recipeService )
        {
            _recipeService = recipeService;
        }

        [CookbookAllowAnonymous]
        [HttpGet( "details/{id}" )]
        public async Task<IActionResult> GetDetails( int id )
        {
            RecipeDetailsDto details = await _recipeService.GetRecipeDetailsDtoById(id);
            return Ok(details);
        }

        [CookbookAllowAnonymous]
        [HttpGet( "previews" )]
        public async Task<IActionResult> GetPreviews()
        {
            IReadOnlyList<Recipe> recipes;

            if ( TryGetUserId( out int userId ) )
            {
                recipes = await _recipeService.GetByUserId( userId );
            }
            else
            {
                recipes = await _recipeService.GetAll();
            }

            return Ok();
        }

        [HttpPost( "create" )]
        public async Task<IActionResult> CreateRecipe(
            [FromBody] CreateRecipeCommand createCommand )
        {
            Recipe recipe = await _recipeService.Create( createCommand );
            return Ok( recipe.Id );
        }

        [HttpDelete( "delete/{id}" )]
        public async Task<IActionResult> DeleteRecipe( int id )
        {
            await _recipeService.DeleteById( id );
            return Ok();
        }

        private bool TryGetUserId( out int userId )
        {
            if ( Request.QueryString.HasValue &&
                Request.Query[ "userId" ].Any() &&
                int.TryParse( Request.Query[ "userId" ], out userId ) )
            {
                return true;
            }
            userId = 0;
            return false;
        }
    }
}
