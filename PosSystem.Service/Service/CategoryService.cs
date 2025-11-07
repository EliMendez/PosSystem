using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PosSystem.Dto.Validators;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

namespace PosSystem.Service.Service
{
    public class CategoryService : IService<Category>
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Create(Category entity)
        {
            return await _categoryRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _categoryRepository.Delete(id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category?> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _categoryRepository.GetById(id);
        }

        public async Task<Category> Update(Category entity)
        {
            return await _categoryRepository.Update(entity);
        }
    }
}
