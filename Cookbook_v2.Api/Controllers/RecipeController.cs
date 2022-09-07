using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.Builders;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Search;
using Cookbook_v2.Domain.Search.RecipeModel;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_v2.Api.Controllers
{
    [CookbookAuthorize]
    [ApiController]
    [Route( "api/[controller]" )]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly RecipeDetailsDtoBuilder _recipeDetailsDtoBuilder;
        private readonly RecipePreviewDtoBuilder _recipePreviewDtoBuilder;

        public RecipeController(
            IRecipeService recipeService,
            RecipeDetailsDtoBuilder recipeDetailsDtoBuilder,
            RecipePreviewDtoBuilder recipePreviewDtoBuilder )
        {
            _recipeService = recipeService;
            _recipeDetailsDtoBuilder = recipeDetailsDtoBuilder;
            _recipePreviewDtoBuilder = recipePreviewDtoBuilder;
        }

        [CookbookAllowAnonymous]
        [HttpGet( "details/{id}" )]
        public async Task<IActionResult> GetDetails( int id )
        {
            Recipe recipe = await _recipeService.GetById( id );
            RecipeDetailsDto details = await _recipeDetailsDtoBuilder.Build( recipe );

            return Ok( details );
        }

        [CookbookAllowAnonymous]
        [HttpGet( "previews" )]
        public async Task<IActionResult> GetPreviews()
        {
            IReadOnlyList<RecipePreviewDto> recipes;

            if ( TryGetUserIdFromQuery( out int userId ) )
            {
                recipes = await _recipeService.GetRecipePreviewDtosByUserId( userId );
            }
            else
            {
                recipes = await _recipeService.GetRecipePreviewDtos();
            }

            return Ok( recipes );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "search" )]
        public async Task<IActionResult> Search( [FromBody] RecipeSearchFilters searchFilters )
        {
            RecipeSearchResult searchResult = await _recipeService.Search( searchFilters );
            SearchResult<RecipePreviewDto> searchPreviewResult = new SearchResult<RecipePreviewDto>()
            {
                Result = await _recipePreviewDtoBuilder.Build( searchResult.Result )
            };

            return Ok( searchPreviewResult );
        }

        [HttpPost( "create" )]
        public async Task<IActionResult> CreateRecipe( [FromBody] CreateRecipeCommand createCommand )
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

        private bool TryGetUserIdFromQuery( out int userId )
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
