using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

namespace PosSystem.Service.Service
{
    public class RoleService : IService<Role>
    {
        private readonly IRepository<Role> _roleRepository;
        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> Create(Role entity)
        {
            return await _roleRepository.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _roleRepository.Delete(id);
        }

        public async Task<List<Role>> GetAll()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<Role?> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _roleRepository.GetById(id);
        }

        public async Task<Role> Update(Role entity)
        {
            return await _roleRepository.Update(entity);
        }
    }
}
