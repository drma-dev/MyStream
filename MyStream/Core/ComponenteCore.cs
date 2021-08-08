using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyStream.Modal;
using System.Net.Http;

namespace MyStream.Core
{
    public class VisibilityOptions
    {
        public bool Loading { get; set; }
        public bool NoData { get; set; }

        public bool HasCustomVisibility => Loading || NoData;
    }

    public abstract class ComponenteCore : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        public IStorageService StorageService { get; set; }

        [Inject]
        protected IToastService Toast { get; set; }

        private Settings _settings;

        public Settings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = StorageService.Local.GetItem<Settings>("Settings");

                    if (_settings == null)
                    {
                        _settings = new Settings();
                    }
                }
                return _settings;
            }
            set
            {
                _settings = value;
                StorageService.Local.SetItem("Settings", _settings);
            }
        }

        protected bool IsLoading { get; set; }
    }
}