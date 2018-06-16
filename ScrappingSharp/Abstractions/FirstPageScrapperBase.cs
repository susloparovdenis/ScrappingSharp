using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ScrappingSharp.Abstractions
{
    public abstract class FirstPageScrapperBase : ISearchEngineScrapper
    {
        private static readonly IConfiguration Configuration = AngleSharp.Configuration.Default.WithDefaultLoader();
        private readonly IBrowsingContext _browsingContext = BrowsingContext.New(Configuration);

        /// <summary>
        ///     Should contain a query placeholder in format of: https://search.yahoo.com/search?p={0}
        /// </summary>
        protected abstract string Url { get; }

        /// <summary>
        ///     Jquery selector
        /// </summary>
        protected abstract string EntrySelector { get; }


        private string Source => GetType().Name.ToLower();

        public async Task<IList<Result>> Scrap(string query)
        {
            try
            {
                var address = string.Format(Url, query);
                var document = await _browsingContext.OpenAsync(address);
                var elements = document.QuerySelectorAll(EntrySelector);
                return elements.Select(ResultFromEntry).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{Source} failed: {e}");
                return Array.Empty<Result>();
            }
        }

        private Result ResultFromEntry(IElement a)
        {
            return new Result(GetTitle(a), GetUrl(a), Source);
        }

        protected abstract string GetTitle(IElement element);

        protected abstract string GetUrl(IElement element);
    }
}