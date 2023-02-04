using Literatura_API.Models;

namespace Literatura_API.DTO
{
    public class SearchResultsDto
    {
        public string ModulName { get; set; }

        public List<SearchResult> SearchResults { get; set; } = new List<SearchResult>();

        public SearchResultsDto(string modul) {
              ModulName = modul;
        }
    }
}
