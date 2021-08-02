﻿using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Enum;
using MyStream.Modal.Imdb;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services.IMDB
{
    public class UpcomingService : ServiceBase, IMediaListService
    {
        public async Task<List<MediaDetail>> GetListMedia(HttpClient http, ISyncSessionStorageService storage,
            Settings settings, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
                {
                    { "apiKey", ApiKey }
                };

            var list_return = new List<MediaDetail>();

            if (settings.TypeMedia == TypeMedia.movie)
            {
                var result = await http.GetSession<NewMovieData>(storage, BaseUri + "ComingSoon".ConfigureParameters(parameter));

                foreach (var item in result.Items)
                {
                    //if (item.vote_count < 100) continue; //ignore low-rated movie
                    //if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new MediaDetail
                    {
                        tmdb_id = item.Id,
                        title = item.Title,
                        //plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                        release_date = DateTime.Parse(item.ReleaseState),
                        poster_path_small = item.Image.Replace("/original/", "/95x136/"),
                        //poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : poster_path_185 + item.poster_path,
                        rating = string.IsNullOrEmpty(item.IMDbRating) ? 0 : double.Parse(item.IMDbRating)
                    });
                }
            }
            else if (settings.TypeMedia == TypeMedia.tv)
            {
                throw new NotImplementedException();
            }

            return list_return;
        }
    }
}