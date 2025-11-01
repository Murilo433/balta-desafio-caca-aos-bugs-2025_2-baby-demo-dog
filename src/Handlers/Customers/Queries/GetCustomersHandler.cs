using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers.Queries
{
    public class GetCustomersHandler(AppDbContext context) : IRequestHandler<GetCustomersQuerie, Result<IEnumerable<Responses.Customers.Get>>>
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<IEnumerable<Responses.Customers.Get>>> Handle(GetCustomersQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _context.Customers.ToListAsync(cancellationToken);

                return Result<IEnumerable<Responses.Customers.Get>>.Success(customers.Select(customer => new Responses.Customers.Get
                {
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    BirthDate = customer.BirthDate
                }));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Responses.Customers.Get>>.Failure(ex.Message);
            }
        }
    }
}
