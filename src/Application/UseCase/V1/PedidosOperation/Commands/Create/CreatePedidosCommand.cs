using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using onboarding_backend.Domain.Entities;
using onboarding_backend.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Andreani.Scheme.Onboarding;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Create
{
    public class CreatePedidosCommand : IRequest<Response<CreatePedidosResponse>>
    {
        public long CodigoDeContratoInterno { get; set; }
        public string CuentaCorriente { get; set; }

    }

    public class CreatePedidosCommandHandler : IRequestHandler<CreatePedidosCommand, Response<CreatePedidosResponse>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly ILogger<CreatePedidosCommandHandler> _logger;
        private Andreani.ARQ.AMQStreams.Interface.IPublisher _publisher;

        public CreatePedidosCommandHandler(ITransactionalRepository repository, ILogger<CreatePedidosCommandHandler> logger, Andreani.ARQ.AMQStreams.Interface.IPublisher publisher)
        {
            _repository = repository;
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<Response<CreatePedidosResponse>> Handle(CreatePedidosCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();

            var entity = new Pedidos
            {
                Id = id,
                NumeroDePedido = null,
                CicloDelPedido = id.ToString(),
                CodigoDeContratoInterno = request.CodigoDeContratoInterno,
                EstadoDelPedido = (int?) EstadoDelPedidoE.CREADO,
                CuentaCorriente = request.CuentaCorriente,
                Cuando = DateTime.Now
            };

            _repository.Insert(entity);
            await _repository.SaveChangeAsync();
            _logger.LogDebug("the pedido was add correctly");

            await _publisher.To<Pedido>(new Pedido()
            {
                id = id.ToString(),
                numeroDePedido = 0,
                cicloDelPedido = id.ToString(),
                codigoDeContratoInterno = request.CodigoDeContratoInterno,
                estadoDelPedido = "CREADO",
                cuentaCorriente = long.Parse(request.CuentaCorriente),
                cuando = DateTime.Now.ToString()
            }, Guid.NewGuid().ToString());

            return new Response<CreatePedidosResponse>
            {
                Content = new CreatePedidosResponse
                {
                    Message = "Success",
                    PedidoId = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
