using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Products.Commands
{
    public class CreateProductCommand : IRequest<Result<Product>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
    }
}
