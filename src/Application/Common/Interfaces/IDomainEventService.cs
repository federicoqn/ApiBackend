using onboarding_backend.Domain.Common;
using System.Threading.Tasks;

namespace onboarding_backend.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
