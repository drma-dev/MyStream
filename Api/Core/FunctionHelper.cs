using Api.Mediator;
using Microsoft.Azure.Functions.Worker.Http;
using SD.Shared.Core;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class FunctionHelper
    {
        public static I BuildRequestDelete<I, O>(this HttpRequestData req) where I : MediatorQuery<O>, new()
        {
            var obj = new I();

            obj.SetIds(req.GetUserId());
            obj.SetParameters(req.Url.Query);

            return obj;
        }

        /// <summary>
        /// Cria uma instancia de classe mediator do tipo Query
        /// </summary>
        /// <typeparam name="I">IN = classe mediator</typeparam>
        /// <typeparam name="O">OUT = classe de retorno</typeparam>
        /// <param name="req">HttpRequest</param>
        /// <returns></returns>
        public static I BuildRequestQuery<I, O>(this HttpRequestData req) where I : MediatorQuery<O>, new() where O : class
        {
            var obj = new I();

            obj.SetIds(req.GetUserId());
            obj.SetParameters(req.Url.Query);

            return obj;
        }

        /// <summary>
        /// Cria uma instancia de classe mediator do tipo Command
        /// </summary>
        /// <typeparam name="I">IN = classe mediator</typeparam>
        /// <param name="req">HttpRequest</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<I> BuildRequestCommand<I>(this HttpRequestData req, CancellationToken cancellationToken = default) where I : CosmosBase
        {
            var obj = await JsonSerializer.DeserializeAsync<I>(req.Body, null, cancellationToken);

            //bool.TryParse(req.Query["enable_seed"], out bool enable_seed);

            //if (!enable_seed)
            obj.SetIds(req.GetUserId());

            return obj;
        }
    }
}