using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Domain.Repositories;
using API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        // List the customer
        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // List the customer with page number and page size
        public async Task<List<Customer>> ToListAsync(int page, int pageSize)
        {
            IQueryable<Customer> customerQuery = _context.Customers.OrderBy(x => x.CustomerName);

            if (page != -1)
                customerQuery = customerQuery.Skip((page - 1) * pageSize);

            if (pageSize != 1)
                customerQuery = customerQuery.Take(pageSize);

            var types = await customerQuery.ToListAsync();
            return types;
        }

        // Save the customer
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        // Find the customer by CustomerId
        public async Task<Customer> FindByIdAsync(Guid CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        // Update the Customer
        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        // Delete the Customer
        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }
    }
}
