
using lab_2.BL.DTOs.Department;
using lab_2.BL.DTOs.Tickets;
using lab_2.DAL.Data.Models;
using lab_2.DAL.Repos.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.Managers.Departement;

public class DepartmentsManager:IDepartmentsManager
{
    private readonly IDepartmentRepo _DepartmentsRepo;

    public DepartmentsManager(IDepartmentRepo DepartmentsRepo)
    {
        _DepartmentsRepo = DepartmentsRepo;
    }
    
    DepartmentReadDetailsDto? IDepartmentsManager.GetDetails(int id)
    {
        Department? department = _DepartmentsRepo.GetDepartmentWithTicketsAndDevelopers(id);
        if (department is null)
        {
            return null;
        }

        return new DepartmentReadDetailsDto
        {
            Id = department.Id,
            Name = department.Name,

            Tickets = department.Tickets
                .Select(p => new TicketChildReadDto
                {
                    Id = p.Id,
                    Description = p.Description,
                    DevelopersCount = p.Developers.Count
                }).ToList()
        };
    }
}
