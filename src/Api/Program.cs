using Andreani.ARQ.WebHost.Extension;
using onboarding_backend.Application;
using onboarding_backend.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.OnboardingVideo.Events.Record;
using onboarding_backend.Services;
using Andreani.Scheme.Onboarding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*").WithHeaders("*").WithMethods("*");
    });
});

builder.Host.ConfigureAndreaniWebHost(args);
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddKafka(builder.Configuration)
    .CreateOrUpdateTopic(6, names: "PedidoCreadoFQ")
    .CreateOrUpdateTopic(6, names: "PedidoAsignadoFQ")
    .ToConsumer<Subscriber, Pedido>("PedidoAsignadoFQ")
    .ToProducer<Pedido>("PedidoCreadoFQ")
    .Build();

var app = builder.Build();

app.UseCors();

app.ConfigureAndreani(app.Environment, app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.Run();
