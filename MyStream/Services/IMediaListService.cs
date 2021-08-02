using Blazored.SessionStorage;
using MyStream.Modal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public interface IMediaListService
    {
        Task<List<MediaDetail>> GetListMedia(HttpClient http, ISyncSessionStorageService storage,
            Settings settings, int page = 1, Dictionary<string, object> ExtraParameters = null);
    }
}