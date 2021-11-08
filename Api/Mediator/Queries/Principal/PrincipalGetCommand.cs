using Api.Core.Interfaces;
using MediatR;
using SD.Shared.Core;
using SD.Shared.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Mediator.Queries.Principal
{
    public class PrincipalGetCommand : MediatorQuery<ClientePrincipal>
    {
        public PrincipalGetCommand() : base(CosmosType.Principal)
        {
        }

        public override void SetParameters(string query)
        {
            //do nothing
        }
    }

    public class PrincipalGetHandler : IRequestHandler<PrincipalGetCommand, ClientePrincipal>
    {
        private readonly IRepository _repo;

        public PrincipalGetHandler(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClientePrincipal> Handle(PrincipalGetCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Get<ClientePrincipal>(request.Id, request.IdLoggedUser, cancellationToken);
        }
    }
}