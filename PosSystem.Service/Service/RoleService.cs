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
    public class RoleService : IService<Role>
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository)
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
            return await _roleRepository.GetById(id);
        }

        public async Task<Role> Update(Role entity)
        {
            return await _roleRepository.Update(entity);
        }
    }
}
