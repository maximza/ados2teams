namespace ADOS2Teams.ADOSSearch
{
    using System.Threading.Tasks;
    using Microsoft.Bot.Schema.Teams;

    public interface ISearchHandler
    {
        /// <summary>
        /// Gets the search result async
        /// </summary>
        /// <param name="query">The invoke query object</param>
        /// <returns></returns>
        Task<MessagingExtensionResult> GetSearchResultAsync(string query);
    }
}
