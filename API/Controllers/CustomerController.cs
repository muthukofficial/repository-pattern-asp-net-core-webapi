using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL;
using API.DAL.Domain.Services;
using API.DAL.Models;
using API.Extensions;
using API.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(IMapper mapper, ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CustomerViewModel>))]
        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync()
        {
            var customers = await _customerService.ListAsync();
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customers);
        }

        //GET: api/customer/{pageNumber:int}/{pageSize:int}
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(200, Type = typeof(List<CustomerViewModel>))]
        public async Task<IActionResult> GetCustomers(int pageNumber, int pageSize)
        {
            var customers = await _customerService.GetCustomers(pageNumber, pageSize);
            return Ok(_mapper.Map<List<CustomerViewModel>>(customers));
        }

        //GET: api/customer/{CustomerId}
        [HttpGet("{CustomerId}")]
        [ProducesResponseType(200, Type = typeof(CustomerViewModel))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> FindAsync(Guid CustomerId)
        {
            var customers = await _customerService.FindAsync(CustomerId);
            return Ok(_mapper.Map<CustomerViewModel>(customers));
        }

        //POST: api/customer
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CustomerViewModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] CustomerViewModel _customers)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var customer = _mapper.Map<CustomerViewModel, Customer>(_customers);
            var result = await _customerService.SaveAsync(customer);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Customer, CustomerViewModel>(result._customer);
            return Ok(resources);
        }

        //PUT: api/customer/CustomerId
        [HttpPut("{CustomerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAsync(Guid CustomerId, [FromBody] CustomerViewModel _customers)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var customer = _mapper.Map<CustomerViewModel, Customer>(_customers);
            var result = await _customerService.UpdateAsync(CustomerId, customer);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Customer, CustomerViewModel>(result._customer);
            return Ok(resources);
        }


        //DELETE: api/customer/CustomerId
        [HttpDelete("{CustomerId}")]
        [ProducesResponseType(200, Type = typeof(CustomerViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(Guid CustomerId)
        {
            var result = await _customerService.DeleteAsync(CustomerId);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Customer, CustomerViewModel>(result._customer);
            return Ok(resources);
        }
    }
}
