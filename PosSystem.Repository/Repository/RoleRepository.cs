using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;

namespace PosSystem.Repository.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly PosSystemContext _posSystemContext;

        public RoleRepository(PosSystemContext posSystemContext)
        {
            _posSystemContext = posSystemContext;
        }
        
        public async Task<Role> Create(Role entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _posSystemContext.Roles.Add(entity);
            await _posSystemContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _posSystemContext.Roles.FindAsync(id);
            if(role == null)
            {
                return false;
            }

            _posSystemContext.Remove(role);
            await _posSystemContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Role>> GetAll()
        {
            return await _posSystemContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetById(int id)
        {
            return await _posSystemContext.Roles.FindAsync(id);
        }

        public async Task<Role> Update(Role entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var roleExists = await _posSystemContext.Roles.FirstOrDefaultAsync(r => r.RoleId == entity.RoleId);
            if(roleExists == null)
            {
                throw new KeyNotFoundException($"No se encontró el rol con el ID: {entity.RoleId}");
            }

            roleExists.Description = entity.Description;
            roleExists.Status = entity.Status;

            _posSystemContext.Roles.Update(roleExists);
            await _posSystemContext.SaveChangesAsync();
            return roleExists;
        }
    }
}
