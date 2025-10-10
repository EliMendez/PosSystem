using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Sale?> CancelSale(int saleId, string reason, int userId)
        {
            return await _saleRepository.CancelSale(saleId, reason, userId);
        }

        public async Task<Sale> Create(Sale sale)
        {
            return await _saleRepository.Create(sale);
        }

        public async Task<List<Sale>> GetAll()
        {
            return await _saleRepository.GetAll();
        }

        public async Task<List<SaleDetail>> GetDetailsBySaleId(int saleId)
        {
            return await _saleRepository.GetDetailsBySaleId(saleId);
        }

        public async Task<List<Sale>> SearchByDate(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.SearchByDate(startDate, endDate);
        }
    }
}
