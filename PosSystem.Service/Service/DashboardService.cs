using PosSystem.Model.View;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<ViewTotalIncomeLastWeek>> GetTotalIncomeLastWeek()
        {
            return await _dashboardRepository.GetTotalIncomeLastWeek();
        }

        public async Task<List<ViewTotalSalesLastWeek>> GetTotalSalesLastWeek()
        {
            return await _dashboardRepository.GetTotalSalesLastWeek();
        }

        public async Task<List<ViewLowStockProducts>> GetLowStockProducts()
        {
            return await _dashboardRepository.GetLowStockProducts();
        }

        public async Task<List<ViewSellingMoreProducts>> GetSellingMoreProducts()
        {
            return await _dashboardRepository.GetSellingMoreProducts();
        }

        public async Task<List<ViewTotalProductsSold>> GetTotalProductsSold()
        {
            return await _dashboardRepository.GetTotalProductsSold();
        }
    }
}
