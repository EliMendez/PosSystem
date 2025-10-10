using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using PosSystem.Service.Service;

namespace PosSystem.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(RoleService roleService, IMapper mapper)
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
                    return NotFound(new { statusCode = 404, message = "Registro no encontrado." });
                }
                return Ok(_mapper.Map<RoleDto>(role));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al obtener el registro.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto roleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDto);
                var createdRole = await _roleService.Create(role);
                return Ok(new {StatusCode = 200, message = "Registro creado con éxito.", data = _mapper.Map<RoleDto>(createdRole)});
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

                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al crear el registro.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDto roleDto)
        {
            try
            {
                if(id != roleDto.RoleId)
                {
                    return BadRequest(new { statusCode = 400, message = "El ID del registro no coincide."});
                }

                var role = _mapper.Map<Role>(roleDto);
                var updatedRole = await _roleService.Update(role);
                return Ok(new { StatusCode = 200, message = "Registro actualizado con éxito.", data = _mapper.Map<RoleDto>(updatedRole) });
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

                return StatusCode(500, new { statusCode = 500, message = "Error al actualizar el registro.", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al actualizar el registro.", error = ex.Message });
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
                    return NotFound(new { StatusCode = 404, message = "Registro no encontrado."});
                }
                return Ok(new { StatusCode = 200, message = "Registro eliminado satisfactoriamente."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al eliminar el registro.", error = ex.Message });
            }
        }
    }
}
