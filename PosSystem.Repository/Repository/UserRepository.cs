using Microsoft.AspNetCore.Identity;
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
    public class UserRepository : IUserRepository
    {
        private readonly PosSystemContext _posSystemContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(PosSystemContext posSystemContext, UserManager<User> userManager)
        {
            _posSystemContext = posSystemContext;
            _userManager = userManager;
        }
        
        public async Task<User> Create(User user, string password)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, password);
            if(!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _posSystemContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _posSystemContext.Remove(user);
            await _posSystemContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAll()
        {
            return await _posSystemContext.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _posSystemContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _posSystemContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<string> GetRoleById(int id)
        {
            var user = await _posSystemContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return user?.Role?.Description ?? string.Empty;
        }

        public async Task<User> Update(User user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userExists = await _posSystemContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if(userExists == null)
            {
                throw new KeyNotFoundException($"No se encontró el usuario con el ID: {user.UserId}");
            }

            userExists.Name = user.Name;
            userExists.Surname = user.Surname;
            userExists.RoleId = user.RoleId;
            userExists.PhoneNumber = user.PhoneNumber;
            userExists.UserName = user.UserName;
            userExists.Status = user.Status;
            if (!string.IsNullOrEmpty(password))
            {
                var passwordHasher = new PasswordHasher<User>();
                userExists.PasswordHash = passwordHasher.HashPassword(userExists, password);
            }

            _posSystemContext.Users.Update(userExists);
            await _posSystemContext.SaveChangesAsync();
            return userExists;
        }
    }
}
