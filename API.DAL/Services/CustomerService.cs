using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Domain.Communication;
using API.DAL.Domain.Repositories;
using API.DAL.Domain.Services;
using API.DAL.Models;

namespace API.DAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _customerRepository.ListAsync();
        }

        public async Task<List<Customer>> GetCustomers(int page, int pageSize)
        {
            return await _customerRepository.ToListAsync(page, pageSize);
        }

        public async Task<CustomerResponse> SaveAsync(Customer customer)
        {
            try
            {
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(customer);
            }
            catch(Exception ex)
            {
                return new CustomerResponse($"An error occurred when saving the customer: {ex.Message}");
            }
        }

        public async Task<Customer> FindAsync(Guid CustomerId)
        {
            var ex = await _customerRepository.FindByIdAsync(CustomerId);
            return ex;
        }

        public async Task<CustomerResponse> UpdateAsync(Guid CustomerId, Customer customer)
        {
            var existingCustomer = await _customerRepository.FindByIdAsync(CustomerId);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Address = customer.Address;
            existingCustomer.PostCode = customer.PostCode;
            existingCustomer.City = customer.City;
            existingCustomer.Country = customer.Country;

            try
            {
                _customerRepository.Update(existingCustomer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error occurred when updating the customer: {ex.Message}");
            }
        }

        public async Task<CustomerResponse> DeleteAsync(Guid CustomerId)
        {
            var existingCustomer = await _customerRepository.FindByIdAsync(CustomerId);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            try
            {
                _customerRepository.Remove(existingCustomer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error occurred when deleting the customer: {ex.Message}");
            }
        }
    }
}
