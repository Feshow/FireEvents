using FireEvents.Domain.Entities;
using FireEvents.Domain.Interfaces.IServices;
using FireEvents.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FireEventsAPI.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        protected ResponseApi _response;
        public EventoController(IEventoService eventoRepository)
        {
            _eventoService = eventoRepository;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetAllEventos()
        {
            try
            {
                IEnumerable<Evento>? eventoList;
                eventoList = await _eventoService.ObterEventosAsync();

                _response.Result = eventoList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetEventoById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _eventoService.ObterEventoPorIdAsync(id);

                if (response == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound();
                }

                _response.Result = response;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("tema/{tema}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetEventoByTema(string tema)
        {
            try
            {
                if (string.IsNullOrEmpty(tema))
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _eventoService.ObterEventoPorTemaAsync(tema);

                if (response == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound();
                }

                _response.Result = response;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> CreateEvento([FromBody] Evento modelEvento)
        {
            try
            {
                var response = await _eventoService.AddEvento(modelEvento);

                if (response == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound();
                }

                _response.Result = response;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseApi>>UpdateEvento(int id, [FromBody] Evento modelEvento)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _eventoService.UpdateEvento(id, modelEvento);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]

        public async Task<ActionResult<ResponseApi>>DeleteEvento(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _eventoService.DeleteEvento(id);
                if (response)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotModified;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }                

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
