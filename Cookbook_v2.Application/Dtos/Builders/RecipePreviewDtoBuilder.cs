using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Repositories.Interfaces;

namespace Cookbook_v2.Application.Dtos.Builders
{
    public class RecipePreviewDtoBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private RecipePreviewDto? _result;

        public RecipePreviewDtoBuilder( 
            IUserRepository userRepository, 
            IRecipeRepository recipeRepository )
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipePreviewDtoBuilder> Build( int recipeId )
        {
            Recipe recipe = await _recipeRepository.GetById( recipeId );
            string authorUsername = ( await _userRepository.GetById( recipe.UserId ) ).Username;

            _result = new RecipePreviewDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                TimesLiked = recipe.TimesLiked,
                TimesFavorited = recipe.TimesFavorited,
                CookingTimeInMinutes = recipe.CookingTimeInMinutes,
                ServingsCount = recipe.ServingsCount,
                ImageName = recipe.ImageName,
                AuthorUsername = authorUsername
            };

            return this;
        }

        public RecipePreviewDto? GetResult()
        {
            return _result;
        }
    }
}
