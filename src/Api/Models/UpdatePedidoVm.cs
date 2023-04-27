using Andreani.ARQ.Core.Clases;
using System;

namespace WebApi.Models;

public class UpdatePedidosVm
{
    public Guid Id { get; set; }
    public int NumeroDePedido { get; set; }
    public string CicloDelPedido { get; set; }
    public Int64 CodigoDeContratoInterno { get; set; }
    public string CuentaCorriente { get; set; }
    public DateTime Cuando { get; set; }
}