using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;

namespace PosSystem.Repository.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly PosSystemContext _posSystemContext;

        public ProductRepository(PosSystemContext posSystemContext)
        {
            _posSystemContext = posSystemContext;
        }
        
        public async Task<Product> Create(Product entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _posSystemContext.Products.Add(entity);
            await _posSystemContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _posSystemContext.Products.FindAsync(id);
            if(product == null)
            {
                return false;
            }

            _posSystemContext.Remove(product);
            await _posSystemContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _posSystemContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _posSystemContext.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> Update(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var productExists = await _posSystemContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == entity.ProductId);

            if(productExists == null)
            {
                throw new KeyNotFoundException($"No se encontró el producto con el ID: {entity.ProductId}");
            }

            productExists.Barcode = entity.Barcode;
            productExists.Description = entity.Description;
            productExists.CategoryId = entity.CategoryId;
            productExists.SalePrice = entity.SalePrice;
            productExists.Stock = entity.Stock;
            productExists.MinimumStock = entity.MinimumStock;
            productExists.Status = entity.Status;

            _posSystemContext.Products.Update(productExists);
            await _posSystemContext.SaveChangesAsync();
            return productExists;
        }
    }
}
