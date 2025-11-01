using MediatR;

namespace BugStore.Handlers.Products.Queries;

public class GetProductsQuerie : IRequest<Result<IEnumerable<Responses.Products.Get>>>
{

}