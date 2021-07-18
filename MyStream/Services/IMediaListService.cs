using Blazored.SessionStorage;
using MyStream.Modal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public interface IMediaListService
    {
        Task<List<Media>> GetListMedia(HttpClient http, ISyncSessionStorageService storage, string region = "US", string language = "en-US", int page = 1);
    }
}