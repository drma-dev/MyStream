using Api.Core;
using Api.Mediator.Command.Principal;
using Api.Mediator.Queries.Principal;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SD.Shared.Model;
using System.Net;
using System.Threading.Tasks;

namespace VerusDate.Api.Function
{
    public class PrincipalFunction
    {
        private readonly IMediator _mediator;

        public PrincipalFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("PrincipalGet")]
        public async Task<HttpResponseData> Get(
           [HttpTrigger(AuthorizationLevel.Anonymous, FunctionMethod.GET, Route = "Principal/Get")] HttpRequestData req,
           FunctionContext executionContext)
        {
            //using var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, executionContext..RequestAborted);
            //var logger = executionContext.GetLogger("PrincipalGet");

            //try
            //{
            var request = req.BuildRequestQuery<PrincipalGetCommand, ClientePrincipal>();

            var result = await _mediator.Send(request);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
            //}
            //catch (Exception ex)
            //{
            //    //logger.LogError(ex, req.Query.BuildMessage(), req.Query.ToList());
            //    return new BadRequestObjectResult(ex.ProcessException());
            //}
        }

        [Function("PrincipalAdd")]
        public async Task<HttpResponseData> Add(
            [HttpTrigger(AuthorizationLevel.Function, FunctionMethod.POST, Route = "Principal/Add")] HttpRequestData req,
            FunctionContext executionContext)
        {
            //using var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, req.HttpContext.RequestAborted);

            //try
            //{
            var request = await req.BuildRequestCommand<PrincipalAddCommand>();

            var result = await _mediator.Send(request);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
            //}
            //catch (Exception ex)
            //{
            //    //log.LogError(ex, req.Query.BuildMessage(), req.Query.ToList());
            //    return new BadRequestObjectResult(ex.ProcessException());
            //}
        }
    }
}