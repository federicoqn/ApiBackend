using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Create;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Update;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Queries.GetList;
using onboarding_backend.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using onboarding_backend.Domain.Entities;
using System;

namespace WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PedidosController : ApiControllerBase
{

    /// <summary>
    /// Creación de nueva pedido
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>

    [HttpPost]
    [ProducesResponseType(typeof(CreatePedidosResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreatePedidosCommand body) => Result(await Mediator.Send(body));

    /// <summary>
    /// Listado de pedidos de la base de datos
    /// </summary>
    /// <remarks>en los remarks podemos documentar información más detallada</remarks>
    /// <returns></returns>

    [HttpGet]
    [ProducesResponseType(typeof(List<PedidosDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPedidos()));


    /// <summary>
    /// Consulta de pedido por id de la base de datos
    /// </summary>
    /// <remarks>en los remarks podemos documentar información más detallada</remarks>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(List<PedidosDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(string id) => Result(await Mediator.Send(new ListPedidosId(id)));
    

}
