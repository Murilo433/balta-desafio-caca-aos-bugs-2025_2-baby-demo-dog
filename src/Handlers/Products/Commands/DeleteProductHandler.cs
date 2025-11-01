using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products.Commands;

public class DeleteProductHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand, Result<Responses.Products.Delete>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Responses.Products.Delete>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return Result<Responses.Products.Delete>.Failure("Product not found.");
            }

            var product = new Responses.Products.Delete
            {
                Id = entity.Id
            };

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Responses.Products.Delete>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Responses.Products.Delete>.Failure(ex.Message);
        }
    }
}

