using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BugStore.Responses.Products;

namespace BugStore.Handlers.Products.Queries
{
    public class GetProductsHandler(AppDbContext context) : IRequestHandler<GetProductsQuerie, Result<IEnumerable<Get>>>
    {
        private readonly AppDbContext _context = context;

        public async Task<Result<IEnumerable<Get>>> Handle(GetProductsQuerie request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _context.Products.ToListAsync(cancellationToken);

                return Result<IEnumerable<Get>>.Success(products.Select(product => new Get
                {
                    Title = product.Title,
                    Description = product.Description,
                    Slug = product.Slug,
                    Price = product.Price
                }));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Get>>.Failure(ex.Message);
            }
        }
    }
}
