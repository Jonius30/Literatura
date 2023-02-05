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
            sqlCommand.Parameters.AddWithValue("@id", id);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var zLiederDto = new ZLiederDto();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    zLiederDto.Id = reader.GetInt32(0);
                    zLiederDto.Content = ConvertQuotes(reader.GetString(1));
                }
                MySqlConnection.Close();
            }
            else
            {
                MySqlConnection.Close();
                zLiederDto = GetZLieder(1);
            }
            return zLiederDto;
        }

        public ZLiederDto GetNextZLieder(int id)
        {
            int next = id + 1;
            MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM ZLieder", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (id + 1 > reader.GetInt32(0))
                    next = 1;
            }
            MySqlConnection.Close();
            return GetZLieder(next);
        }

        public ZLiederDto GetPreviousZLieder(int id)
        {
            int next = id - 1;
            if (next == 0)
            {
                MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM ZLieder", MySqlConnection);
                MySqlConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    next = reader.GetInt32(0);
                }
                MySqlConnection.Close();
            }
            return GetZLieder(next);
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM zlieder WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("ZLieder");
            while (reader.Read())
            {
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), null, null, null, ResultForOneMySqlReaderResult(ConvertQuotes(reader.GetString(1)), searchString, 40), searchString));
            }
            return searchResultsDto;

        }
    }
}
