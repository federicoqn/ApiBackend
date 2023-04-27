using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using onboarding_backend.Domain.Dtos;
using onboarding_backend.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Queries.GetList
{

    public record struct ListPedidosId : IRequest<Response<PedidosDto>>
    {
        public ListPedidosId(string id) { Id = id; }
        public string Id { get; set; }
    }

    public class ListPedidosIdHandler : IRequestHandler<ListPedidosId, Response<PedidosDto>>
    {
        private readonly IReadOnlyQuery _query;

        public ListPedidosIdHandler(IReadOnlyQuery query)
        {
            _query = query;
        }

        public async Task<Response<PedidosDto>> Handle(ListPedidosId request, CancellationToken cancellationToken)
        {
            var result = await _query.GetByIdAsync<Pedidos>(nameof(request.Id), request.Id);
            var sqlString = $"select * from dbo.EstadoDelpedido where id = '{result.EstadoDelPedido}'";
            var resultadoEstadoDelPedido = await _query.FirstOrDefaultQueryAsync<EstadoDelPedido>(sqlString);

            PedidosDto pedidodto = new PedidosDto()
            {
                Id = result.Id,
                NumeroDePedido = result.NumeroDePedido,
                CicloDelPedido = result.CicloDelPedido,
                CodigoDeContratoInterno = result.CodigoDeContratoInterno,
                EstadoDelPedido = new EstadoDelPedido()
                {
                    Id = resultadoEstadoDelPedido is null ? 1 : resultadoEstadoDelPedido.Id,
                    Descripcion = resultadoEstadoDelPedido is null ? "Vacio" : resultadoEstadoDelPedido.Descripcion
                },
                CuentaCorriente = result.CuentaCorriente,
                Cuando = result.Cuando.ToString("MM/dd/yyyy")
            };
            

            return new Response<PedidosDto>
            {
                Content = pedidodto,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
