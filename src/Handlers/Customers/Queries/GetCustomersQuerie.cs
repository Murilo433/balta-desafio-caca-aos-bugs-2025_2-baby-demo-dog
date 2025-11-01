using MediatR;

namespace BugStore.Handlers.Customers.Queries;

public class GetCustomersQuerie : IRequest<Result<IEnumerable<Responses.Customers.Get>>>
{
}