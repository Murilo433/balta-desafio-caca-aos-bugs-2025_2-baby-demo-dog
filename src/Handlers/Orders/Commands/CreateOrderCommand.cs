using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Result<Order>>
    {
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<OrderLine> Lines { get; set; } = null!;
    }
}
