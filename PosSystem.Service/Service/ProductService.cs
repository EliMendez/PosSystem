using PosSystem.Model.Model;
using PosSystem.Repository.Repository;
using PosSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Service
{
    public class ProductService : IService<Product>
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Create(Product entity)
        {
            return await _productRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _productRepository.Delete(id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<Product> Update(Product entity)
        {
            return await _productRepository.Update(entity);
        }
    }
}
