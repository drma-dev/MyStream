using Blazored.LocalStorage;
using MyStream.Core;
using MyStream.Modal;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public static class ProviderServide
    {
        private static List<Provider> _providers = new();

        public static List<Provider> GetAllProviders(HttpClient Http, ISyncLocalStorageService session)
        {
            if (!_providers.Any())
            {
                Task<List<Provider>> task;

                task = Http.Get<List<Provider>>(session, "Data/providers.json");

                task.Wait();

                _providers = task.Result;
            }

            return _providers;
        }

        public static void SaveProvider(ISyncLocalStorageService session, Provider provider)
        {
            var temp = _providers.Single(s => s.id == provider.id);

            temp = provider;

            session.SetItem("Data/providers.json", _providers);
        }
    }
}