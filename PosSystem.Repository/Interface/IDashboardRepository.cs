using PosSystem.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Interface
{
    public interface IDashboardRepository
    {
        Task<List<ViewSellingMoreProducts>> GetSellingMoreProducts();
        Task<List<ViewLowStockProducts>> GetLowStockProducts();
        Task<List<ViewLastWeekSales>> GetLastWeekSales();
        Task<List<ViewTotalProductsSold>> GetTotalProductsSold();
        Task<List<ViewLastWeekIncomeTotal>> GetLastWeekIncomeTotal();
    }
}
