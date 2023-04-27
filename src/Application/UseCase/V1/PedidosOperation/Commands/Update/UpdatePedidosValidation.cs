using FluentValidation;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Update;

public class UpdatePedidosValidation : AbstractValidator<UpdatePedidosCommand>
{
    public UpdatePedidosValidation()
    {
    }
}
