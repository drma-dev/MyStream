using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class ListService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            if (ExtraParameters == null) throw new ArgumentNullException(nameof(ExtraParameters));

            if (page > 1)
            {
                return new List<MediaDetail>();
            }

            //https://developers.themoviedb.org/4/list/get-list
            //TODO: get comments (rewards years)

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

            var result = await http.Get<CustomList>(storage.Local, BaseUri + "list/" + ExtraParameters["list_id"].ToString().ConfigureParameters(parameter));

            foreach (var item in result.items)
            {
                var tv = item.media_type == "tv";

                list_return.Add(new MediaDetail
                {
                    tmdb_id = item.id.ToString(),
                    title = tv ? item.name : item.title,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = tv ? item.first_air_date.GetDate() : item.release_date.GetDate(),
                    poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                    poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                    rating = item.vote_average,
                    TypeMedia = tv ? Modal.Enum.TypeMedia.tv : Modal.Enum.TypeMedia.movie
                });
            }

            return list_return;
        }
    }
}