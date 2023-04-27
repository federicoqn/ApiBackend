using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using onboarding_backend.Domain.Dtos;
using onboarding_backend.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Queries.GetList {

  public record struct ListPedidos : IRequest<Response<List<PedidosDto>>>
  {
  }

  public class ListPedidosHandler : IRequestHandler<ListPedidos, Response<List<PedidosDto>>>
  {
    private readonly IReadOnlyQuery _query;

    public ListPedidosHandler(IReadOnlyQuery query)
    {
      _query = query;
    }

    public async Task<Response<List<PedidosDto>>> Handle(ListPedidos request, CancellationToken cancellationToken)
    {
        var result = await _query.GetAllAsync<Pedidos>(nameof(Pedidos));
        List<PedidosDto> resultPedidos = new List<PedidosDto>();

        foreach (var item in result)
        {
            var sqlString = $"select * from dbo.EstadoDelpedido where id = '{item.EstadoDelPedido}'";
            var resultadoEstadoDelPedido = await _query.FirstOrDefaultQueryAsync<EstadoDelPedido>(sqlString);

            PedidosDto pedidodto = new PedidosDto()
            {
                Id = item.Id,
                NumeroDePedido = item.NumeroDePedido,
                CicloDelPedido = item.CicloDelPedido,
                CodigoDeContratoInterno = item.CodigoDeContratoInterno,
                EstadoDelPedido = new EstadoDelPedido()
                {
                    Id = resultadoEstadoDelPedido is null ? 1 : resultadoEstadoDelPedido.Id,
                    Descripcion = resultadoEstadoDelPedido is null ? "Vacio" : resultadoEstadoDelPedido.Descripcion
                },
                CuentaCorriente = item.CuentaCorriente,
                Cuando = item.Cuando.ToString("MM/dd/yyyy")
            };
            resultPedidos.Add(pedidodto);
        }

        return new Response<List<PedidosDto>>
        {
            Content = resultPedidos,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
    }
}
