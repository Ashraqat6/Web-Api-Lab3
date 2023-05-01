using lab_2.BL.DTOs.Ticket;
using lab_2.BL.DTOs.Tickets;
using lab_2.DAL.Repos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.Managers.Tickets;

public interface ITicketsManager
{
    List<TicketReadDto> GetAll();
    int Add(TicketAddDto ticketAddDTO);
    void Delete(int Id);
    TicketReadDto? GetById(int id);
    bool Update(TicketsUpdateDto ticketsUpdateDTO);
}
