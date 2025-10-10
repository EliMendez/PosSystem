using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;
using PosSystem.Service.Service;

namespace PosSystem.Api.Controllers
{
    [Route("api/business")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly IMapper _mapper;
        public BusinessController(IBusinessService businessService, IMapper mapper) 
        {
            _businessService = businessService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Business>> Get()
        {
            try
            {
                var business = await _businessService.Get();
                if (business == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<BusinessDto>(business));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al obtener el registro.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BusinessDto businessDto)
        {
            try
            { 
                if(businessDto == null)
                {
                    return BadRequest(new { statusCode = 400, message = "Los datos de la empresa son inválidos." });
                }

                var business = _mapper.Map<Business>(businessDto);
                var createdBusiness = await _businessService.Save(business);
                return Ok(new { StatusCode = 200, message = "Registro creado con éxito.", data = _mapper.Map<BusinessDto>(createdBusiness) });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    return BadRequest(new { statusCode = 400, message = "Ya existe un registro con el mismo RUC. Inténtelo de nuevo."});
                }

                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.Message });
            }
        }


    }
}
