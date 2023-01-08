using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace BlazorApp4.Server.Controllers
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
                new Employee(){ Id = 1, Name="Andy", Email="andy@rbc.com", EmployeeNumber = "EMP70001"},
                  new Employee(){ Id = 2,Name="Tony", Email="tony@rbc.com", EmployeeNumber = "EMP70002"},
                new Employee(){Id = 3, Name="Steven", Email="steven@rbc.com", EmployeeNumber = "EMP70003"},
                new Employee(){Id = 4, Name="Mark", Email="mark@rbc.com", EmployeeNumber = "EMP70004"}
          };
        }
    }
}
