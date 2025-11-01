using MediatR;

namespace BugStore.Handlers.Products.Commands
{
    public class UpdateProductCommand : IRequest<Result<Responses.Products.Update>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
    }
}
