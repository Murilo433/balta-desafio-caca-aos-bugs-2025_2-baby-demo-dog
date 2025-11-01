using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers.Queries
{
    public class GetCustomerByIdHandler(AppDbContext context) : IRequestHandler<GetCustomerByIdQuerie, Result<Responses.Customers.GetById>>
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<Responses.Customers.GetById>> Handle(GetCustomerByIdQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var enitity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (enitity == null)
                {
                    return Result<Responses.Customers.GetById>.Failure("Customer not found");
                }

                var customer = new Responses.Customers.GetById
                {
                    Name = enitity.Name,
                    Email = enitity.Email,
                    Phone = enitity.Phone,
                    Birthday = enitity.BirthDate
                };

                return enitity != null ? Result<Responses.Customers.GetById>.Success(customer) : Result<Responses.Customers.GetById>.Failure("Customer not found");
            }
            catch (Exception ex)
            {
                return Result<Responses.Customers.GetById>.Failure(ex.Message);
            }
        }
    }
}
