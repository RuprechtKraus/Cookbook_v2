using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Converters;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.UoW.Interfaces;

namespace Cookbook_v2.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(
            IRecipeRepository recipeRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork )
        {
            _recipeRepository = recipeRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Recipe> GetById( int id )
        {
            Recipe recipe = await _recipeRepository.GetById( id );
            return recipe ?? throw new KeyNotFoundException( "Recipe not found" );
        }

        public async Task<IReadOnlyList<Recipe>> GetByUsername( string username )
        {
            return await _recipeRepository.GetByUsername( username );
        }

        public async Task<Recipe> Create( CreateRecipeCommand createCommand )
        {
            List<Tag> tags = new List<Tag>();

            foreach ( var tagName in createCommand.Tags )
            {
                Tag tag = await _tagRepository.GetByName( tagName );

                if ( tag != null )
                {
                    tags.Add( tag );
                }
                else
                {
                    tags.Add( new Tag( tagName ) );
                }
            }

            Recipe recipe = new Recipe(
                createCommand.UserId,
                createCommand.Title,
                createCommand.Description,
                createCommand.CookingTimeInMinutes,
                createCommand.ServingsCount,
                "No image",
                createCommand.RecipeSteps.Select( x => x.ToRecipeStep() ).ToList(),
                createCommand.IngredientsSections.Select( x => x.ToRecipeIngredientsSection() ).ToList(),
                tags
                );

            await _recipeRepository.Add( recipe );
            await _unitOfWork.SaveAsync();

            return recipe;
        }

        public async Task DeleteById( int id )
        {
            throw new MissingMethodException( "Method not implemented" );
        }

        public Task Delete( Recipe recipe )
        {
            throw new MissingMethodException( "Method not implemented" );
        }

        public async Task AddLike( int userId, int recipeId )
        {
            throw new MissingMethodException( "Method not implemented" );
        }

        public async Task DeleteLike( int userId, int recipeId )
        {
            throw new MissingMethodException( "Method not implemented" );
        }
    }
}
