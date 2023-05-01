using lab_2.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.DAL.Repos.Departments;

public interface IDepartmentRepo
{
    //IEnumerable<Department> GetAll();
    //Department? GetById(int id);
    Department? GetDepartmentWithTicketsAndDevelopers(int id);

}
