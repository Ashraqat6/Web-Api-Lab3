using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.DTOs.Tickets;

public class TicketChildReadDto
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public int DevelopersCount { get; set; }
}
