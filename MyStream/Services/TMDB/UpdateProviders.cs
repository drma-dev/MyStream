using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class UpdateProviders : ServiceBase
    {
        public static async Task UpdateAllProvider(HttpClient http, ISyncSessionStorageService storage)
        {
            var result = new List<Provider>();
            var details = await http.GetFromJsonAsync<List<Provider>>("Data/providers.json");

            foreach (var region in EnumHelper.GetList(typeof(Region)))
            {
                var parameter = new Dictionary<string, object>()
                {
                    { "api_key", ApiKey },
                    { "language", Language.enUS.GetName() },
                    { "watch_region", region.ValueObject.ToString() }
                };

                var movies = await http.GetSession<TMDB_AllProviders>(storage, BaseUri + "watch/providers/movie".ConfigureParameters(parameter));

                AddProvider(result, movies.results, details, region.ValueObject.ToString(), TypeMedia.movie.ToString());

                var tvs = await http.GetSession<TMDB_AllProviders>(storage, BaseUri + "watch/providers/tv".ConfigureParameters(parameter));

                AddProvider(result, tvs.results, details, region.ValueObject.ToString(), TypeMedia.tv.ToString());
            }

            storage.SetItem("AllProviders", result);
        }

        private static void AddProvider(List<Provider> final_list, List<ProviderBase> new_providers, List<Provider> current_providers, string region, string type)
        {
            foreach (var item in new_providers)
            {
                var detail = current_providers.FirstOrDefault(f => f.id == item.provider_id.ToString());
                var new_item = final_list.FirstOrDefault(f => f.id == item.provider_id.ToString());

                if (new_item == null)
                {
                    final_list.Add(new Provider
                    {
                        id = item.provider_id.ToString(),
                        name = item.provider_name,
                        description = detail?.description,
                        link = detail?.link,
                        logo_path = item.logo_path,
                        regions = new List<string> { region },
                        types = new List<string> { type }
                    });
                }
                else
                {
                    if (!new_item.regions.Any(a => a == region))
                    {
                        new_item.regions.Add(region);
                    }

                    if (!new_item.types.Any(a => a == type))
                    {
                        new_item.types.Add(type);
                    }
                }
            }
        }
    }
}