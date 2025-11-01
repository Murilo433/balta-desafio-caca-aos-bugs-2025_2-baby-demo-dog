using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers.Commands;

public class DeleteCustomerHandler(AppDbContext context) : IRequestHandler<DeleteCustomerCommand, Result<Responses.Customers.Delete>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Responses.Customers.Delete>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return Result<Responses.Customers.Delete>.Failure("Customer not found.");
            }

            var customer = new Responses.Customers.Delete
            {
                Id = entity.Id
            };

            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Responses.Customers.Delete>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<Responses.Customers.Delete>.Failure(ex.Message);
        }
    }
}

