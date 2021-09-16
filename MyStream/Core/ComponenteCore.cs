using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
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

        protected bool IsLoading { get; set; }
    }
}