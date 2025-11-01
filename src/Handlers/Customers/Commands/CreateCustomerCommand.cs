using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Result<Customer>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
