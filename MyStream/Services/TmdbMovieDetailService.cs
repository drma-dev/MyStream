using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using MyStream.Services.TMDB;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbMovieDetailService: ServiceBase
    {
        public async Task<Media> GetMedia(HttpClient http, ISyncSessionStorageService storage, string id, Language language = Language.ptBR)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", language.GetName() },
                { "append_to_response", "videos" }
            };

            var item = await http.GetSession<MovieDetail>(storage, BaseUri + "movie/" + id.ConfigureParameters(parameter));

            return new Media
            {
                id = item.id.ToString(),
                title = item.title,
                plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                release_date = item.release_date.GetDate(),
                poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                rating = item.vote_average,
                runtime = item.runtime,
                homepage = item.homepage,
                Videos = item.videos.results.Select(s => new Video { id = s.id, key = s.key, name = s.name }).ToList(),
                Genres = item.genres.Select(s=>s.name).ToList()
            };
        }
    }
}