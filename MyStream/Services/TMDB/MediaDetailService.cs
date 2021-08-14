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
        public async Task<MediaDetail> GetMedia(HttpClient http, IStorageService storage, Settings settings, string tmdb_id, TypeMedia type)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "append_to_response", "videos" }
            };

            MediaDetail obj_return;

            if (type == TypeMedia.movie)
            {
                var item = await http.Get<MovieDetail>(storage.Session, BaseUri + "movie/" + tmdb_id.ConfigureParameters(parameter));

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
                    Genres = item.genres.Select(s => s.name).ToList(),
                    TypeMedia = TypeMedia.movie
                };
            }
            else
            {
                var item = await http.Get<TVDetail>(storage.Session, BaseUri + "tv/" + tmdb_id.ConfigureParameters(parameter));

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
                    Genres = item.genres.Select(s => s.name).ToList(),
                    TypeMedia = TypeMedia.tv
                };
            }

            return obj_return;
        }
    }
}