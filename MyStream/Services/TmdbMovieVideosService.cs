using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbMovieVideosService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<TMDB_Videos> GetVideos(HttpClient http, ISyncSessionStorageService storage, string id, string language)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                //{ "language", language } //TODO tem que ser o código completo
            };

            return await http.GetSession<TMDB_Videos>(storage, BaseUri + $"movie/{id}/videos".ConfigureParameters(parameter));
        }
    }
}