using Microsoft.Extensions.Configuration;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbExternalIdTvService
    {
        private readonly IConfiguration Configuration;

        public TmdbExternalIdTvService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public async Task<string> GetTmdbId(HttpClient http, IStorageService storage, Settings settings, string imdb_id)
        {
            var options = Configuration.GetSection(TmdbOptions.Section).Get<TmdbOptions>();

            var parameter = new Dictionary<string, string>()
            {
                { "api_key", options.ApiKey },
                { "language", settings.Language.GetName() },
                { "external_source", "imdb_id" }
            };

            var result = await http.Get<FindByImdb>(storage.Session, options.BaseUri + "find/" + imdb_id.ConfigureParameters(parameter));

            return result.tv_results.FirstOrDefault().id.ToString();
        }
    }
}