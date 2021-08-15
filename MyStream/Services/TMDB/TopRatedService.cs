using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class TopRatedService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "region", settings.Region.ToString() },
                { "language", settings.Language.GetName() },
                { "page", page }
            };

            var list_return = new List<MediaDetail>();

            if (type == TypeMedia.movie)
            {
                var result = await http.Get<MovieTopRated>(storage.Local, BaseUri + "movie/top_rated".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < 500) continue;
                    if (string.IsNullOrEmpty(item.poster_path)) continue;

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.id.ToString(),
                        title = item.title,
                        plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = item.release_date.GetDate(),
                        poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                        poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                        rating = item.vote_average,
                        TypeMedia = TypeMedia.movie
                    });
                }
            }
            else if (type == TypeMedia.tv)
            {
                var result = await http.Get<TVTopRated>(storage.Local, BaseUri + "tv/top_rated".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < 500) continue;
                    if (string.IsNullOrEmpty(item.poster_path)) continue;

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.id.ToString(),
                        title = item.name,
                        plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = item.first_air_date.GetDate(),
                        poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                        poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                        rating = item.vote_average,
                        TypeMedia = TypeMedia.tv
                    });
                }
            }

            return list_return;
        }
    }
}