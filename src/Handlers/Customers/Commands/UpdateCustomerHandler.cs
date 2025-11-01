using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers.Commands;

public class UpdateCustomerHandler(AppDbContext context) : IRequestHandler<UpdateCustomerCommand, Result<Responses.Customers.Update>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Responses.Customers.Update>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return Result<Responses.Customers.Update>.Failure("Customer not found.");
            }

            var customer = new Responses.Customers.Update
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            _context.Customers.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Responses.Customers.Update>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<Responses.Customers.Update>.Failure(ex.Message);
        }
    }
}

