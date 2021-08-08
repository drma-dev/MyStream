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
    public class UpcomingService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "region", settings.Region.ToString() },
                { "language", settings.Language.GetName() },
                { "page", page }
            };

            var list_return = new List<MediaDetail>();

            if (type == TypeMedia.movie)
            {
                var result = await http.Get<MovieUpcoming>(storage.Session, BaseUri + "movie/upcoming".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    //if (item.vote_count < 100) continue;
                    //if (string.IsNullOrEmpty(item.poster_path)) continue;

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.id.ToString(),
                        title = item.title,
                        plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = item.release_date.GetDate(),
                        poster_path_small = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w92/" + item.poster_path,
                        poster_path_large = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w185/" + item.poster_path,
                        rating = item.vote_count > 10 ? item.vote_average : 0
                    });
                }
            }
            else if (type == TypeMedia.tv)
            {
                throw new NotImplementedException();
            }

            return list_return;
        }
    }
}