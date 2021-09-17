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
        Task PopulateListMedia(HttpClient http, IStorageService storage, Settings settings, 
            HashSet<MediaDetail> list_media, MediaType type, int qtd = 9, Dictionary<string, string> ExtraParameters = null);
    }
}