using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.View;
using PosSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly PosSystemContext _context;

        public DashboardRepository(PosSystemContext context)
        { 
            _context = context;
        }

        public async Task<List<ViewLastWeekIncomeTotal>> GetLastWeekIncomeTotal()
        {
            var sales = await _context.LastWeekIncomeTotal
                .FromSqlRaw("SELECT * FROM vwLastWeekIncomeTotal")
                .ToListAsync();

            return sales;
        }

        public Task<List<ViewLastWeekSales>> GetLastWeekSales()
        {
            var sales = _context.LastWeekSales
                .FromSqlRaw("SELECT * FROM vwLastWeekSales")
                .ToListAsync();

            return sales;
        }

        public Task<List<ViewLowStockProducts>> GetLowStockProducts()
        {
            var products = _context.LowStockProducts
                .FromSqlRaw("SELECT * FROM vwLowStockProducts")
                .ToListAsync();

            return products;
        }

        public Task<List<ViewSellingMoreProducts>> GetSellingMoreProducts()
        {
            var sales = _context.SellingMoreProducts
                .FromSqlRaw("SELECT * FROM vwSellingMoreProducts")
                .ToListAsync();

            return sales;
        }

        public async Task<List<ViewTotalProductsSold>> GetTotalProductsSold()
        {
            var sales = await _context.TotalProductsSolds
                .FromSqlRaw("SELECT * FROM vwTotalProductsSold")
                .ToListAsync();

            return sales;
        }
    }
}
