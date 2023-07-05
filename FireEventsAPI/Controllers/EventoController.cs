using FireEvents.Data;
using FireEventsAPI.Models.Evento;
using Microsoft.AspNetCore.Mvc;

namespace FireEventsAPI.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EventoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetEventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EventoModel>> Get()
        {
            return Ok(_db.Eventos);
        }

        [HttpGet("{id:int}", Name = "GetEvento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventoModel> GetEventos(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var evento = _db.Eventos.FirstOrDefault(s => s.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }
    }
}
