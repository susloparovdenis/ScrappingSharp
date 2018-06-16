using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrappingSharp.Abstractions
{
    public interface ISearchEngineScrapper
    {
        Task<IList<Result>> Scrap(string query);
    }
}