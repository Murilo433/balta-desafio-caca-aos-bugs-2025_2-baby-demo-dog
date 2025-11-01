using BugStore.Data;
using BugStore.Responses.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products.Queries
{
    public class GetProductByIdHandler(AppDbContext context) : IRequestHandler<GetProductByIdQuerie, Result<Get>>
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<Get>> Handle(GetProductByIdQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var enitity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


                var product = new Get
                {
                    Title = enitity?.Title ?? string.Empty,
                    Description = enitity?.Description ?? string.Empty,
                    Slug = enitity?.Slug ?? string.Empty,
                    Price = enitity?.Price ?? 0
                };

                return enitity != null ? Result<Get>.Success(product) : Result<Get>.Failure("Product not found");
            }
            catch (Exception ex)
            {
                return Result<Get>.Failure(ex.Message);
            }
        }
    }
}
