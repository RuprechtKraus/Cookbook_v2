using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.Builders;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Extensions;
using Cookbook_v2.Application.Helpers.Converters;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.EntitiesValidators;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.Search.Interfaces;
using Cookbook_v2.Domain.Search.RecipeModel;
using Cookbook_v2.Domain.UoW.Interfaces;
using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeSearchRepository _searchRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly RecipePreviewDtoBuilder _recipePreviewDtoBuilder;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IRecipeSearchRepository searchRepository,
            IUserRepository userRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IImageService imageService,
            RecipeDetailsDtoBuilder recipeDetailsDtoBuilder,
            RecipePreviewDtoBuilder recipePreviewDtoBuilder )
        {
            _recipeRepository = recipeRepository;
            _searchRepository = searchRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _recipePreviewDtoBuilder = recipePreviewDtoBuilder;
        }

        public async Task<IReadOnlyList<Recipe>> GetAll()
        {
            return await _recipeRepository.GetAll();
        }

        public async Task<Recipe> GetById( int id )
        {
            Recipe recipe = await _recipeRepository.GetById( id );

            return recipe ?? throw new KeyNotFoundException( "Recipe not found" );
        }

        public async Task<IReadOnlyList<Recipe>> GetByUserId( int id )
        {
            return await _recipeRepository.GetByUserId( id );
        }

        public async Task<RecipeSearchResult> Search( RecipeSearchFilters searchFilters )
        {
            return await _searchRepository.Search( searchFilters );
        }

        public async Task<Recipe> Create( CreateRecipeCommand createCommand )
        {
            string imageName = await CreateRecipeImage( createCommand.ImageBase64 );

            Recipe recipe = new Recipe(
                createCommand.UserId,
                createCommand.Title,
                createCommand.Description,
                createCommand.CookingTimeInMinutes,
                createCommand.ServingsCount,
                imageName,
                createCommand.RecipeSteps.ToRecipeStepList(),
                createCommand.IngredientsSections.ToIngredientsSectionList(),
                await CreateRecipeTagList( createCommand.Tags )
                );

            await IncrementUserRecipeCount( createCommand.UserId );
            await _recipeRepository.Add( recipe );
            await _unitOfWork.SaveAsync();

            return recipe;
        }

        public async Task DeleteById( int id )
        {
            Recipe recipe = await GetById( id );
            await Delete( recipe );
        }

        public async Task Delete( Recipe recipe )
        {
            if ( recipe == null )
            {
                throw new ArgumentNullException( nameof( recipe ) );
            }

            string imageName = recipe.ImageName;
            await _recipeRepository.Delete( recipe );
            await DecrementUserRecipeCount( recipe.UserId );
            await _unitOfWork.SaveAsync();
            _imageService.DeleteImage( imageName );
        }

        public async Task<RecipeLike> AddUserLike( int userId, int recipeId )
        {
            if ( await HasUserLike( userId, recipeId ) )
            {
                throw new EntityAlreadyExistsException( "Like already exists" );
            }

            RecipeLike recipeLike = new RecipeLike( userId, recipeId );

            await _recipeRepository.AddRecipeLike( recipeLike );
            await IncrementRecipeLikeCount( recipeId );
            await _unitOfWork.SaveAsync();

            return recipeLike;
        }

        public async Task DeleteUserLike( int userId, int recipeId )
        {
            RecipeLike recipeLike = await _recipeRepository.GetRecipeLike( userId, recipeId );

            if ( recipeLike == null )
            {
                throw new KeyNotFoundException( "Like doesn't exist" );
            }

            await _recipeRepository.DeleteRecipeLike( recipeLike );
            await DecrementRecipeLikeCount( recipeId );
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> HasUserLike( int userId, int recipeId )
        {
            return await _recipeRepository.GetRecipeLike( userId, recipeId ) != null;
        }

        public async Task<FavoriteRecipe> AddToUserFavorites( int userId, int recipeId )
        {
            if ( await IsFavoritedByUser( userId, recipeId ) )
            {
                throw new EntityAlreadyExistsException( "Favorite recipe already exists" );
            }

            FavoriteRecipe favoriteRecipe = new FavoriteRecipe( userId, recipeId );

            await _recipeRepository.AddFavoriteRecipe( favoriteRecipe );
            await IncrementRecipeFavoritedCount( recipeId );
            await _unitOfWork.SaveAsync();

            return favoriteRecipe;
        }

        public async Task RemoveFromUserFavorites( int userId, int recipeId )
        {
            FavoriteRecipe favoriteRecipe = await _recipeRepository
                .GetFavoriteRecipe( userId, recipeId );

            if ( favoriteRecipe == null )
            {
                throw new KeyNotFoundException( "Favorite recipe doesn't exist" );
            }

            await _recipeRepository.RemoveFavoriteRecipe( favoriteRecipe );
            await DecrementRecipeFavoritedCount( recipeId );
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsFavoritedByUser( int userId, int recipeId )
        {
            return await _recipeRepository.GetFavoriteRecipe( userId, recipeId ) != null;
        }

        private async Task<IReadOnlyList<RecipePreviewDto>> CreateRecipePreviewDtos(
            ICollection<Recipe> recipes )
        {
            List<RecipePreviewDto> previews = new List<RecipePreviewDto>();
            foreach ( Recipe recipe in recipes )
            {
                previews.Add( await _recipePreviewDtoBuilder.Build( recipe ) );
            }

            return previews;
        }

        private async Task<string> CreateRecipeImage( string? base64Image )
        {
            if ( base64Image != null )
            {
                return await _imageService.CreateAndSaveImageFromBase64( base64Image );
            }

            return "default_recipe_image.jpg";
        }

        private async Task<List<Tag>> CreateRecipeTagList( ICollection<string> tags )
        {
            List<Tag> recipeTags = tags.Select( x => new Tag( x ) ).ToList();
            List<Tag> result = ( await _tagRepository.GetAllByNames( tags.ToList() ) ).ToList();
            result.AddRange( recipeTags.Except( result ) );

            return result;
        }

        private async Task IncrementUserRecipeCount( int userId )
        {
            User user = await _userRepository.GetById( userId );
            user.ThrowNotFoundIfNull( "User not found" );
            user.RecipesCount++;
            await _userRepository.Update( user );
        }

        private async Task DecrementUserRecipeCount( int userId )
        {
            User user = await _userRepository.GetById( userId );
            user.ThrowNotFoundIfNull( "User not found" );
            user.RecipesCount--;
            await _userRepository.Update( user );
        }

        private async Task IncrementRecipeLikeCount( int recipeId )
        {
            Recipe recipe = await GetById( recipeId );
            recipe.TimesLiked++;
            await _recipeRepository.Update( recipe );
        }

        private async Task DecrementRecipeLikeCount( int recipeId )
        {
            Recipe recipe = await GetById( recipeId );
            recipe.TimesLiked--;
            await _recipeRepository.Update( recipe );
        }

        private async Task IncrementRecipeFavoritedCount( int recipeId )
        {
            Recipe recipe = await _recipeRepository.GetById( recipeId );
            recipe.TimesFavorited++;
            await _recipeRepository.Update( recipe );
        }

        private async Task DecrementRecipeFavoritedCount( int recipeId )
        {
            Recipe recipe = await _recipeRepository.GetById( recipeId );
            recipe.TimesFavorited--;
            await _recipeRepository.Update( recipe );
        }
    }
}
