using lab_2.BL.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2.BL.Managers.Departement;

public interface IDepartmentsManager
{
    DepartmentReadDetailsDto? GetDetails(int id);
}
