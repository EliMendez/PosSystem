using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly PosSystemContext _posSystemContext;

        public CategoryRepository(PosSystemContext posSystemContext)
        {
            _posSystemContext = posSystemContext;
        }
        
        public async Task<Category> Create(Category entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _posSystemContext.Categories.Add(entity);
            await _posSystemContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _posSystemContext.Categories.FindAsync(id);
            if(category == null)
            {
                return false;
            }

            _posSystemContext.Remove(category);
            await _posSystemContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _posSystemContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _posSystemContext.Categories.FindAsync(id);
        }

        public async Task<Category> Update(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var categoryExists = await _posSystemContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == entity.CategoryId);
            if(categoryExists == null)
            {
                throw new KeyNotFoundException($"No se encontró la categoría con el ID: {entity.CategoryId}");
            }

            categoryExists.Description = entity.Description;
            categoryExists.Status = entity.Status;

            _posSystemContext.Categories.Update(categoryExists);
            await _posSystemContext.SaveChangesAsync();
            return categoryExists;
        }
    }
}
