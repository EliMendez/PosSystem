using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Dto.Validators;
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
                    return NotFound(new { StatusCode =404, Message ="No existe ningún registro para la consulta solicitada."});
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
            var validator = new SaleDtoValidator();
            var validationResult = await validator.ValidateAsync(saleDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var sale = _mapper.Map<Sale>(saleDto);
                var createdSale = await _saleService.Create(sale);
                return Ok(new { StatusCode = 200, Message = "Registro creado con éxito.", Data = _mapper.Map<SaleDto>(createdSale) });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.Message });
            }
        }

        [HttpPut("{saleId}/annular")]
        public async Task<IActionResult> CancelSale(int saleId, [FromBody] CancelSaleDto cancelSaleDto)
        {
            if (cancelSaleDto.Reason == null || cancelSaleDto.UserId <= 0)
            {
                return BadRequest(new { StatusCode = 400, Message = "El ID del usuario y el motivo son obligatorios para anular la venta." });
            }

            try
            {
                var canceledSale = await _saleService.CancelSale(saleId, cancelSaleDto.Reason, cancelSaleDto.UserId);
                if(canceledSale == null)
                {
                    return NotFound(new { StatusCode =404, Message ="Venta no encontrada." });

                }
                return Ok(new { StatusCode = 200, Message = "Venta anulada con éxito.", Data = _mapper.Map<SaleDto>(canceledSale) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al anular la venta.", Error = ex.Message });
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
                    return NotFound(new { StatusCode =404, Message ="No se encontraron detalles para la venta solicitada." });
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
