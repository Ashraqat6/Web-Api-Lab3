using lab_2.BL.DTOs.Ticket;
using lab_2.BL.DTOs.Tickets;
using lab_2.BL.Managers.Tickets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsManager ticketManager;

        public TicketsController(ITicketsManager ticketManager)
        {
            this.ticketManager = ticketManager;
        }
        [HttpGet]
        public ActionResult<List<TicketReadDto>> GetAll()
        {
            return ticketManager.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<TicketReadDto> GetById(int id)
        {
            TicketReadDto? ticketReadDTO = ticketManager.GetById(id);
            if (ticketReadDTO != null)
            {
                return ticketReadDTO;
            }
            else
                return NotFound();
        }
        [HttpPost]
        public ActionResult Add(TicketAddDto ticketAdd)
        {
            int newId = ticketManager.Add(ticketAdd);
            return CreatedAtAction("GetById", 
                new { id = newId }, 
                new { Message = "Added Successfully" });
        }
        [HttpPut]
        public ActionResult Update(TicketsUpdateDto ticketsUpdateDTO)
        {
            bool isFound = ticketManager.Update(ticketsUpdateDTO);
            if (!isFound)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ticketManager.Delete(id);
            return NoContent();
        }
    }
}
