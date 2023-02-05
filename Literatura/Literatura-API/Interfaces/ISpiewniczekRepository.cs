using Literatura_API.DTO;

namespace Literatura_API.Interfaces
{
    public interface ISpiewniczekRepository
    {
        public SpiewniczekDto GetSpiewniczek(int id);
        public SearchResultsDto SearchResults(string searchString);
        public SpiewniczekDto GetNextSpiewniczek(int id);
        public SpiewniczekDto GetPreviousSpiewniczek(int id);
    }
}
