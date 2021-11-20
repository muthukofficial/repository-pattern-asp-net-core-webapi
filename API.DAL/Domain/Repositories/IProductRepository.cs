using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Models;

namespace API.DAL.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<List<Product>> ToListAsync(int page, int pageSize);
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(Guid ProductId);
        void Update(Product product);
        void Remove(Product product);
    }
}
