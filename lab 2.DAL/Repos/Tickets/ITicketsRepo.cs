using lab_2.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Repos.Tickets;

public interface ITicketsRepo
{
    IEnumerable<Ticket> GetAll();
    Ticket? GetById(int id);
    void Add(Ticket entity);
    void Update(Ticket entity);
    void Delete(Ticket entity);

    int SaveChanges();
}
