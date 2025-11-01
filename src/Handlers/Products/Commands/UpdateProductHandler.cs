using BugStore.Data;
using BugStore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Validation;

namespace BugStore.Handlers.Products.Commands;

public class UpdateProductHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand, Result<Responses.Products.Update>>
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Responses.Products.Update>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return Result<Responses.Products.Update>.Failure("Product not found.");
            }

            var product = new Responses.Products.Update
            {
                Description = request.Description,
                Price = request.Price,
                Slug = request.Slug,
                Title = request.Title
            };

            _context.Products.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<Responses.Products.Update>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Responses.Products.Update>.Failure(ex.Message);
        }
    }
}

