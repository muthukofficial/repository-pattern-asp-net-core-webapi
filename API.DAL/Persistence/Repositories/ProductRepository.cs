using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Domain.Repositories;
using API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        // List the Product
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // List the Product with page number and page size
        public async Task<List<Product>> ToListAsync(int page, int pageSize)
        {
            IQueryable<Product> productQuery = _context.Products.OrderBy(x => x.ProductName);

            if (page != -1)
                productQuery = productQuery.Skip((page - 1) * pageSize);

            if (pageSize != 1)
                productQuery = productQuery.Take(pageSize);

            var types = await productQuery.ToListAsync();
            return types;
        }

        // Save the Product
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        // Find the Product by ProductId
        public async Task<Product> FindByIdAsync(Guid ProductId)
        {
            return await _context.Products.FindAsync(ProductId);
        }

        // Update the Product
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        // Delete the Product
        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
