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

namespace MyStream.Services
{
    public class TmdbTVDetailService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<Media> GetMedia(HttpClient http, ISyncSessionStorageService storage, string id, Language language = Language.ptBR)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", language.GetName() },
                { "append_to_response", "videos" }
            };

            var item = await http.GetSession<TVDetail>(storage, BaseUri + "tv/" + id.ConfigureParameters(parameter));

            return new Media
            {
                id = item.id.ToString(),
                title = item.name,
                plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                release_date = item.first_air_date.GetDate(),
                poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w92/" + item.poster_path,
                poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w185/" + item.poster_path,
                rating = item.vote_average,
                runtime = item.episode_run_time.FirstOrDefault(),
                homepage = item.homepage,
                Videos = item.videos.results.Select(s => new Video { id = s.id, key = s.key, name = s.name }).ToList(),
                Genres = item.genres.Select(s=>s.name).ToList()
            };
        }
    }
}