using System.Collections.Generic;
using System.Diagnostics;

namespace ScrappingSharp
{
    [DebuggerDisplay("{Title} [{Sources] ")]
    public class Result
    {
        private readonly HashSet<string> _sources = new HashSet<string>();

        public Result(string title, string url, string source)
        {
            Title = title;
            Url = url;
            _sources.Add(source);
        }

        public string Title { get; }
        public string Url { get; }

        public IReadOnlyCollection<string> Sources => _sources;

        public void AddSources(IEnumerable<string> sources)
        {
            _sources.UnionWith(sources);
        }
    }
}