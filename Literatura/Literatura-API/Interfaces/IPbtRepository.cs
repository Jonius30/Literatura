using Literatura_API.DTO;

namespace Literatura_API.Interfaces
{
    public interface IPbtRepository
    {
        public PbtDto GetPbt(int id);
        public SearchResultsDto SearchResults(string searchString);
    }
}
