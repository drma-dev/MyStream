using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Core
{
    public class VisibilityOptions
    {
        public bool Loading { get; set; }
        public bool NoData { get; set; }

        public bool HasCustomVisibility => Loading || NoData;
    }

    public static class ComponenteUtils
    {
        public static string IdUser { get; set; }
        public static bool IsAuthenticated { get; set; }

        public static string BaseApi(this HttpClient http) => http.BaseAddress.ToString().Contains("localhost") ? "http://localhost:7071/api/" : http.BaseAddress.ToString() + "api/";
    }

    public abstract class ComponenteCore<TClass> : ComponentBase where TClass : class
    {
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        public IStorageService StorageService { get; set; }

        [Inject]
        protected IToastService Toast { get; set; }

        [Inject]
        protected ILogger<TClass> Logger { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }

        protected bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(ComponenteUtils.IdUser))
                {
                    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;

                    ComponenteUtils.IsAuthenticated = user.Identity != null && user.Identity.IsAuthenticated;
                    ComponenteUtils.IdUser = user.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                }
            }
            catch (Exception ex)
            {
                ex.ProcessException(Toast, Logger);
            }
        }
    }
}