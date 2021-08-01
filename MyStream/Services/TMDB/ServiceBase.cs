namespace MyStream.Services.TMDB
{
    public abstract class ServiceBase
    {
        protected const string BaseUri = "https://api.themoviedb.org/3/";
        protected const string ApiKey = "745ee705ec04f3be69ba3e449609f430";
        
        protected const string poster_path_small = "https://www.themoviedb.org/t/p/w154/";
        protected const string poster_path_large = "https://www.themoviedb.org/t/p/w300/";
    }
}