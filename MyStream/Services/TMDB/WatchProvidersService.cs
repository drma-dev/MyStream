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
        public async Task<MediaProviders> GetProviders(HttpClient http, IStorageService storage, Settings settings, string tmdb_id, MediaType type)
        {
            var parameter = new Dictionary<string, string>()
            {
                { "api_key", ApiKey }
            };

            if (type == MediaType.movie)
            {
                return await http.Get<MediaProviders>(BaseUri + $"movie/{tmdb_id}/watch/providers".ConfigureParameters(parameter));
            }
            else //tv
            {
                return await http.Get<MediaProviders>(BaseUri + $"tv/{tmdb_id}/watch/providers".ConfigureParameters(parameter));
            }
        }
    }
}