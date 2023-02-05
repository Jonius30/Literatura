using System.Text.RegularExpressions;

namespace Literatura_API.Models
{
    public class SearchResult
    {
        /// <summary>
        /// Ids[0] primary id, e.g songnumber, or page in books, in case of TP/SB = id
        /// Ids[1] in case of TP/SB year
        /// Ids[2] in case of TP/SB number
        /// Id[3] in case of  TP/SB page
        /// </summary>
        public List<int> Ids { get; set; } = new List<int>();

        public List<string> Content { get; set; } = new List<string>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryId">Songs: Songnumber, TP/SB/manna id</param>
        /// <param name="secondaryId">TP/SB year, Manna day</param>
        /// <param name="thirdId">TP/SB month, Manna month</param>
        /// <param name="fourthId">TP/SB page </param>
        /// <param name="singleContexts"></param>
        /// <param name="searchString"></param>
        public SearchResult(int primaryId, int? secondaryId, int? thirdId,int? fourthId, List<string> singleContexts, string searchString) { 

            Ids.Add(primaryId);
            if (secondaryId != null)
                Ids.Add(secondaryId.Value);
            if(thirdId != null)
                Ids.Add(thirdId.Value);
            if(fourthId != null)
                Ids.Add(fourthId.Value);
            foreach (string singleContext in singleContexts)
            {
                Content.Add(Regex.Replace(singleContext, searchString, $"<b>{searchString}</b>", RegexOptions.IgnoreCase));
            }
        }


    }
}
