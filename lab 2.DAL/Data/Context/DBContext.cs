using lab_2.DAL.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Data.Context;

public class DBContext: IdentityDbContext<Employee>
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }
    public DbSet<Department> Departments=>Set<Department>();
    public DbSet<Ticket> Tickets =>Set<Ticket>();
    public DbSet<Developer> Developers =>Set<Developer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Employee>().ToTable("Employees");
    }

}
