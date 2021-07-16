using System.Collections.Generic;

namespace MyStream.Modal
{
    public class Result_Video
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    public class TMDB_Videos
    {
        public int id { get; set; }
        public List<Result_Video> results { get; set; }
    }
}