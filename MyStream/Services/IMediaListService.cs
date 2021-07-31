using Blazored.SessionStorage;
using MyStream.Modal;
using MyStream.Modal.Enum;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public interface IMediaListService
    {
        Task<List<Media>> GetListMedia(HttpClient http, ISyncSessionStorageService storage, TypeMedia type, Region region = Region.BR, Language language = Language.ptBR, int page = 1);
    }
}