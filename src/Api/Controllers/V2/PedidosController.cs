using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Queries.GetList;
using onboarding_backend.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using onboarding_backend.Application.UseCase.V1.PersonOperation.Queries.GetList;

namespace WebApi.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]

public class PedidosController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<PedidosDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] string id) => this.Result(await Mediator.Send(new ListPedidos()));

}
