using lab_2.BL.DTOs.Department;
using lab_2.BL.Managers.Departement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsManager _DepartmentsManager;

        public DepartmentsController(IDepartmentsManager departmentsManager)
        {
            _DepartmentsManager = departmentsManager;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<DepartmentReadDetailsDto> GetDetails(int id)
        {
            DepartmentReadDetailsDto? department = _DepartmentsManager.GetDetails(id);
            if (department is null)
            {
                return NotFound();
            }
            return department; //Status 200 Ok
        }
    }
}
