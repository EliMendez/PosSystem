using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

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
            if (saleId <= 0 || userId <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            if (reason != null || reason != "")
                throw new ArgumentException("La razón para anular la venta es requerida.");

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
            if (saleId <= 0)
                throw new ArgumentException("ID no puede ser menor o igual a cero.");

            return await _saleRepository.GetDetailsBySaleId(saleId);
        }

        public async Task<List<Sale>> SearchByDate(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.SearchByDate(startDate, endDate);
        }
    }
}
