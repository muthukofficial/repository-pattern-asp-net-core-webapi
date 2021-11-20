using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DAL.Domain.Communication;
using API.DAL.Domain.Repositories;
using API.DAL.Domain.Services;
using API.DAL.Models;

namespace API.DAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<List<Product>> GetProducts(int page, int pageSize)
        {
            return await _productRepository.ToListAsync(page, pageSize);
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<Product> FindAsync(Guid ProductId)
        {
            return await _productRepository.FindByIdAsync(ProductId);
        }

        public async Task<ProductResponse> UpdateAsync(Guid ProductId, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(ProductId);

            if (existingProduct == null)
                return new ProductResponse("Product not found");

            try
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;

                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> DeleteAsync(Guid ProductId)
        {
            var existingProduct = await _productRepository.FindByIdAsync(ProductId);

            if (existingProduct == null)
                return new ProductResponse("Product not found");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }
    }
}
