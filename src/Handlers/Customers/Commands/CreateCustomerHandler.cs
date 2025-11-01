using BugStore.Data;
using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Customers.Commands;

public class CreateCustomerHandler(AppDbContext context) : IRequestHandler<CreateCustomerCommand, Result<Customer>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };

            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Customer>.Success(customer);

        }
        catch (Exception ex)
        {
            return Result<Customer>.Failure(ex.Message);
        }
    }
}
