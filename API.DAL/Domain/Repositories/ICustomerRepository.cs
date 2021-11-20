using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Models;

namespace API.DAL.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task<List<Customer>> ToListAsync(int page, int pageSize);
        Task AddAsync(Customer customer);
        Task<Customer> FindByIdAsync(Guid CustomerId);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
