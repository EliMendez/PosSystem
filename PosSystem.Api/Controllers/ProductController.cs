using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Dto.Validators;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PosSystem.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;

        public ProductController(IService<Product> productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAll();
                var productsDto = _mapper.Map<List<ProductDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el listado de registros.", ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Registro no encontrado." });
                }
                return Ok(_mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al obtener el registro.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            var validator = new ProductDtoValidator();
            var validationResult = await validator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var product = _mapper.Map<Product>(productDto);
                var createdProduct = await _productService.Create(product);
                return Ok(new { StatusCode = 200, Message = "Registro creado con éxito.", Data = _mapper.Map<ProductDto>(createdProduct) });
            }
            catch (DbUpdateException ex)
            {
                // Intentar obtener la SqlException directamente o desde una InnerException
                SqlException? sqlEx = ex.InnerException as SqlException ??
                                      (ex.InnerException?.InnerException as SqlException);

                // Los códigos de error 2627 y 2601 indican una violación de clave duplicada.
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    string duplicateField = "un valor único";

                    // SqlException.Message o a menudo contienen el nombre del índice o la restricción violada.
                    string constraintName = sqlEx.Message;

                    if (!string.IsNullOrEmpty(constraintName))
                    {
                        if (constraintName.Contains("IX_Products_barcode"))
                        {
                            duplicateField = "el código de barra";
                        }
                        else if (constraintName.Contains("IX_Products_description"))
                        {
                            duplicateField = "el nombre";
                        }
                    }

                    return BadRequest(new { StatusCode = 400, Message = $"Ya existe un registro con {duplicateField} ingresado. Inténtelo de nuevo." });
                }

                return StatusCode(500, new { StatusCode = 500, Message = $"Error al crear el registro, Error ={ex.InnerException?.Message ?? ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id <= 0)
                 return BadRequest(new { StatusCode = 400, Message = "ID no puede ser menor o igual a cero." });

            var validator = new ProductDtoValidator();
            var validationResult = await validator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var product = _mapper.Map<Product>(productDto);
                var updatedProduct = await _productService.Update(product);
                return Ok(new { StatusCode = 200, Message = "Registro actualizado con éxito.", Data = _mapper.Map<ProductDto>(updatedProduct) });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { StatusCode =404, Error = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                // Intentar obtener la SqlException directamente o desde una InnerException
                SqlException? sqlEx = ex.InnerException as SqlException ??
                                      (ex.InnerException?.InnerException as SqlException);

                // Los códigos de error 2627 y 2601 indican una violación de clave duplicada.
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    string duplicateField = "un valor único";

                    // SqlException.Message o a menudo contienen el nombre del índice o la restricción violada.
                    string constraintName = sqlEx.Message;

                    if (!string.IsNullOrEmpty(constraintName))
                    {
                        if (constraintName.Contains("IX_Products_barcode"))
                        {
                            duplicateField = "el código de barra";
                        }
                        else if (constraintName.Contains("IX_Products_description"))
                        {
                            duplicateField = "el nombre";
                        }
                    }

                    return BadRequest(new { StatusCode = 400, Message = $"Ya existe un registro con {duplicateField} ingresado. Inténtelo de nuevo." });
                } 

                return StatusCode(500, new { StatusCode = 500, Message = $"Error al crear el registro, Error ={ex.InnerException?.Message ?? ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.Delete(id);
                if (!result)
                {
                    return NotFound(new { StatusCode = 404, Message ="Registro no encontrado." });
                }
                return Ok(new { StatusCode = 200, Message = "Registro eliminado satisfactoriamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al eliminar el registro.", Error = ex.Message });
            }
        }
    }
}
