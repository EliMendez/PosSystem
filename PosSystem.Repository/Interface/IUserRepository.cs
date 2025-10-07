using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User> Create(User user, string password);
        Task<User> Update(User user, string password);
        Task<bool> Delete(int id);
        Task<string> GetRoleById(int id);
        Task<User?> GetByEmail(string email);
    }
}
