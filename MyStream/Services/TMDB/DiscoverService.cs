using Microsoft.Extensions.Configuration;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.TMDB
{
    public class DiscoverService : IMediaListService
    {
        private readonly IConfiguration Configuration;

        public DiscoverService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public async Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings,
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, string> ExtraParameters = null)
        {
            var options = Configuration.GetSection(TmdbOptions.Section).Get<TmdbOptions>();

            var page = 0;

            if (ExtraParameters != null)
            {
                if (ExtraParameters.ContainsValue("popularity.desc"))
                {
                    ExtraParameters.TryAdd("vote_count.gte", "50"); //ignore low-rated movie
                }
                if (ExtraParameters.ContainsValue("primary_release_date.desc"))
                {
                    ExtraParameters.TryAdd("primary_release_date.lte", System.DateTime.Now.ToString("yyyy-MM-dd")); //only releasead
                }
                if (ExtraParameters.ContainsValue("vote_average.desc"))
                {
                    ExtraParameters.TryAdd("primary_release_date.gte", System.DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd")); //only recent releases
                    ExtraParameters.TryAdd("vote_count.gte", "1000"); //ignore low-rated movie
                    ExtraParameters.TryAdd("vote_average.gte", "7.4"); //only the best
                }
            }

            var parameter = new Dictionary<string, string>()
            {
                { "api_key", options.ApiKey },
                { "language", settings.Language.GetName(false) },
                { "watch_region", settings.Region.ToString() },
                { "page", page.ToString() }
            };

            if (ExtraParameters != null)
            {
                foreach (var item in ExtraParameters)
                {
                    parameter.TryAdd(item.Key, item.Value);
                }
            }

            if (type == MediaType.movie)
            {
                while (list_media.Count < qtd)
                {
                    page++;
                    parameter["page"] = page.ToString();
                    var result = await http.Get<MovieDiscover>(storage.Session, options.BaseUri + "discover/movie".ConfigureParameters(parameter));

                    foreach (var item in result.results)
                    {
                        if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                        list_media.Add(new MediaDetail
                        {
                            tmdb_id = item.id.ToString(),
                            title = item.title,
                            plot = string.IsNullOrEmpty(item.overview) ? Resources.App.NoPlot : item.overview,
                            release_date = item.release_date.GetDate(),
                            poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : options.SmallPosterPath + item.poster_path,
                            poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : options.LargePosterPath + item.poster_path,
                            rating = item.vote_count > 5 ? item.vote_average : 0,
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
                while (list_media.Count < qtd)
                {
                    page++;
                    parameter["page"] = page.ToString();
                    var result = await http.Get<TvDiscover>(storage.Session, options.BaseUri + "discover/tv".ConfigureParameters(parameter));

                    foreach (var item in result.results)
                    {
                        if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                        list_media.Add(new MediaDetail
                        {
                            tmdb_id = item.id.ToString(),
                            title = item.name,
                            plot = string.IsNullOrEmpty(item.overview) ? Resources.App.NoPlot : item.overview,
                            release_date = item.first_air_date.GetDate(),
                            poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : options.SmallPosterPath + item.poster_path,
                            poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : options.LargePosterPath + item.poster_path,
                            rating = item.vote_count > 10 ? item.vote_average : 0,
                            MediaType = MediaType.tv
                        });
                    }

                    if (result.total_results < qtd) break; //if there is less result than requested
                    if (page >= result.total_pages) break; //passed the last page
                    if (page > 10) break; //if it exceeds 10 calls, something is wrong
                }
            }
        }
    }
}