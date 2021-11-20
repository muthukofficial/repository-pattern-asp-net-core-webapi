using System;
namespace API.ViewModels
{
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
