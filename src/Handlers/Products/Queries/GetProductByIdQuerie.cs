using MediatR;

namespace BugStore.Handlers.Products.Queries;

public class GetProductByIdQuerie : IRequest<Result<Responses.Products.Get>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
}