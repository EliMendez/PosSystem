using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Interface
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAll();
        Task<List<Sale>> SearchByDate(DateOnly startDate, DateOnly endDate);
        Task<Sale> Create(Sale sale);
        Task<Sale?> CancelSale(int saleId, string reason, int userId);
        Task UpdateStock(List<SaleDetail> saleDetails);
        Task<List<SaleDetail>> GetDetailsBySaleId(int saleId);
    }
}
