using System.Collections.Generic;

namespace MyStream.Modal.Tmdb
{
    public class RegionProvider
    {
        public string iso_3166_1 { get; set; }
        public string english_name { get; set; }
        public string native_name { get; set; }
    }

    public class Region_Provider
    {
        public List<RegionProvider> results { get; set; }
    }
}