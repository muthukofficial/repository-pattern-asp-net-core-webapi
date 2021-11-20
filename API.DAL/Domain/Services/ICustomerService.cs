using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Domain.Communication;
using API.DAL.Models;

namespace API.DAL.Domain.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task<List<Customer>> GetCustomers(int page, int pageSize);
        Task<CustomerResponse> SaveAsync(Customer customer);
        Task<CustomerResponse> UpdateAsync(Guid CustomerId, Customer customer);
        Task<CustomerResponse> DeleteAsync(Guid CustomerId);
        Task<Customer> FindAsync(Guid CustomerId);
    }
}
