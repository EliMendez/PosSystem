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
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IService<Role> _roleService;
        private readonly IMapper _mapper;

        public RoleController(IService<Role> roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            try
            {
                var roles = await _roleService.GetAll();
                var rolesDto = _mapper.Map<List<RoleDto>>(roles);
                return Ok(rolesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el listado de registros.", ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            try
            {
                var role = await _roleService.GetById(id);
                if(role == null)
                {
                    return NotFound(new { StatusCode =404, Message ="Registro no encontrado." });
                }
                return Ok(_mapper.Map<RoleDto>(role));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al obtener el registro.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto roleDto)
        {
            var validator = new RoleDtoValidator();
            var validationResult = await validator.ValidateAsync(roleDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var role = _mapper.Map<Role>(roleDto);
                var createdRole = await _roleService.Create(role);
                return Ok(new {StatusCode = 200, Message ="Registro creado con éxito.", Data = _mapper.Map<RoleDto>(createdRole)});
            }
            catch(DbUpdateException ex)
            {
                if(ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Ya existe un registro con la misma descripción o clave única. Inténtelo de nuevo."
                    });
                }

                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.InnerException?.Message ?? ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al crear el registro.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDto roleDto)
        {
            if (id <= 0)
                return BadRequest(new { StatusCode = 400, Message = "ID no puede ser menor o igual a cero." });

            var validator = new RoleDtoValidator();
            var validationResult = await validator.ValidateAsync(roleDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }
            
            try
            {
                if(id != roleDto.RoleId)
                {
                    return BadRequest(new { StatusCode = 400, Message = "El ID del registro no coincide."});
                }

                var role = _mapper.Map<Role>(roleDto);
                var updatedRole = await _roleService.Update(role);
                return Ok(new { StatusCode = 200, Message = "Registro actualizado con éxito.", Data = _mapper.Map<RoleDto>(updatedRole) });
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
                var result = await _roleService.Delete(id);
                if(!result)
                {
                    return NotFound(new { StatusCode = 404, Message ="Registro no encontrado."});
                }
                return Ok(new { StatusCode = 200, Message = "Registro eliminado satisfactoriamente."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al eliminar el registro.", Error = ex.Message });
            }
        }
    }
}
