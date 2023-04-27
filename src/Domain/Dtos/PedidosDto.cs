using onboarding_backend.Domain.Enums;
using onboarding_backend.Domain.Entities;
using System;

namespace onboarding_backend.Domain.Dtos;

public record struct PedidosDto(
        Guid Id,
        int? NumeroDePedido,
        string? CicloDelPedido,
        long? CodigoDeContratoInterno,
        string? CuentaCorriente,
        string? Cuando,
        EstadoDelPedido EstadoDelPedido
        )
{ }
