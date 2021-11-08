using Api.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SD.Shared.Model;

namespace Api.Mediator.Command.Principal
{
    public class PrincipalAddCommand : ClientePrincipal, IRequest<ClientePrincipal> { }

    public class PrincipalAddHandler : IRequestHandler<PrincipalAddCommand, ClientePrincipal>
    {
        private readonly IRepository _repo;

        public PrincipalAddHandler(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClientePrincipal> Handle(PrincipalAddCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Add(request, cancellationToken);
        }
    }
}