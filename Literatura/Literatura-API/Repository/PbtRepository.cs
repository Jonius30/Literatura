using Literatura_API.DTO;
using Literatura_API.Interfaces;
using Literatura_API.Models;
using MySqlConnector;
using System.Data.Common;

namespace Literatura_API.Repository
{
    public class PbtRepository : BaseRepository, IPbtRepository
    {
        public PbtRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public PbtDto GetPbt(int id)
        {
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM pbt WHERE id = @id Limit 1", MySqlConnection);
            sqlCommand.Parameters.AddWithValue("id", id);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var pbtDTO = new PbtDto();
            while (reader.Read())
            {
                pbtDTO.Id = reader.GetInt32(0);
                pbtDTO.Content = reader.GetString(1);
            }
            MySqlConnection.Close();
            return pbtDTO;
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM pbt WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("Pbt");
            while (reader.Read())
            { 
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), null, null, ResultForOneMySqlReaderResult(reader.GetString(1), searchString,40), searchString));
            }
            return searchResultsDto;

        }
    }
}
