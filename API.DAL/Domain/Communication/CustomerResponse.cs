using System;
using API.DAL.Models;

namespace API.DAL.Domain.Communication
{
    public class CustomerResponse : BaseResponse
    {
        public Customer _customer { get; private set; }

        public CustomerResponse(bool success, string message, Customer customer) : base(success, message)
        {
            _customer = customer;
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="customer">Customer has been added successfully</param>
        /// <returns>Response.</returns>
        public CustomerResponse(Customer customer) : this(true, string.Empty, customer)
        { }

        /// <summary>
        /// Create an error response
        /// </summary>
        /// <param name="message">verify the customer details</param>
        /// <returns>Response.</returns>
        public CustomerResponse(string message) : this(false, message, null)
        { }
    }
}
