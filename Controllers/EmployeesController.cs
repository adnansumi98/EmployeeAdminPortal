using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly ApplicationDbContext dbContext;

    public EmployeesController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
      var allEmployees = dbContext.Employees.ToList();
      return Ok(allEmployees);
    }

    [HttpPost]
    public IActionResult AddEmployee([FromBody] Employee employee)
    {
      dbContext.Employees.Add(employee);
      dbContext.SaveChanges();
      return Ok();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployeeById(Guid id)
    {
      var employee = dbContext.Employees.Find(id);
      if (employee == null)
      {
        return NotFound();
      }
      return Ok(employee);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployee([FromBody] Employee employee, Guid id)
    {
      var employeeToUpdate = dbContext.Employees.Find(id);
      if (employeeToUpdate == null)
      {
         return NotFound();
      }
      employeeToUpdate.Name = employee.Name;
      employeeToUpdate.Email = employee.Email;
      employeeToUpdate.phone = employee.phone;
      employeeToUpdate.salary = employee.salary;  
      
      dbContext.SaveChanges();
      return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployee(Guid id)
    {
      var employeeToDelete = dbContext.Employees.Find(id);
      if (employeeToDelete == null)
      {
        return NotFound();
      }
      dbContext.Employees.Remove(employeeToDelete);
      dbContext.SaveChanges();
      return Ok();
    }
  }
}
