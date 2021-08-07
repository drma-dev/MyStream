using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class ListService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, ISyncSessionStorageService storage,
            Settings settings, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
            };

            if (ExtraParameters != null)
            {
                foreach (var item in ExtraParameters)
                {
                    parameter.Add(item.Key, item.Value);
                }
            }

            var list_return = new List<MediaDetail>();

            var result = await http.GetSession<CustomList>(storage, BaseUri + "list/" + ExtraParameters["list_id"].ToString().ConfigureParameters(parameter));

            foreach (var item in result.items)
            {
                list_return.Add(new MediaDetail
                {
                    tmdb_id = item.id.ToString(),
                    title = item.title,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = item.release_date.GetDate(),
                    poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                    poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                    rating = item.vote_average
                });
            }

            return list_return;
        }
    }
}