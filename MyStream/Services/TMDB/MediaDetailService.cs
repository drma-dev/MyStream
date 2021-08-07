using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class MediaDetailService : ServiceBase
    {
        public async Task<MediaDetail> GetMedia(HttpClient http, ISyncSessionStorageService storage, Settings settings, string tmdb_id, TypeMedia? type = null)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "append_to_response", "videos" }
            };

            MediaDetail obj_return;

            if (!type.HasValue)
            {
                type = settings.TypeMedia;
            }

            if (type == TypeMedia.movie)
            {
                var item = await http.GetSession<MovieDetail>(storage, BaseUri + "movie/" + tmdb_id.ConfigureParameters(parameter));

                obj_return = new MediaDetail
                {
                    tmdb_id = item.id.ToString(),
                    title = item.title,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = item.release_date.GetDate(),
                    poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                    poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                    rating = item.vote_average,
                    runtime = item.runtime,
                    homepage = item.homepage,
                    Videos = item.videos.results.Select(s => new Video { id = s.id, key = s.key, name = s.name }).ToList(),
                    Genres = item.genres.Select(s => s.name).ToList()
                };
            }
            else
            {
                var item = await http.GetSession<TVDetail>(storage, BaseUri + "tv/" + tmdb_id.ConfigureParameters(parameter));

                obj_return = new MediaDetail
                {
                    tmdb_id = item.id.ToString(),
                    title = item.name,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = item.first_air_date.GetDate(),
                    poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                    poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                    rating = item.vote_average,
                    runtime = item.episode_run_time.FirstOrDefault(),
                    homepage = item.homepage,
                    Videos = item.videos.results.Select(s => new Video { id = s.id, key = s.key, name = s.name }).ToList(),
                    Genres = item.genres.Select(s => s.name).ToList()
                };
            }

            return obj_return;
        }
    }
}