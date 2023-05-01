using lab_2.DAL.Data.Context;
using lab_2.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Repos.Tickets;

public class TicketsRepo : ITicketsRepo
{
    private readonly DBContext _context;

    public TicketsRepo(DBContext context)
    {
        _context = context;
    }
    public IEnumerable<Ticket> GetAll()
    {
        //return _context.Tickets;
        return _context.Set<Ticket>().AsNoTracking(); //ReadOnly
    }

    public Ticket? GetById(int id)
    {
        return _context.Set<Ticket>().Find(id);
        //VS => return _context.Set<Ticket>().FirstOrDefault(id);
    }

    public void Add(Ticket entity)
    {
        _context.Set<Ticket>().Add(entity);
        //_context.Tickets.Add(entity);
    }

    public void Update(Ticket entity)
    {
    }

    public void Delete(Ticket entity)
    {
        _context.Set<Ticket>().Remove(entity);
        //_context.Tickets.Remove(entity);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
