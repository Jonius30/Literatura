using Literatura_API.DTO;
using Literatura_API.Interfaces;
using Literatura_API.Models;
using MySqlConnector;

namespace Literatura_API.Repository
{
    public class SpiewniczekRepository : BaseRepository, ISpiewniczekRepository
    {
        public SpiewniczekRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public SpiewniczekDto GetSpiewniczek(int id)
        {
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM spiewniczek WHERE id = @id Limit 1", MySqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var spiewniczekDto = new SpiewniczekDto();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    spiewniczekDto.Id = reader.GetInt32(0);
                    spiewniczekDto.Content = reader.GetString(1);
                }
                MySqlConnection.Close();
            }
            else
            {
                MySqlConnection.Close();
                spiewniczekDto = GetSpiewniczek(1);
            }
            return spiewniczekDto;
        }

        public SpiewniczekDto GetNextSpiewniczek(int id)
        {
            int next = id + 1;
            MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM Spiewniczek", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (id + 1 > reader.GetInt32(0))
                    next = 1;
            }
            MySqlConnection.Close();
            return GetSpiewniczek(next);
        }

        public SpiewniczekDto GetPreviousSpiewniczek(int id)
        {
            int next = id - 1;
            if (next == 0)
            {
                MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM Spiewniczek", MySqlConnection);
                MySqlConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    next = reader.GetInt32(0);
                }
                MySqlConnection.Close();
            }
            return GetSpiewniczek(next);
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM spiewniczek WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("Spiewniczek");
            while (reader.Read())
            {
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), null, null,null, ResultForOneMySqlReaderResult(reader.GetString(1), searchString, 40), searchString));
            }
            return searchResultsDto;

        }
    }
}
