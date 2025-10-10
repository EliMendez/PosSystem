using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Interface
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAll();
        Task<List<Sale>> SearchByDate(DateTime startDate, DateTime endDate);
        Task<Sale> Create(Sale sale);
        Task<Sale?> CancelSale(int saleId, string reason, int userId);
        Task<List<SaleDetail>> GetDetailsBySaleId(int saleId);
    }
}
