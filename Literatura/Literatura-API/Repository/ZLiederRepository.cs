using Literatura_API.DTO;
using Literatura_API.Interfaces;
using Literatura_API.Models;
using MySqlConnector;

namespace Literatura_API.Repository
{
    public class ZLiederRepository : BaseRepository, IZLiederRepository
    {
        public ZLiederRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public ZLiederDto GetZLieder(int id)
        {
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM zlieder WHERE id = @id Limit 1", MySqlConnection);
            sqlCommand.Parameters.AddWithValue("id", id);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var zLiederDto = new ZLiederDto();
            while (reader.Read())
            {
                zLiederDto.Id = reader.GetInt32(0);
                zLiederDto.Content = ConvertQuotes(reader.GetString(1));
            }
            MySqlConnection.Close();
            return zLiederDto;
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM zlieder WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("ZLieder");
            while (reader.Read())
            {
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), null, null, ResultForOneMySqlReaderResult(ConvertQuotes(reader.GetString(1)), searchString, 40), searchString));
            }
            return searchResultsDto;

        }
    }
}
