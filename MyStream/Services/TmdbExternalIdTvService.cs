using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Tmdb;
using MyStream.Services.TMDB;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbExternalIdTvService : ServiceBase
    {
        public async Task<string> GetTmdbId(HttpClient http, ISyncSessionStorageService storage, Settings settings, string imdb_id)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "external_source", "imdb_id" }
            };

            var result = await http.GetSession<FindByImdb>(storage, BaseUri + "find/" + imdb_id.ConfigureParameters(parameter));

            return result.tv_results.FirstOrDefault().id.ToString();
        }
    }
}