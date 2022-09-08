using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.MessageContracts.CategoryModel;
using Cookbook_v2.Api.Converters;
using Cookbook_v2.Domain.Repositories.Interfaces;

namespace Cookbook_v2.Api.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController( ICategoryRepository categoryRepository )
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CategoryDto> categories = ( await _categoryRepository.GetAll() )
                .Select( x => x.ToDto() );

            return Ok( categories );
        }
    }
}
