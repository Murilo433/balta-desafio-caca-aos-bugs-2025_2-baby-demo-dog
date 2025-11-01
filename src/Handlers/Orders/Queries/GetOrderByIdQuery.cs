using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Orders.Queries;

public class GetOrderByIdQuery : IRequest<Result<Responses.Orders.GetById>>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<OrderLine> Lines { get; set; } = null!;
}