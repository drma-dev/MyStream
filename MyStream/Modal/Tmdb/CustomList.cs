﻿using System.Collections.Generic;

namespace MyStream.Modal.Tmdb
{
    public class ItemCustomList
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string media_type { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public object backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
    }

    public class CustomList
    {
        public string created_by { get; set; }
        public string description { get; set; }
        public int favorite_count { get; set; }
        public string id { get; set; }
        public List<ItemCustomList> items { get; set; }
        public int item_count { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
    }
}