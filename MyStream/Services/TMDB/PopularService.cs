﻿using Blazored.SessionStorage;
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
    public class PopularService : ServiceBase, IMediaListService
    {
        public async Task<List<Media>> GetListMedia(HttpClient http, ISyncSessionStorageService storage, 
            TypeMedia type, Region region = Region.BR, Language language = Language.ptBR, int page = 1, Dictionary<string, object> ExtraParameters = null)
        {
            var parameter = new Dictionary<string, object>()
                {
                    { "api_key", ApiKey },
                    { "language", language.GetName() },
                    { "page", page }
                };

            var list_return = new List<Media>();

            if (type == TypeMedia.movie)
            {
                var result = await http.GetSession<MoviePopular>(storage, BaseUri + "movie/popular".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < 100) continue; //ignore low-rated movie
                    if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new Media
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
                var result = await http.GetSession<TVPopular>(storage, BaseUri + "tv/popular".ConfigureParameters(parameter));

                foreach (var item in result.results)
                {
                    if (item.vote_count < 100) continue; //ignore low-rated movie
                    if (string.IsNullOrEmpty(item.poster_path)) continue; //ignore empty poster

                    list_return.Add(new Media
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