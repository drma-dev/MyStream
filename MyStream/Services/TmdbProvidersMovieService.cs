using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbProvidersMovieService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<List<Provider>> GetListMedia(HttpClient http, ISyncSessionStorageService storage, string region = "US", string language = "en-US")
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", language },
                { "watch_region", region }
            };

            var result = await http.GetSession<TMDB_AllProviders>(storage, BaseUri + "watch/providers/movie".ConfigureParameters(parameter));
            var details = await http.GetFromJsonAsync<TMDB_AllProviders>("Data/providers.json");

            var temp = new List<Provider>();
            foreach (var item in result.results)
            {
                //if (item.vote_count < 100) continue;
                //if (string.IsNullOrEmpty(item.poster_path)) continue;

                var detail = details.results.FirstOrDefault(f => f.provider_id == item.provider_id);

                temp.Add(new Provider
                {
                    id = item.provider_id.ToString(),
                    name = item.provider_name,
                    description = detail?.provider_desciption,
                    link = detail?.provider_link,
                    logo_path = item.logo_path
                });
            }
            return temp;
        }
    }
}