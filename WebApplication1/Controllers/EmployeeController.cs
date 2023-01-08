using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("employees")]
        public IEnumerable<Employee> GetEmployees()
        {
            //...
            return new List<Employee>()
            {
                new Employee(){ Name="Andy", Email="andy@rbc.com", EmployeeNumber = "EMP70001"},
                  new Employee(){ Name="Tony", Email="tony@rbc.com", EmployeeNumber = "EMP70001"},
                new Employee(){ Name="Steven", Email="steven@rbc.com", EmployeeNumber = "EMP70001"},
                new Employee(){ Name="Mark", Email="mark@rbc.com", EmployeeNumber = "EMP70001"}
          };
        }
    }
}
