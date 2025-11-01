using BugStore.Data;
using BugStore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders.Commands;

public class CreateOrderHandler(AppDbContext context) : IRequestHandler<CreateOrderCommand, Result<Order>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);


            var productIds = request.Lines.Select(l => l.ProductId).ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            if (products.Count == 0)
            {
                return Result<Order>.Failure("Product not found.");
            }

            if (customer is null)
            {
                return Result<Order>.Failure("Customer not found.");
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                Lines = request.Lines
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Order>.Success(order);

        }
        catch (Exception ex)
        {
            return Result<Order>.Failure(ex.Message);
        }
    }
}
