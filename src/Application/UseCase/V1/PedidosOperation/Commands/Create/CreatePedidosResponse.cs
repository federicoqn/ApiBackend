using System;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Create
{
    public record struct CreatePedidosResponse(Guid PedidoId, string Message) { }
}
