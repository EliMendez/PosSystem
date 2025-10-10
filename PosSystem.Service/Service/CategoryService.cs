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
    public class CategoryService : IService<Category>
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Create(Category entity)
        {
            return await _categoryRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _categoryRepository.Delete(id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task<Category> Update(Category entity)
        {
            return await _categoryRepository.Update(entity);
        }
    }
}
