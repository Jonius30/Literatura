using Literatura_API.DTO;
using Literatura_API.Interfaces;
using Literatura_API.Models;
using MySqlConnector;

namespace Literatura_API.Repository
{
    public class MannaRepository : BaseRepository, IMannaRepository
    {
        public MannaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public MannaDto GetManna(int day, int month)
        {
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM manna WHERE day = @day AND month = @month Limit 1", MySqlConnection);
            sqlCommand.Parameters.AddWithValue("@day", day);
            sqlCommand.Parameters.AddWithValue("@month", month);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            var mannaDto = new MannaDto();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    mannaDto.Id = reader.GetInt32(0);
                    mannaDto.Day = reader.GetInt32(2);
                    mannaDto.Month = reader.GetInt32(3);
                    mannaDto.Content = reader.GetString(1);
                }
                MySqlConnection.Close();
            }
            else
            {
                MySqlConnection.Close();
                mannaDto = GetManna(1, 1);
            }
            return mannaDto;
        }

        public MannaDto GetNextManna(int day, int month)
        {
            int nextDay = 1;
            int nextMonth = 1;
            if(!(day == 31 && month == 12)) 
            {
                MySqlCommand maxSqlCommand = new MySqlCommand("SELECT MAX(day) FROM manna WHERE month = @month", MySqlConnection);
                maxSqlCommand.Parameters.AddWithValue("@month", month);
                MySqlConnection.Open();
                var reader = maxSqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (day >= reader.GetInt32(0))
                    {
                        nextDay = 1;
                        nextMonth = month + 1;
                        if (nextMonth == 13)
                            nextMonth = 1;
                    }
                    else
                        nextDay = day + 1;
                }
                MySqlConnection.Close(); 
            }
            return GetManna(nextDay, nextMonth);
        }

        public MannaDto GetPreviousManna(int day, int month)
        {
            int nextDay = 31;
            int nextMonth = 12;
            if (!(day == 1 && month == 1))
            {
                nextDay = day - 1;
                if (nextDay == 0)
                {
                    MySqlCommand maxSqlCommand = new MySqlCommand("SELECT MAX(day) FROM manna WHERE month = @month", MySqlConnection);
                    maxSqlCommand.Parameters.AddWithValue("@month", month - 1);
                    MySqlConnection.Open();
                    var reader = maxSqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        nextDay = reader.GetInt32(0);
                        nextMonth = month - 1;
                    }
                    MySqlConnection.Close();
                }
                else
                    nextMonth = month;
               
            }
            return GetManna(nextDay, nextMonth);
        }

        public SearchResultsDto SearchResults(string searchString)
        {
            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM manna WHERE wpis LIKE '%{searchString}%'", MySqlConnection);
            MySqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            SearchResultsDto searchResultsDto = new SearchResultsDto("Manna");
            while (reader.Read())
            {
                searchResultsDto.SearchResults.Add(new SearchResult(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), null, ResultForOneMySqlReaderResult(reader.GetString(1), searchString, 40), searchString));
            }
            return searchResultsDto;

        }
    }
}
