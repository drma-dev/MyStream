using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class WatchProvidersService : ServiceBase
    {
        public async Task<MediaProviders> GetProviders(HttpClient http, ISyncSessionStorageService storage, Settings settings, string tmdb_id)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey }
            };

            if (settings.TypeMedia == TypeMedia.movie)
            {
                return await http.GetSession<MediaProviders>(storage, BaseUri + $"movie/{tmdb_id}/watch/providers".ConfigureParameters(parameter));
            }
            else //tv
            {
                return await http.GetSession<MediaProviders>(storage, BaseUri + $"tv/{tmdb_id}/watch/providers".ConfigureParameters(parameter));
            }
        }
    }
}