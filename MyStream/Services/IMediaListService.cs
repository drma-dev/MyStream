using MyStream.Core;
using MyStream.Modal;
using MyStream.Modal.Enum;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public interface IMediaListService
    {
        Task<List<MediaDetail>> GetListMedia(HttpClient http, IStorageService storage, Settings settings, TypeMedia type, int page = 1, Dictionary<string, object> ExtraParameters = null);
    }
}