using MediatR;

namespace BugStore.Handlers.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Result<Responses.Customers.Update>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
