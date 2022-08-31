using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Helpers.Converters;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Repositories.Interfaces;

namespace Cookbook_v2.Application.Dtos.Builders
{
    public class RecipeDetailsDtoBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private RecipeDetailsDto? _result;

        public RecipeDetailsDtoBuilder( 
            IUserRepository userRepository, 
            IRecipeRepository recipeRepository )
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDetailsDtoBuilder> Build( int recipeId )
        {
            Recipe recipe = await _recipeRepository.GetById( recipeId );
            string authorUsername = ( await _userRepository.GetById( recipe.UserId )).Username;

            _result = new RecipeDetailsDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                TimesLiked = recipe.TimesLiked,
                TimesFavorited = recipe.TimesFavorited,
                CookingTimeInMinutes = recipe.CookingTimeInMinutes,
                ServingsCount = recipe.ServingsCount,
                ImageName = recipe.ImageName,
                AuthorUsername = authorUsername,
                Tags = recipe.Tags.Select( x => x.Name ).ToList(),
                RecipeSteps = recipe.RecipeSteps.ToDtoList(),
                IngredientsSections = recipe.IngredientsSections.ToDtoList()
            };

            return this;
        }

        public RecipeDetailsDto? GetResult()
        {
            return _result;
        }
    }
}
