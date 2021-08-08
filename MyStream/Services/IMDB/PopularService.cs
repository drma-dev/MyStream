using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Imdb;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.IMDB
{
    public class PopularService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
                {
                    { "apiKey", ApiKey }
                };

            var list_return = new List<MediaDetail>();

            if (type == TypeMedia.movie)
            {
                var result = await http.Get<MostPopularData>(storage.Session, BaseUri + "MostPopularMovies".ConfigureParameters(parameter));

                foreach (var item in result.Items)
                {
                    if (item.IMDbRatingCount == "0") continue; //ignore low-rated movie
                    //if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.Id,
                        title = item.Title,
                        //plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = new System.DateTime(int.Parse(item.Year), 1, 1),
                        poster_path_small = item.Image.Replace("/original/", "/95x136/"),
                        //poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_185 + item.poster_path,
                        rating = string.IsNullOrEmpty(item.IMDbRating) ? 0 : double.Parse(item.IMDbRating)
                    });
                }
            }
            else if (type== TypeMedia.tv)
            {
                var result = await http.Get<MostPopularData>(storage.Session, BaseUri + "MostPopularTVs".ConfigureParameters(parameter));

                foreach (var item in result.Items)
                {
                    if (item.IMDbRatingCount == "0") continue; //ignore low-rated movie
                    //if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.Id,
                        title = item.Title,
                        //plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = new System.DateTime(int.Parse(item.Year), 1, 1),
                        poster_path_small = item.Image.Replace("/original/", "/95x136/"),
                        //poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_185 + item.poster_path,
                        rating = string.IsNullOrEmpty(item.IMDbRating) ? 0 : double.Parse(item.IMDbRating)
                    });
                }
            }

            return list_return;
        }
    }
}