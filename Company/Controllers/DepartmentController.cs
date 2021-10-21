using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Company.Models;
using Company.Services;
using Newtonsoft.Json;

namespace Company.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DepartmentController : ControllerBase
    {
        public DepartmentController()
        {
        }

        [HttpGet]
        public ActionResult<List<Department>> GetAll() => DepartmentService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<List<Department>> Get(int id)
        {
            var department = DepartmentService.Get(id);

            if(department == null)
                return NotFound();

            return department;
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            // Department department = JsonConvert.DeserializeObject<Department>(input);
            DepartmentService.Add(department);
            return CreatedAtAction(nameof(Create), new {id=department.DepartmentId, name=department.DepartmentName}, department);
        }
        
        [HttpPut]
        public IActionResult Update(Department department)
        {
            DepartmentService.Update(department);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            DepartmentService.Delete(id);
            return NoContent();
        }
    }
}