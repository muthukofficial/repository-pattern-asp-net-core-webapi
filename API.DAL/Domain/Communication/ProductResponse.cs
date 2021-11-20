using System;
using API.DAL.Models;

namespace API.DAL.Domain.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product _product { get; private set; }

        public ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            _product = product;
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="product">Product has been added successfully</param>
        /// <returns>Response.</returns>
        public ProductResponse(Product product) : this(true, string.Empty, product)
        { }

        /// <summary>
        /// Create an error response
        /// </summary>
        /// <param name="message">verify the product details</param>
        /// <returns>Response.</returns>
        public ProductResponse(string message) : this(false, message, null)
        { }
    }
}
