﻿using System;
using System.Collections.Generic;

namespace MyStream.Modal
{
    public class Media
    {
        public string id { get; set; }
        public string title { get; set; }
        public string plot { get; set; }
        public DateTime? release_date { get; set; }
        public string poster_path_92 { get; set; }
        public string poster_path_185 { get; set; }
        public double rating { get; set; }
        public int runtime { get; set; }
        public string homepage { get; set; }

        public List<Video> Videos { get; set; } = new();
        public List<string> Genres { get; set; } = new();
    }

    public class Video
    {
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
    }
}