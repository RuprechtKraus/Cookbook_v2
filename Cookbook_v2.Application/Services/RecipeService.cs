using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.Builders;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Extensions;
using Cookbook_v2.Application.Helpers.Converters;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Application.Settings;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.UoW.Interfaces;
using Microsoft.Extensions.Options;

namespace Cookbook_v2.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly RecipeDetailsDtoBuilder _recipeDetailsDtoBuilder;
        private readonly RecipePreviewDtoBuilder _recipePreviewDtoBuilder;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IUserRepository userRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IImageService imageService,
            RecipeDetailsDtoBuilder recipeDetailsDtoBuilder,
            RecipePreviewDtoBuilder recipePreviewDtoBuilder )
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _recipeDetailsDtoBuilder = recipeDetailsDtoBuilder;
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

        public async Task<RecipeDetailsDto> GetRecipeDetailsDtoById( int id )
        {
            RecipeDetailsDtoBuilder builder = new RecipeDetailsDtoBuilder( 
                _userRepository, _recipeRepository );
            return await builder.Build( id );
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
            Recipe recipe = await _recipeRepository.GetById( id );
            if ( recipe == null )
            {
                throw new KeyNotFoundException( "Recipe not found" );
            }
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

        public async Task AddLike( int userId, int recipeId )
        {
            throw new MissingMethodException( "Method not implemented" );
        }

        public async Task DeleteLike( int userId, int recipeId )
        {
            throw new MissingMethodException( "Method not implemented" );
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
            List<Tag> tagsInDb = ( await _tagRepository.GetAll() ).ToList();
            List<Tag> recipeTags = tags.Select( x => new Tag( x ) ).ToList();
            List<Tag> result = tagsInDb.Intersect( recipeTags ).ToList();
            result.AddRange( recipeTags.Except( result ) );
            return result;
        }

        private async Task IncrementUserRecipeCount( int userId )
        {
            User user = await _userRepository.GetById( userId );
            user.RecipesCount++;
            await _userRepository.Update( user );
        }

        private async Task DecrementUserRecipeCount( int userId )
        {
            User user = await _userRepository.GetById( userId );
            user.RecipesCount--;
            await _userRepository.Update( user );
        }
    }
}
