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
            sqlCommand.Parameters.AddWithValue("@id", id);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var pbtDTO = new PbtDto();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pbtDTO.Id = reader.GetInt32(0);
                    pbtDTO.Content = reader.GetString(1);
                }
                MySqlConnection.Close();
            }
            else
            {
                MySqlConnection.Close();
                pbtDTO = GetPbt(1);
            }
            return pbtDTO;
        }

        public PbtDto GetNextPbt(int id)
        {
            int next = id+1;
            MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM pbt", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (id + 1 > reader.GetInt32(0))
                    next = 1;
            }
            MySqlConnection.Close();
            return GetPbt(next);
        }

        public PbtDto GetPreviousPbt(int id)
        {
            int next = id - 1;
            if(next == 0)
            {
                MySqlCommand sqlCommand = new MySqlCommand("SELECT MAX(id) FROM pbt", MySqlConnection);
                MySqlConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    next = reader.GetInt32(0);    
                }
                MySqlConnection.Close();
            }
            return GetPbt(next);
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM pbt WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("Pbt");
            while (reader.Read())
            { 
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), null, null, null, ResultForOneMySqlReaderResult(reader.GetString(1), searchString,40), searchString));
            }
            return searchResultsDto;

        }
    }
}
