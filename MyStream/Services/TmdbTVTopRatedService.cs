﻿using Blazored.SessionStorage;
using MyStream.Core;
using MyStream.Helper;
using MyStream.Modal;
using MyStream.Modal.Tmdb;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class TmdbTVTopRatedService : IMediaListService
    {
        private const string BaseUri = "https://api.themoviedb.org/3/";
        private const string ApiKey = "745ee705ec04f3be69ba3e449609f430";

        public async Task<List<Media>> GetListMedia(HttpClient http, ISyncSessionStorageService storage, string region = "US", string language = "en-US", int page = 1)
        {
            var parameter = new Dictionary<string, object>()
            {
                { "api_key", ApiKey },
                { "region", region },
                { "language", language },
                { "page", page }
            };

            var result = await http.GetSession<TVTopRated>(storage, BaseUri + "tv/top_rated".ConfigureParameters(parameter));

            var temp = new List<Media>();
            foreach (var item in result.results)
            {
                if (item.vote_count < 500) continue;
                if (string.IsNullOrEmpty(item.poster_path)) continue;

                temp.Add(new Media
                {
                    id = item.id.ToString(),
                    title = item.name,
                    plot = string.IsNullOrEmpty(item.overview) ? "No plot found" : item.overview,
                    release_date = item.first_air_date.GetDate(),
                    poster_path_92 = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w92/" + item.poster_path,
                    poster_path_185 = string.IsNullOrEmpty(item.poster_path) ? null : "https://www.themoviedb.org/t/p/w185/" + item.poster_path,
                    rating = item.vote_average
                });
            }
            return temp;
        }
    }
}