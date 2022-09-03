using System.Threading.Tasks;

namespace Cookbook_v2.Domain.Search.Interfaces
{
    public interface ISearchRepository<TSearchFilters, TSearchResult>
        where TSearchFilters : class
        where TSearchResult : class
    {
        Task<TSearchResult> Search( TSearchFilters searchFilters );
    }
}
