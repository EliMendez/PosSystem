using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Dto.Validators;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;

namespace PosSystem.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IService<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAll();
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "Error al obtener el listado de registros.", ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                {
                    return NotFound(new { statusCode = 404, message = "Registro no encontrado." });
                }
                return Ok(_mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al obtener el registro.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            var validator = new CategoryDtoValidator();
            var validationResult = await validator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                var createdCategory = await _categoryService.Create(category);
                return Ok(new { StatusCode = 200, Message = "Registro creado con éxito.", Data = _mapper.Map<CategoryDto>(createdCategory) });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Ya existe un registro con la misma descripción o clave única. Inténtelo de nuevo."
                    });
                }

                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id <= 0)
                return BadRequest(new { StatusCode = 400, Message = "ID no puede ser menor o igual a cero." });

            var validator = new CategoryDtoValidator();
            var validationResult = await validator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                if (id != categoryDto.CategoryId)
                {
                    return BadRequest(new { StatusCode = 400, Message = "El ID del registro no coincide." });
                }

                var category = _mapper.Map<Category>(categoryDto);
                var updatedCategory = await _categoryService.Update(category);
                return Ok(new { StatusCode = 200, Message = "Registro actualizado con éxito.", Data = _mapper.Map<CategoryDto>(updatedCategory) });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Ya existe un registro con la misma descripción. Inténtelo de nuevo."
                    });
                }

                return StatusCode(500, new { StatusCode = 500, Message = "Error al actualizar el registro.", Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al actualizar el registro.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
                if (!result)
                {
                    return NotFound(new { StatusCode = 404, Message = "Registro no encontrado." });
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
