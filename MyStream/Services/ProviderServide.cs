using Blazored.LocalStorage;
using MyStream.Core;
using MyStream.Modal;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Services
{
    public class ProviderServide
    {
        private List<Provider> _providers = new();

        public async Task<List<Provider>> GetAllProviders(HttpClient Http, ISyncLocalStorageService session)
        {
            if (!_providers.Any())
            {
                _providers = await Http.Get<List<Provider>>(session, "Data/providers.json");
            }

            return _providers;
        }

        public void SaveProvider(ISyncLocalStorageService session, Provider provider)
        {
            var temp = _providers.Single(s => s.id == provider.id);

            temp = provider;

            session.SetItem("Data/providers.json", _providers);
        }
    }
}