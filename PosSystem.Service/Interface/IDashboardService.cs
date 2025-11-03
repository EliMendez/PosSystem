using PosSystem.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Interface
{
    public interface IDashboardService
    {
        Task<List<ViewSellingMoreProducts>> GetSellingMoreProducts();
        Task<List<ViewLowStockProducts>> GetLowStockProducts();
        Task<List<ViewTotalSalesLastWeek>> GetTotalSalesLastWeek();
        Task<List<ViewTotalProductsSold>> GetTotalProductsSold();
        Task<List<ViewTotalIncomeLastWeek>> GetTotalIncomeLastWeek();
    }
}
