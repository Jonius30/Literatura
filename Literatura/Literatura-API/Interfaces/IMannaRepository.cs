using Literatura_API.DTO;

namespace Literatura_API.Interfaces
{
    public interface IMannaRepository
    {
        public MannaDto GetManna(int day, int month);
        public MannaDto GetNextManna(int day, int month);
        public MannaDto GetPreviousManna(int day, int month);
        public SearchResultsDto SearchResults(string searchString);
    }
}
