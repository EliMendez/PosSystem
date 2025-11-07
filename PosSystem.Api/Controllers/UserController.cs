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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                var usersDto = _mapper.Map<List<UserDto>>(users);
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el listado de registros.", ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user == null)
                {
                    return NotFound(new { StatusCode =404, Message ="Registro no encontrado." });
                }
                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al obtener el registro.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            var validator = new UserDtoValidator();
            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                var createdUser = await _userService.Create(user, userDto.Password);
                return Ok(new { StatusCode = 200, Message = "Registro creado con éxito.", Data = _mapper.Map<UserDto>(createdUser) });
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
                        if (constraintName.Contains("IX_Products_UserName"))
                        {
                            duplicateField = "el correo electrónico";
                        }else if (constraintName.Contains("IX_Users_Phone"))
                        {
                            duplicateField = "el télefono";
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
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            if (id <= 0)
                return BadRequest(new { StatusCode = 400, Message = "ID no puede ser menor o igual a cero." });

            var validator = new UserDtoValidator();
            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { StatusCode = 400, Message = "Los datos proporcionados no son válidos.", errors });
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                var updatedUser = await _userService.Update(user, userDto.Password);
                return Ok(new { StatusCode = 200, Message = "Registro actualizado con éxito.", Data = _mapper.Map<UserDto>(updatedUser) });
            }
            catch (KeyNotFoundException ex)
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
                        //if (constraintName.Contains("IX_Users_Username"))
                        //{
                        //    duplicateField = "el correo electrónico";
                        //}
                        //else 
                        if (constraintName.Contains("IX_Users_Phone"))
                        {
                            duplicateField = "el télefono";
                        }
                    }

                    return BadRequest(new { StatusCode = 400, Message = $"Ya existe un registro con {duplicateField} ingresado. Inténtelo de nuevo." });
                }

                return StatusCode(500, new { StatusCode = 500, Message = $"Error al actualizar el registro, Error ={ex.InnerException?.Message ?? ex.Message}" });
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
                var result = await _userService.Delete(id);
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

        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetByRoleId(int id)
        {
            try
            {
                var user = await _userService.GetRoleById(id);
                if (string.IsNullOrEmpty(user))
                {
                    return NotFound(new { StatusCode = 404, Message ="Registro no encontrado." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Error al obtener el registro.", Error = ex.Message });
            }
        }
    }
}
