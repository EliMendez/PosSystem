using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

namespace PosSystem.Service.Service
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Create(Product entity)
        {
            return await _productRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _productRepository.Delete(id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product?> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _productRepository.GetById(id);
        }

        public async Task<Product> Update(Product entity)
        {
            return await _productRepository.Update(entity);
        }
    }
}
