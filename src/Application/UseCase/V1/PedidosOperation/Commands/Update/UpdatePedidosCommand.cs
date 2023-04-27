using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using onboarding_backend.Domain.Common;
using onboarding_backend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Update;

namespace onboarding_backend.Application.UseCase.V1.PedidosOperation.Commands.Update
{
    public class UpdatePedidosCommand : IRequest<Response<string>>
    {
    }

 }
