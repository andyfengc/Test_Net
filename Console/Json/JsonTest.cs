using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Console.Json
{
    public class JsonTest
    {
        public static void Serialize()
        {
            var employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "John", DateOfBirth = new DateTime(1980, 1, 10), Salary = 5000.00, IsMale = true},
                new Employee() { Id = 2, Name = "Lily", DateOfBirth = new DateTime(1989, 3, 1), Salary = 4000.00, IsMale = false}
            };
            string jsonString = JsonConvert.SerializeObject(employees);
            System.Console.Write(jsonString);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Salary { get; set; }
        public bool IsMale { get; set; }
    }
}
