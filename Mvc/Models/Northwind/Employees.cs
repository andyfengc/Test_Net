using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Northwind
{
    public class Employees
    {
        public Employees()
        {
            this.Territories = new HashSet<Territories>();
        }
        [Key]
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public virtual ICollection<Territories> Territories { get; set; }
    }
}