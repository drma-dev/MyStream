using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbTVWatchProvidersService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<TMDB_Providers> GetProviders(HttpClient http, ISyncSessionStorageService storage, string id)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey }
            };

            return await http.GetSession<TMDB_Providers>(storage, BaseUri + $"tv/{id}/watch/providers".ConfigureParameters(parameter));
        }
    }
}