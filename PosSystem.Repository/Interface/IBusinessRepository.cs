using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Interface
{
    public interface IBusinessRepository
    {
        Task<Business?> GetById(int id);
        Task<Business> Create(Business business);
        Task<Business> Update(Business business);
    }
}
