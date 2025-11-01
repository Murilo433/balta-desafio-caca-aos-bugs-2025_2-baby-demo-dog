using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders.Queries
{
    public class GetOrderByIdHandler(AppDbContext context) : IRequestHandler<GetOrderByIdQuery, Result<Responses.Orders.GetById>>
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<Responses.Orders.GetById>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _context.Orders
                    .Include(x => x.Customer)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (order == null)
                    return Result<Responses.Orders.GetById>.Failure("Order not found");

                return Result<Responses.Orders.GetById>.Success(new Responses.Orders.GetById
                {
                    Lines = order.Lines,
                    Customer = order.Customer,
                    CreatedAt = order.CreatedAt,
                    UpdatedAt = order.UpdatedAt
                });
            }
            catch (Exception ex)
            {
                return Result<Responses.Orders.GetById>.Failure(ex.Message);
            }
        }
    }
}
