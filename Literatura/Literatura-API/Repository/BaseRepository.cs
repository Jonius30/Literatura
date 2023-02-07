using Literatura_API.Models;
using MySqlConnector;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Literatura_API.Repository
{
    public abstract class BaseRepository
    {
        public MySqlConnection MySqlConnection { get; set; }
      
        public BaseRepository(IConfiguration configuration) 
        {
            MySqlConnection = new MySqlConnection(configuration.GetConnectionString("MySQL"));
        }

        private IEnumerable<int> GetAllIndexes(string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            foreach (Match match in Regex.Matches(source, matchString))
            {
                yield return match.Index;
            }
        }
        
        public string ConvertQuotes(string content)
        {
            content = content.Replace(@"\""", @"""");
            content = content.Replace(@"\'", @"'");
            return content;
        }
        private string HtmlToSpecial(string content)
        {
            content = content.Replace("<br>", "¼");
            content = content.Replace("<b>", "ᾠ");
            content = content.Replace("</b>", "ᾡ");
            Regex x = new Regex("(<img)(.*?)(\">)");
            content = x.Replace(content, "");
            return content;
        }
        private string SpecialToHtml(string content)
        {
            content = content.Replace("¼", "<br>");
            content = content.Replace("ᾠ", "<b>");
            content = content.Replace("ᾡ", "</b>");
            return content;
        }
        private string PrepareToSearch(string content)
        {
            content = content.Replace("<b>", "");
            content = content.Replace("</b>", "");
            content = content.Replace("<B>", "");
            content = content.Replace("</B>", "");
            return content;
        }

        private List<string> ResultsWithContextInOneRow(List<int> positionOfMatches, string searchString, string CompleteContentForOneRow, int amountOfContextChars)
        {
            List<string> RowResultsContext = new List<string>();
            for (int i = 0; i < positionOfMatches.Count(); i++)
            {
                int start;
                int lenght;
                if (positionOfMatches[i] < amountOfContextChars)
                    start = 0;
                else
                    start = positionOfMatches[i] - amountOfContextChars;


                if (i != positionOfMatches.Count() - 1)
                {
                    bool moreInContext = false;
                    do
                    {
                        if (positionOfMatches[i] + searchString.Length + amountOfContextChars*2 <= positionOfMatches[i + 1])
                            moreInContext = false;
                        else
                        {
                            i++;
                            moreInContext = true;
                        }
                    } while (moreInContext && i != positionOfMatches.Count() - 1);
                }
                if (positionOfMatches[i] + searchString.Length + amountOfContextChars >= CompleteContentForOneRow.Length)
                    lenght = CompleteContentForOneRow.Length - start;
                else
                    lenght = positionOfMatches[i] + searchString.Length + amountOfContextChars - start;
                RowResultsContext.Add(SpecialToHtml(CompleteContentForOneRow.Substring(start, lenght)));
            }
            return RowResultsContext;
        }

        protected List<string> ResultForOneMySqlReaderResult(string content, string searchString, int amountOfContextChars)
        {
            content = PrepareToSearch(content);
            content = HtmlToSpecial(content);
            List<int> positionOfMatches = (GetAllIndexes(content.ToLower(), searchString.ToLower())).ToList();
            return ResultsWithContextInOneRow(positionOfMatches, searchString, content, amountOfContextChars);
        }
    }
}
