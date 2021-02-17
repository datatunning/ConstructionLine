using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            var searchByColor = SearchByColor(this._shirts.OrderBy(s => s.Color), options.Colors);
            var searchBySize = SearchBySize(this._shirts.OrderBy(s => s.Size), options.Sizes);

            Task.WhenAll(searchByColor, searchBySize);

            var selection = searchByColor.Result.Intersect(searchBySize.Result).ToArray();

            return new SearchResults
            {
                Shirts = selection.ToList(),
                ColorCounts = this.getColorSummary(selection),
                SizeCounts = this.getSizeSummary(selection)
            };
        }


        private Task<List<Shirt>> SearchByColor(IOrderedEnumerable<Shirt> shirts, List<Color> colors)
        {
            if (!colors.Any())
            {
                return Task.FromResult(shirts.ToList());
            }

            return Task.FromResult((from shirt in shirts
                where colors.Contains(shirt.Color) 
                select shirt).ToList());
        }

        private Task<List<Shirt>> SearchBySize(IOrderedEnumerable<Shirt> shirts, List<Size> sizes)
        {
            if (!sizes.Any())
            {
                return Task.FromResult(shirts.ToList());
            }
            
            return Task.FromResult((from shirt in shirts
                where sizes.Contains(shirt.Size)
                select shirt).ToList());
        }

        private List<ColorCount> getColorSummary(IEnumerable<Shirt> shirts)
        {
            var summary = shirts
                .GroupBy(x => x.Color)
                .Select(c => new ColorCount()
                {
                    Color = c.Key,
                    Count = c.Count()
                }).ToList();

            summary.AddRange(Color.All
                                          .Except(summary.Select(x => x.Color))
                                          .Select(c => new ColorCount {Color = c, Count = 0}));
            return summary;
        }

        private List<SizeCount> getSizeSummary(IEnumerable<Shirt> shirts)
        {
            var summary = shirts
                .GroupBy(x => x.Size)
                .Select(s => new SizeCount()
                {
                    Size = s.Key,
                    Count = s.Count()
                }).ToList();

            summary.AddRange(Size.All
                .Except(summary.Select(x => x.Size))
                .Select(s => new SizeCount { Size = s, Count = 0 }));
            return summary;
        }
    }
}