using Literatura_API.DTO;

namespace Literatura_API.Interfaces
{
    public interface IZLiederRepository
    {
        public ZLiederDto GetZLieder(int id);
        public SearchResultsDto SearchResults(string searchString);
        public ZLiederDto GetNextZLieder(int id);
        public ZLiederDto GetPreviousZLieder(int id);
    }
}
