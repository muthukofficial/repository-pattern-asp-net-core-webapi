using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductController(IMapper mapper, IProductService productService, ILogger<ProductController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductViewModel>))]
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
        }

        //GET: api/product/{pageNumber:int}/{pageSize:int}
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(200, Type = typeof(List<CustomerViewModel>))]
        public async Task<IActionResult> GetProducts(int pageNumber, int pageSize)
        {
            var products = await _productService.GetProducts(pageNumber, pageSize);
            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        //GET: api/product/{ProductId}
        [HttpGet("{ProductId}")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> FindAsync(Guid ProductId)
        {
            var products = await _productService.FindAsync(ProductId);
            return Ok(_mapper.Map<ProductViewModel>(products));
        }

        //POST: api/product
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] ProductViewModel _products)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<ProductViewModel, Product>(_products);
            var result = await _productService.SaveAsync(product);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Product, ProductViewModel>(result._product);
            return Ok(resources);
        }

        //PUT: api/product/ProductId
        [HttpPut("{ProductId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAsync(Guid ProductId, [FromBody] ProductViewModel _products)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<ProductViewModel, Product>(_products);
            var result = await _productService.UpdateAsync(ProductId, product);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Product, ProductViewModel>(result._product);
            return Ok(resources);
        }


        //DELETE: api/product/ProductId
        [HttpDelete("{ProductId}")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(Guid ProductId)
        {
            var result = await _productService.DeleteAsync(ProductId);

            if (!result.Success)
                return BadRequest(result.Message);

            var resources = _mapper.Map<Product, ProductViewModel>(result._product);
            return Ok(resources);
        }
    }
}

