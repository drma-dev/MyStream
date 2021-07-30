﻿using System.Collections.Generic;

namespace MyStream.Modal
{
    public class Provider
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string logo_path { get; set; }
        public List<string> regions { get; set; } = new();
        public List<string> types { get; set; } = new();
    }
}