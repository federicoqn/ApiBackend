using FluentValidation;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Queries.GetList;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Create
{
    public class CreatePedidosValidation : AbstractValidator<ListPedidosId>
    {
        public CreatePedidosValidation() {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El campo id está vacio")
            .Length(36)
            .WithMessage("Longitud de caracteres fuera de rango")
            .Matches("[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}")
            .WithMessage("Expresion sin formato de GuId");
            ;
        }
    }
}
