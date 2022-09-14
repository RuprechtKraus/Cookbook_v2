using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.Builders;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Search;
using Cookbook_v2.Domain.Search.RecipeModel;
using Cookbook_v2.Api.Extensions;

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
            User activeUser = Request.GetActiveUser();
            Recipe recipe = await _recipeService.GetById( id );
            RecipeDetailsDto details = await _recipeDetailsDtoBuilder.Build( recipe, activeUser );

            return Ok( details );
        }

        [HttpGet( "favorites" )]
        public async Task<IActionResult> GetFavorites()
        {
            User activeUser = Request.GetActiveUser();
            IReadOnlyList<Recipe> recipes =
                await _recipeService.GetFavoritesByUserId( activeUser.Id );
            IReadOnlyList<RecipePreviewDto> previewDtos =
                ( await _recipePreviewDtoBuilder.Build( recipes, activeUser ) ).ToList();

            return Ok( previewDtos );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "search" )]
        public async Task<IActionResult> Search( [FromBody] RecipeSearchFilters searchFilters )
        {
            User activeUser = Request.GetActiveUser();
            RecipeSearchResult searchResult = await _recipeService.Search( searchFilters );
            SearchResult<RecipePreviewDto> searchPreviewResult = new SearchResult<RecipePreviewDto>()
            {
                Result = await _recipePreviewDtoBuilder.Build( searchResult.Result, activeUser )
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

        [HttpPost( "{recipeId}/likes/add" )]
        public async Task<IActionResult> AddLike( int recipeId )
        {
            User activeUser = Request.GetActiveUser();
            await _recipeService.AddUserLike( activeUser.Id, recipeId );

            return Ok();
        }

        [HttpDelete( "{recipeId}/likes/remove" )]
        public async Task<IActionResult> RemoveLike( int recipeId )
        {
            User activeUser = Request.GetActiveUser();
            await _recipeService.DeleteUserLike( activeUser.Id, recipeId );

            return Ok();
        }

        [HttpPost( "{recipeId}/favorites/add" )]
        public async Task<IActionResult> AddToFavorites( int recipeId )
        {
            User activeUser = Request.GetActiveUser();
            await _recipeService.AddToUserFavorites( activeUser.Id, recipeId );

            return Ok();
        }

        [HttpDelete( "{recipeId}/favorites/remove" )]
        public async Task<IActionResult> RemoveFromFavorites( int recipeId )
        {
            User activeUser = Request.GetActiveUser();
            await _recipeService.RemoveFromUserFavorites( activeUser.Id, recipeId );

            return Ok();
        }
    }
}
