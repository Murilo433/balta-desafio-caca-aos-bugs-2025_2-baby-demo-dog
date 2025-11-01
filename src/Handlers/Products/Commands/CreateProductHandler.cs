using BugStore.Data;
using BugStore.Models;
using MediatR;

namespace BugStore.Handlers.Products.Commands;

public class CreateProductHandler(AppDbContext context) : IRequestHandler<CreateProductCommand, Result<Product>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product
            {
                Description = request.Description,
                Price = request.Price,
                Slug = request.Slug,
                Title = request.Title
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Product>.Success(product);

        }
        catch (Exception ex)
        {
            return Result<Product>.Failure(ex.Message);
        }
    }
}
