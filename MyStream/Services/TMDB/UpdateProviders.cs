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
        public async Task UpdateAllProvider(HttpClient http, IStorageService storage)
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

                var movies = await http.Get<TMDB_AllProviders>(storage.Session, BaseUri + "watch/providers/movie".ConfigureParameters(parameter));

                AddProvider(result, movies.results, details, (Region)region.ValueObject, MediaType.movie);

                var tvs = await http.Get<TMDB_AllProviders>(storage.Session, BaseUri + "watch/providers/tv".ConfigureParameters(parameter));

                AddProvider(result, tvs.results, details, (Region)region.ValueObject, MediaType.tv);
            }

            storage.Session.SetItem("AllProviders", result);
        }

        private static void AddProvider(List<Provider> final_list, List<ProviderBase> new_providers, List<Provider> current_providers, Region region, MediaType type)
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
                        priority = item.display_priority,
                        description = detail?.description,
                        link = detail?.link,
                        logo_path = item.logo_path,
                        regions = new List<Region> { region },
                        types = new List<MediaType> { type }
                    });
                }
                else
                {
                    new_item.id = item.provider_id.ToString();
                    new_item.name = item.provider_name;
                    new_item.priority = item.display_priority;
                    new_item.description = detail?.description;
                    new_item.link = detail?.link;
                    new_item.logo_path = item.logo_path;
                    new_item.head_language = detail?.head_language;
                    new_item.plans = detail?.plans;

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