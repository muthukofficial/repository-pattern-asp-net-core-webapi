using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Domain.Communication;
using API.DAL.Models;

namespace API.DAL.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<List<Product>> GetProducts(int page, int pageSize);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(Guid ProductId, Product product);
        Task<ProductResponse> DeleteAsync(Guid ProductId);
        Task<Product> FindAsync(Guid ProductId);
    }
}
