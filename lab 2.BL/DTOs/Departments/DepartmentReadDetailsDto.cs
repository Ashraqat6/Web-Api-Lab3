using lab_2.BL.DTOs.Ticket;
using lab_2.BL.DTOs.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.DTOs.Department;

public class DepartmentReadDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<TicketChildReadDto> Tickets { get; set; } = new ();
}
