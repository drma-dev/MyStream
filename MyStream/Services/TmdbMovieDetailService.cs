using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbMovieDetailService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<Media> GetMedia(HttpClient http, ISyncSessionStorageService storage, string id, string language = "en-US")
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", language },
                { "append_to_response", "videos" }
            };

            var item = await http.GetSession<MovieDetail>(storage, BaseUri + "movie/" + id.ConfigureParameters(parameter));

            return new Media
            {
                id = item.id.ToString(),
                title = item.title,
                plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                release_date = item.release_date.GetDate(),
                poster_path_92 = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w92/" + item.poster_path,
                poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w185/" + item.poster_path,
                rating = item.vote_average,
                runtime = item.runtime,
                homepage = item.homepage,
                Videos = item.videos.results.Select(s => new Video { id = s.id, key = s.key, name = s.name }).ToList(),
                Genres = item.genres.Select(s=>s.name).ToList()
            };
        }
    }
}