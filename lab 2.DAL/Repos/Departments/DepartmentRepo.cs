using lab_2.DAL.Data.Context;
using lab_2.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Repos.Departments;

public class DepartmentRepo:IDepartmentRepo
{
    private readonly DBContext _context;

    public DepartmentRepo(DBContext context)
    {
        _context = context;
    }
    //public IEnumerable<Department> GetAll()
    //{
    //    return _context.Set<Department>().AsNoTracking();
    //}
    //public Department? GetById(int id)
    //{
    //    return _context.Set<Department>().Find(id);
    //}
    public Department? GetDepartmentWithTicketsAndDevelopers(int id)
    {
        return _context.Departments
            .Include(d => d.Tickets)
                .ThenInclude(p => p.Developers)
            .FirstOrDefault(d => d.Id == id);
    }


}
