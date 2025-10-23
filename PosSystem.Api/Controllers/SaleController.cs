using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;

namespace PosSystem.Api.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SaleController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet("search-date")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> SearchByDate([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = await _saleService.SearchByDate(startDate, endDate);
                if (sales.Count() == 0)
                {
                    return NotFound(new { statusCode = 404, message = "No existe ningún registro para la consulta solicitada."});
                }

                var salesDto = _mapper.Map<List<SaleDto>>(sales);
                return Ok(salesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar por fechas.", ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(p => p.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { statusCode = 400, message = "Los datos proporcionados son inválidos.", errors = errors ?? new List<string>() });
            }

            try
            {
                var sale = _mapper.Map<Sale>(saleDto);
                var createdSale = await _saleService.Create(sale);
                return Ok(new { StatusCode = 200, message = "Registro creado con éxito.", data = _mapper.Map<SaleDto>(createdSale) });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { statusCode = 400, message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.Message });
            }
        }

        [HttpPut("{saleId}/annular")]
        public async Task<IActionResult> CancelSale(int saleId, [FromBody] CancelSaleDto cancelSaleDto)
        {
            if (cancelSaleDto.Reason == null || cancelSaleDto.UserId <= 0)
            {
                return BadRequest(new { statusCode = 400, message = "El ID del usuario y el motivo son obligatorios para anular la venta." });
            }

            try
            {
                var canceledSale = await _saleService.CancelSale(saleId, cancelSaleDto.Reason, cancelSaleDto.UserId);
                if(canceledSale == null)
                {
                    return NotFound(new { statusCode = 404, message = "Venta no encontrada." });

                }
                return Ok(new { StatusCode = 200, message = "Venta anulada con éxito.", data = _mapper.Map<SaleDto>(canceledSale) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al anular la venta.", error = ex.Message });
            }
        }

        [HttpGet("{saleId}/details")]
        public async Task<ActionResult<IEnumerable<SaleDetailDto>>> GetDetailsBySaleId(int saleId)
        {
            try
            {
                var saleDetails = await _saleService.GetDetailsBySaleId(saleId);
                if (saleDetails.Count == 0 || saleDetails == null)
                {
                    return NotFound(new { statusCode = 404, message = "No se encontraron detalles para la venta solicitada." });
                }

                var saleDetailsDto = _mapper.Map<List<SaleDetailDto>>(saleDetails);
                return Ok(saleDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los detalles de la venta.", ex.Message });
            }
        }
    }
}
