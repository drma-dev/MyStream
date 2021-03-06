using Blazored.Toast.Services;
using Microsoft.Extensions.Logging;
using MyStream.Helper;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream.Core
{
    public static class NotificationCore
    {
        public static async Task ProcessResponse(this HttpResponseMessage response, IToastService toast, string msgSuccess = null, string msgInfo = null)
        {
            var msg = await response.Content.ReadAsStringAsync();

            if ((short)response.StatusCode >= 100 && (short)response.StatusCode <= 199) //Respostas de informação
            {
                //do nothing
            }
            else if ((short)response.StatusCode >= 200 && (short)response.StatusCode <= 299) //Respostas de sucesso
            {
                if (!string.IsNullOrEmpty(msgSuccess)) toast.ShowSuccess("", msgSuccess);
                if (!string.IsNullOrEmpty(msgInfo)) toast.ShowInfo("", msgInfo);
            }
            else if ((short)response.StatusCode >= 300 && (short)response.StatusCode <= 399) //Redirecionamentos
            {
                throw new NotificationException(msg);
            }
            else if ((short)response.StatusCode >= 400 && (short)response.StatusCode <= 499) //Erros do cliente
            {
                throw new NotificationException(msg);
            }
            else //Erros do servidor (above 500)
            {
                throw new Exception(msg);
            }
        }

        public static void ProcessException(this Exception ex, IToastService toast, ILogger logger = default)
        {
            if (ex is NotificationException)
            {
                logger.LogWarning(ex, null);
                toast.ShowWarning("", ex.Message);
            }
            else
            {
                logger.LogError(ex, null);
                toast.ShowError("", ex.Message);
            }
        }
    }
}