using MediatR;

namespace BugStore.Handlers.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result<Responses.Products.Delete>>
    {
        public Guid Id { get; set; }
    }
}
