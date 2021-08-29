using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Imdb;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.IMDB
{
    public class TopRatedService : ServiceBase, IMediaListService
    {
        public async Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings,
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
                {
                    { "apiKey", ApiKey }
                };

            if (type == MediaType.movie)
            {
                var result = await http.Get<Top250Data>(storage.Local, BaseUri + "Top250Movies".ConfigureParameters(parameter)); //bring 250 records

                foreach (var item in result.Items)
                {
                    //if (item.vote_count < 100) continue; //ignore low-rated movie
                    //if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_media.Add(new MediaDetail
                    {
                        tmdb_id = item.Id,
                        title = item.Title,
                        //plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = new System.DateTime(int.Parse(item.Year), 1, 1),
                        poster_path_small = item.Image.Replace("/original/", "/95x136/"),
                        //poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_185 + item.poster_path,
                        rating = string.IsNullOrEmpty(item.IMDbRating) ? 0 : double.Parse(item.IMDbRating, CultureInfo.InvariantCulture),
                        MediaType = MediaType.movie
                    });
                }
            }
            else if (type == MediaType.tv)
            {
                var result = await http.Get<Top250Data>(storage.Local, BaseUri + "Top250TVs".ConfigureParameters(parameter)); //bring 250 records

                foreach (var item in result.Items)
                {
                    //if (item.vote_count < 100) continue; //ignore low-rated movie
                    //if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_media.Add(new MediaDetail
                    {
                        tmdb_id = item.Id,
                        title = item.Title,
                        //plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = new System.DateTime(int.Parse(item.Year), 1, 1),
                        poster_path_small = item.Image.Replace("/original/", "/95x136/"),
                        //poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_185 + item.poster_path,
                        rating = string.IsNullOrEmpty(item.IMDbRating) ? 0 : double.Parse(item.IMDbRating, CultureInfo.InvariantCulture),
                        MediaType = MediaType.tv
                    });
                }
            }
        }
    }
}