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

        public Settings Settings { get; set; }

        protected bool IsLoading { get; set; }

        protected override void OnInitialized()
        {
            if (Settings == null)
            {
                Settings = StorageService.Local.GetItem<Settings>("Settings");

                if (Settings == null)
                {
                    Settings = new();
                }
            }
        }
    }
}