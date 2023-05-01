using lab_2.BL.DTOs.Ticket;
using lab_2.BL.DTOs.Tickets;
using lab_2.DAL.Data.Models;
using lab_2.DAL.Repos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.Managers.Tickets;

public class TicketsManager:ITicketsManager
{
    private readonly ITicketsRepo _TicketsRepo;

    public TicketsManager(ITicketsRepo TicketsRepo)
    {
        _TicketsRepo = TicketsRepo;
    }   
    
    public List<TicketReadDto> GetAll()
    {
        IEnumerable<Ticket> TicketsFromDb = _TicketsRepo.GetAll();
        return TicketsFromDb.Select(d => new TicketReadDto
        {
            Id = d.Id,
            Title = d.Title,
            Description = d.Description,
        }).ToList();
    }

    public int Add(TicketAddDto ticketAddDTO)
    {
        Ticket ticket = new Ticket
        {
            Description = ticketAddDTO.Description,
            Title = ticketAddDTO.Title,
            DepartmentId = ticketAddDTO.DepartmentId,
        };
        _TicketsRepo.Add(ticket);
        _TicketsRepo.SaveChanges();
        return ticket.Id;
    }

    TicketReadDto? ITicketsManager.GetById(int id)
    {
        Ticket? ticket = _TicketsRepo.GetById(id);
        if (ticket != null)
        {
            return new TicketReadDto
            {
                Id = ticket.Id,
                Description = ticket.Description,
                Title = ticket.Title,
            };
        }
        else { return null; }
    }

   

    public bool Update(TicketsUpdateDto ticketsUpdateDTO)
    {
        Ticket? ticketFromDb = _TicketsRepo.GetById(ticketsUpdateDTO.Id);
        if (ticketFromDb != null)
        {
            ticketFromDb.Title = ticketsUpdateDTO.Title;
            ticketFromDb.Description = ticketsUpdateDTO.Description;
            _TicketsRepo.Update(ticketFromDb);
            _TicketsRepo.SaveChanges();
            return true;
        }
        else return false;
    }

    public void Delete(int Id)
    {
        Ticket? ticketFromDb = _TicketsRepo.GetById(Id);
        if (ticketFromDb != null)
        {
            _TicketsRepo.Delete(ticketFromDb);
            _TicketsRepo.SaveChanges();
        }
        else return;
    }
}
