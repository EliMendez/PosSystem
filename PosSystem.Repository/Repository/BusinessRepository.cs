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
    public class BusinessRepository : IBusinessRepository
    {
        private readonly PosSystemContext _posSystemContext;

        public BusinessRepository(PosSystemContext posSystemContext)
        {
            _posSystemContext = posSystemContext;
        }
        
        public async Task<Business> Create(Business business)
        {
            if(business == null)
            {
                throw new ArgumentNullException(nameof(business));
            }

            _posSystemContext.Businesses.Add(business);
            await _posSystemContext.SaveChangesAsync();
            return business;
        }

        public async Task<Business?> GetById(int id)
        {
            return await _posSystemContext.Businesses.FindAsync(id);
        }

        public async Task<Business> Update(Business business)
        {
            if (business == null)
            {
                throw new ArgumentNullException(nameof(business));
            }

            var businessExists = await _posSystemContext.Businesses.FirstOrDefaultAsync(b => b.BusinessId == business.BusinessId);
            if(businessExists == null)
            {
                throw new KeyNotFoundException($"No se encontró la empresa con el ID: {business.BusinessId}");
            }

            businessExists.Ruc = business.Ruc;
            businessExists.CompanyName = business.CompanyName;
            businessExists.Email = business.Email;
            businessExists.Phone = business.Phone;
            businessExists.Address = business.Address;
            businessExists.Owner = business.Owner;
            businessExists.Discount = business.Discount;

            _posSystemContext.Businesses.Update(businessExists);
            await _posSystemContext.SaveChangesAsync();
            return businessExists;
        }
    }
}
