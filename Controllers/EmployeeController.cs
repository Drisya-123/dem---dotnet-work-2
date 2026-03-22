using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Models;
using EmployeeAPI.Data;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null)
                return NotFound();

            return Ok(emp);
        }

        // ✅ POST (ADD)
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();

            return Ok(emp);
        }

        // ✅ PUT (UPDATE)
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updated)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null)
                return NotFound();

            emp.Name = updated.Name;
            emp.Department = updated.Department;
            emp.Salary = updated.Salary;

            _context.SaveChanges();

            return Ok(emp);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var emp = _context.Employees.Find(id);

            if (emp == null)
                return NotFound();

            _context.Employees.Remove(emp);
            _context.SaveChanges();

            return Ok("Deleted successfully");
        }
    }
}