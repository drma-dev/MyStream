using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal.Enum;
using MyStream.Services.TMDB;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbExternalIdTvService : ServiceBase
    {
        public async Task<string> GetTmdbId(HttpClient http, ISyncSessionStorageService storage, string imdb_id, Language language = Language.ptBR)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", language.GetName() },
                { "external_source", "imdb_id" }
            };

            var result = await http.GetSession<FindByImdb>(storage, BaseUri + "find/" + imdb_id.ConfigureParameters(parameter));

            return result.tv_results.FirstOrDefault().id.ToString();
        }
    }
}