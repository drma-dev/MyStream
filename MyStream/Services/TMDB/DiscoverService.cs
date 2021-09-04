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
    public class DiscoverService : ServiceBase, IMediaListService
    {
        public async Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings,
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, object> ExtraParameters = null)
        {
            var page = 0;

            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "language", settings.Language.GetName() },
                { "watch_region", settings.Region.ToString() },
                { "page", page }
            };

            if (ExtraParameters != null)
            {
                foreach (var item in ExtraParameters)
                {
                    parameter.Add(item.Key, item.Value);
                }
            }

            var min_votes = 5; //popularity.desc
            if (ExtraParameters != null && ExtraParameters.ContainsValue("vote_average.desc"))
            {
                min_votes = 30;
            }
            if (ExtraParameters != null && ExtraParameters.ContainsValue("release_date.desc"))
            {
                min_votes = 0;
            }

            if (type == MediaType.movie)
            {
                while (list_media.Count < qtd)
                {
                    page++;
                    parameter["page"] = page;
                    var result = await http.Get<MovieDiscover>(storage.Session, BaseUri + "discover/movie".ConfigureParameters(parameter));

                    foreach (var item in result.results)
                    {
                        if (item.vote_count < min_votes) continue; //ignore low-rated movie
                        if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster
                        if (ExtraParameters != null && ExtraParameters.ContainsValue("vote_average.desc") && item.vote_average < 7) continue; //only the best

                        list_media.Add(new MediaDetail
                        {
                            tmdb_id = item.id.ToString(),
                            title = item.title,
                            plot = string.IsNullOrEmpty(item.overview) ? Resources.App.NoPlot : item.overview,
                            release_date = item.release_date.GetDate(),
                            poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                            poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
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
                    parameter["page"] = page;
                    var result = await http.Get<TvDiscover>(storage.Session, BaseUri + "discover/tv".ConfigureParameters(parameter));

                    foreach (var item in result.results)
                    {
                        if (item.vote_count < min_votes) continue; //ignore low-rated movie
                        if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster
                        if (ExtraParameters != null && ExtraParameters.ContainsValue("vote_average.desc") && item.vote_average < 7) continue; //only the best

                        list_media.Add(new MediaDetail
                        {
                            tmdb_id = item.id.ToString(),
                            title = item.name,
                            plot = string.IsNullOrEmpty(item.overview) ? Resources.App.NoPlot : item.overview,
                            release_date = item.first_air_date.GetDate(),
                            poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                            poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
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