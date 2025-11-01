using MediatR;

namespace BugStore.Handlers.Customers.Queries;

public class GetCustomerByIdQuerie : IRequest<Result<Responses.Customers.GetById>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}