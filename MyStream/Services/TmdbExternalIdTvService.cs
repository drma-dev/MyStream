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
        public async Task<string> GetTmdbId(HttpClient http, IStorageService storage, Settings settings, string imdb_id)
        {
            var parameter = new Dictionary<string, string>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "external_source", "imdb_id" }
            };

            var result = await http.Get<FindByImdb>(storage.Session, BaseUri + "find/" + imdb_id.ConfigureParameters(parameter));

            return result.tv_results.FirstOrDefault().id.ToString();
        }
    }
}