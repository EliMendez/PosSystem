using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Dto.Dto;
using PosSystem.Model.View;
using PosSystem.Service.Interface;

namespace PosSystem.Api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService dashboardService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        [HttpGet("selling_more_products")]
        public async Task<ActionResult<List<ViewSellingMoreProducts>>> GetSellingMoreProducts()
        {
            try
            {
                var products = await _dashboardService.GetSellingMoreProducts();
                var productsDto = _mapper.Map<List<SellingMoreProductsDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los productos más vendidos.", error = ex.Message });
            }
        }

        [HttpGet("low_stock_products")]
        public async Task<ActionResult<List<ViewLowStockProducts>>> GetLowStockProducts()
        {
            try
            {
                var products = await _dashboardService.GetLowStockProducts();
                var productsDto = _mapper.Map<List<LowStockProductsDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los productos por agotar.", error = ex.Message });
            }
        }

        [HttpGet("last_week_sales")]
        public async Task<ActionResult<List<ViewLastWeekSales>>> GetLastWeekSales()
        {
            try
            {
                var sales = await _dashboardService.GetLastWeekSales();
                var salesDto = _mapper.Map<List<LastWeekSalesDto>>(sales);
                return Ok(salesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las ventas de la última semana.", error = ex.Message });
            }
        }

        [HttpGet("last_week_income_total")]
        public async Task<ActionResult<List<ViewLastWeekIncomeTotal>>> GetLastWeekIncomeTotal()
        {
            try
            {
                var sales = await _dashboardService.GetLastWeekIncomeTotal();
                var salesDto = _mapper.Map<List<LastWeekIncomeTotalDto>>(sales);
                return Ok(salesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los ingresos de la última semana.", error = ex.Message });
            }
        }

        [HttpGet("total_products_sold")]
        public async Task<ActionResult<List<ViewTotalProductsSold>>> GetTotalProductsSold()
        {
            try
            {
                var sales = await _dashboardService.GetTotalProductsSold();
                var salesDto = _mapper.Map<List<TotalProductsSoldDto>>(sales);
                return Ok(salesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los productos vendidos.", error = ex.Message });
            }
        }
    }
}
