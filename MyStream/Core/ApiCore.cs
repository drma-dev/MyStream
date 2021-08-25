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
        public async static Task<T> Get<T>(this HttpClient http, ISyncLocalStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                var response = await http.GetAsync(request_uri);

                if (!response.IsSuccessStatusCode) throw new NotificationException(response);

                storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
            }

            return storage.GetItem<T>(request_uri);
        }

        public async static Task<T> Get<T>(this HttpClient http, ISyncSessionStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                var response = await http.GetAsync(request_uri);

                if (!response.IsSuccessStatusCode) throw new NotificationException(response);

                storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
            }

            return storage.GetItem<T>(request_uri);
        }

        public async static Task<T> Get<T>(this HttpClient http, string request_uri) where T : class
        {
            var response = await http.GetAsync(request_uri);

            if (!response.IsSuccessStatusCode) throw new NotificationException(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async static Task<T> GetNew<T>(this HttpClient http, ISyncLocalStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, request_uri);

                request.Headers.Add("authorization", "Bearer <<access_token>>");
                request.Headers.TryAddWithoutValidation("content-type", "application/json;charset=utf-8");

                var response = await http.SendAsync(request);

                if (!response.IsSuccessStatusCode) throw new NotificationException(response);

                storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
            }

            return storage.GetItem<T>(request_uri);
        }

        public async static Task<T> GetNew<T>(this HttpClient http, ISyncSessionStorageService storage, string request_uri) where T : class
        {
            if (!storage.ContainKey(request_uri))
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, request_uri);

                request.Headers.Add("authorization", "Bearer <<access_token>>");
                request.Headers.TryAddWithoutValidation("content-type", "application/json;charset=utf-8");

                var response = await http.SendAsync(request);

                if (!response.IsSuccessStatusCode) throw new NotificationException(response);

                storage.SetItem(request_uri, await response.Content.ReadFromJsonAsync<T>());
            }

            return storage.GetItem<T>(request_uri);
        }
    }
}