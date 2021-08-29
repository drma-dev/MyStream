using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class ListService : ServiceBase, IMediaListService
    {
        public async Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings,
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, object> ExtraParameters = null)
        {
            if (ExtraParameters == null) throw new ArgumentNullException(nameof(ExtraParameters));

            if (qtd > 9)
            {
                return;
            }

            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "page", 1 },
                { "sort_by", "original_order.asc" }
            };

            if (ExtraParameters != null)
            {
                foreach (var item in ExtraParameters)
                {
                    parameter.Add(item.Key, item.Value);
                }
            }

            var result = await http.GetNew<CustomListNew>(storage.Local, BaseUriNew + "list/" + ExtraParameters["list_id"].ToString().ConfigureParameters(parameter));

            foreach (var item in result.results)
            {
                var tv = item.media_type == "tv";

                result.comments.TryGetProperty($"{(tv ? "tv" : "movie")}:{item.id}", out JsonElement value);

                list_media.Add(new MediaDetail
                {
                    tmdb_id = item.id.ToString(),
                    title = tv ? item.name : item.title,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = tv ? item.first_air_date.GetDate() : item.release_date.GetDate(),
                    poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                    poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                    rating = item.vote_average,
                    MediaType = tv ? MediaType.tv : MediaType.movie,
                    comments = value.GetString()
                });
            }
        }
    }
}