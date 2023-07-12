using FireEvents.Domain.Entities;
using FireEvents.Domain.Interfaces.IRepository;
using FireEvents.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FireEventsAPI.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _dbEvento;
        protected ResponseApi _response;
        public EventoController(IEventoRepository eventoRepository)
        {
            _dbEvento = eventoRepository;
            _response = new();
        }

        [HttpGet(Name = "GetAllEventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetAllEventos(bool includePalestrantes)
        {
            try
            {
                IEnumerable<Evento> eventoList;
                eventoList = await _dbEvento.GetAllEventosAsync(includePalestrantes);

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

        [HttpGet("{id:int}", Name = "GetEventoById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetEventoById(int id, bool includePalestrantes)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _dbEvento.GetEventoByIdAsync(id, includePalestrantes);

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

        [HttpGet("{id:int}", Name = "GetEventoByTema")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> GetEventoByTema(string tema, bool includePalestrantes)
        {
            try
            {
                if (string.IsNullOrEmpty(tema))
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var response = await _dbEvento.GetEventosByTemaAsync(tema, includePalestrantes);

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseApi>> Create([FromBody] Evento modelCreator)
        {
            try
            {
                if (modelCreator == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound();
                }

                _response.Result = modelCreator;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("Get by Id", new { id = modelCreator.Id }, _response); //After create the object, it gerates the route where we can acesss the objet by id (Invoke GetById);
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
