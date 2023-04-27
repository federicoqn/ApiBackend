using Microsoft.Extensions.Logging;
using Andreani.Scheme.Onboarding;
using System.Threading.Tasks;
using Andreani.ARQ.AMQStreams.Interface;
using System;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Update;
using onboarding_backend.Domain.Entities;
using MediatR;
using Andreani.ARQ.Core.Interface;

namespace onboarding_backend.Services
{
    public class Subscriber : ISubscriber
    {
        private ILogger<ISubscriber> _logger;
        private readonly ISender _mediator;
        private readonly ITransactionalRepository _repository;

        public Subscriber(ILogger<Subscriber> logger, ISender mediator, ITransactionalRepository repository)
        {
            _logger = logger;
            _mediator = mediator;
            _repository = repository;
        }
        public async Task ReciveCustomEvent(Pedido response)
        {
            //_logger.LogInformation("{0}", response.id);
            Console.WriteLine($"Pedido ID: {response.id} - Estado del Pedido: {response.estadoDelPedido} - Codigo Interno: {response.codigoDeContratoInterno}");
            _repository.Insert(response);
            await _repository.SaveChangeAsync();
        }
    }
}
