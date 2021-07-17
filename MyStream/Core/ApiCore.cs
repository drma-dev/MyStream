using Blazored.LocalStorage;
using Blazored.SessionStorage;
using MyStream.Helper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyStream.Core
{
    public static class ApiCore
    {
        //public async static Task<T> GetLocal<T>(this HttpClient http, ISyncLocalStorageService storage, string request_uri) where T : class
        //{
        //    if (!storage.ContainKey(request_uri))
        //    {
        //        var response = await http.GetAsync(request_uri);

        //        if (!response.IsSuccessStatusCode) throw new NotificationException(response);

        //        storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
        //    }

        //    return storage.GetItem<T>(request_uri);
        //}

        public async static Task<T> GetSession<T>(this HttpClient http, ISyncSessionStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                var response = await http.GetAsync(request_uri);

                if (!response.IsSuccessStatusCode) throw new NotificationException(response);

                storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
            }

            return storage.GetItem<T>(request_uri);
        }

        public async static Task<T> GetLocalRapid<T>(this HttpClient http, ISyncLocalStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, request_uri))
                {
                    requestMessage.Headers.Add("x-rapidapi-key", "36af8735e3msh39423dcd3a94067p1975bdjsn4536c4c2ed8a");
                    requestMessage.Headers.Add("x-rapidapi-host", "streaming-availability.p.rapidapi.com");

                    var response = await http.SendAsync(requestMessage);

                    storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
                }
            }

            return storage.GetItem<T>(request_uri);
        }

        public async static Task<T> GetSessionRapid<T>(this HttpClient http, ISyncSessionStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, request_uri))
                {
                    requestMessage.Headers.Add("x-rapidapi-key", "36af8735e3msh39423dcd3a94067p1975bdjsn4536c4c2ed8a");
                    requestMessage.Headers.Add("x-rapidapi-host", "streaming-availability.p.rapidapi.com");

                    var response = await http.SendAsync(requestMessage);

                    storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
                }
            }

            return storage.GetItem<T>(request_uri);
        }
    }
}