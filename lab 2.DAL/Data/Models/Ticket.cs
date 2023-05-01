using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Data.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    public Department? Department { get; set;}
    public ICollection<Developer> Developers { get; set; } = new HashSet<Developer>();
    public int DepartmentId { get; set; }
}
