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
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
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

            var list_return = new List<MediaDetail>();

            var min_votes = 5;
            //if (ExtraParameters != null && ExtraParameters.ContainsValue("vote_average.desc"))
            //{
            //    min_votes = 10;
            //}

            if (type == TypeMedia.movie)
            {
                var result = await http.Get<MovieDiscover>(storage.Session, BaseUri + "discover/movie".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < min_votes) continue; //ignore low-rated movie
                    if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.id.ToString(),
                        title = item.title,
                        plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = item.release_date.GetDate(),
                        poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                        poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                        rating = item.vote_average
                    });
                }
            }
            else if (type == TypeMedia.tv)
            {
                var result = await http.Get<TvDiscover>(storage.Session, BaseUri + "discover/tv".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < min_votes) continue; //ignore low-rated movie
                    if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.id.ToString(),
                        title = item.name,
                        plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = item.first_air_date.GetDate(),
                        poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_small + item.poster_path,
                        poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_large + item.poster_path,
                        rating = item.vote_average
                    });
                }
            }

            return list_return;
        }
    }
}