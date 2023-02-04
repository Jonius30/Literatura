using System.Text.RegularExpressions;

namespace Literatura_API.Models
{
    public class SearchResult
    {
        /// <summary>
        /// Ids[0] primary id, e.g songnumber, or page in books, in case of TP/SB = year
        /// Ids[1] in case of TP/SB number
        /// Ids[2] in case of TP/SB page
        /// </summary>
        public List<int> Ids { get; set; } = new List<int>();

        public List<string> Content { get; set; } = new List<string>();

        public SearchResult(int primaryId, int? secondaryId, int? thirdId, List<string> singleContexts, string searchString) { 

            Ids.Add(primaryId);
            if (secondaryId != null)
                Ids.Add(secondaryId.Value);
            if(thirdId != null)
                Ids.Add(thirdId.Value);
            
            foreach (string singleContext in singleContexts)
            {
                Content.Add(Regex.Replace(singleContext, searchString, $"<b>{searchString}</b>", RegexOptions.IgnoreCase));
            }
        }


    }
}
