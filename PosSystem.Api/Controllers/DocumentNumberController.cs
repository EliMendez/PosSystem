using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;

namespace PosSystem.Api.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentNumberController : ControllerBase
    {
        private readonly IDocumentNumberService _documentNumberService;
        private readonly IMapper _mapper;

        public DocumentNumberController(IDocumentNumberService documentNumberService, IMapper mapper)
        {
            _documentNumberService = documentNumberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentNumber>>> Get()
        {
            try
            {
                var documentNumber = await _documentNumberService.Get();
                if(documentNumber == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<DocumentNumberDto>(documentNumber));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { statusCode = 500, message = "Error al obtener el registro.", error = ex.Message });
            }
        }
    }
}
