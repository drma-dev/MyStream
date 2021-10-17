using Microsoft.Extensions.Configuration;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class UpcomingService : IMediaListService
    {
        private readonly IConfiguration Configuration;

        public UpcomingService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public async Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings,
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, string> ExtraParameters = null)
        {
            var options = Configuration.GetSection(TmdbOptions.Section).Get<TmdbOptions>();
            var page = 0;

            var parameter = new Dictionary<string, string>()
            {
                { "api_key", options.ApiKey },
                { "region", settings.Region.ToString() },
                { "language", settings.Language.GetName(false) },
                { "page", page.ToString() }
            };

            if (type == MediaType.movie)
            {
                while (list_media.Count < qtd)
                {
                    page++;
                    parameter["page"] = page.ToString();
                    var result = await http.Get<MovieUpcoming>(storage.Session, options.BaseUri + "movie/upcoming".ConfigureParameters(parameter));

                    foreach (var item in result.results)
                    {
                        if (string.IsNullOrEmpty(item.poster_path)) continue;

                        list_media.Add(new MediaDetail
                        {
                            tmdb_id = item.id.ToString(),
                            title = item.title,
                            plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                            release_date = item.release_date.GetDate(),
                            poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : options.SmallPosterPath + item.poster_path,
                            poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : options.LargePosterPath + item.poster_path,
                            rating = item.vote_count > 10 ? item.vote_average : 0,
                            MediaType = MediaType.movie
                        });
                    }

                    if (result.total_results < qtd) break; //if there is less result than requested
                    if (page >= result.total_pages) break; //passed the last page
                    if (page > 10) break; //if it exceeds 10 calls, something is wrong
                }
            }
            else if (type == MediaType.tv)
            {
                throw new NotImplementedException();
            }
        }
    }
}