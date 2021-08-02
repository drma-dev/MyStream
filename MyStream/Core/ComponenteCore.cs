using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyStream.Modal;
using System.Net.Http;

namespace MyStream.Core
{
    public class VisibilityOptions
    {
        public bool Premium { get; set; }
        public bool Invalid { get; set; }
        public bool Loading { get; set; }
        public bool NoData { get; set; }

        public bool HasCustomVisibility => Premium || Invalid || Loading || NoData;
    }

    /// <summary>
    /// if you implement the OnInitializedAsync method, call 'await base.OnInitializedAsync();'
    /// </summary>
    public abstract class ComponenteCore : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncSessionStorageService SessionStorage { get; set; }

        [Inject]
        protected IToastService Toast { get; set; }

        private Settings _settings;

        public Settings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = LocalStorage.GetItem<Settings>("Settings");

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
                LocalStorage.SetItem("Settings", _settings);
            }
        }

        protected bool IsLoading { get; set; }
    }
}