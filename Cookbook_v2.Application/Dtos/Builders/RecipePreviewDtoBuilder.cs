using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Application.Dtos.Builders
{
    public class RecipePreviewDtoBuilder
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;

        public RecipePreviewDtoBuilder(
            IUserService userService,
            IRecipeService recipeService )
        {
            _userService = userService;
            _recipeService = recipeService;
        }

        public async Task<RecipePreviewDto> Build( Recipe recipe, User activeUser )
        {
            if ( recipe == null )
            {
                throw new ArgumentNullException( nameof( recipe ) );
            }

            string authorUsername = ( await _userService.GetById( recipe.UserId ) ).Username;

            bool isLikedByActiveUser = activeUser != null && await _recipeService
                .HasUserLike( activeUser.Id, recipe.Id );

            bool isFavoritedByActiveUser = activeUser != null && await _recipeService
                .IsFavoritedByUser( activeUser.Id, recipe.Id );

            return new RecipePreviewDto
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
                IsLikedByActiveUser = isLikedByActiveUser,
                IsFavoritedByActiveUser = isFavoritedByActiveUser,
                Tags = recipe.Tags.Select( x => x.Name ).ToList()
            };
        }

        public async Task<ICollection<RecipePreviewDto>> Build(
            IEnumerable<Recipe> recipes, User activeUser )
        {
            ICollection<RecipePreviewDto> result = new List<RecipePreviewDto>();

            foreach ( Recipe recipe in recipes )
            {
                result.Add( await Build( recipe, activeUser ) );
            }

            return result;
        }
    }
}
