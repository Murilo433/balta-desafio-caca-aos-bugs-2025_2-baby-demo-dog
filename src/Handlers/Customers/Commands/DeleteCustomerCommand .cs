using MediatR;

namespace BugStore.Handlers.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Result<Responses.Customers.Delete>>
    {
        public Guid Id { get; set; }
    }
}
