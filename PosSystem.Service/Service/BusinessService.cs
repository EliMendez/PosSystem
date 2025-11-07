using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

namespace PosSystem.Service.Service
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        public BusinessService(IBusinessRepository businessRepository) 
        { 
            _businessRepository = businessRepository;
        }

        public async Task<Business?> Get()
        {
            var business = await _businessRepository.Get();
            if (business == null)
            {
                return null;
            }
            return business;
        }

        public async Task<Business> Save(Business business)
        {
            var businessExists = await _businessRepository.Get();
            if (businessExists == null)
            {
                var createdBusiness = await _businessRepository.Create(business);
                return createdBusiness;
            }
            else
            {
                business.BusinessId = businessExists.BusinessId;
                var updatedBusiness = await _businessRepository.Update(business);
                return updatedBusiness;
            }
        }
    }
}
