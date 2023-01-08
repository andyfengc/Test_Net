using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Mvc.Models.Northwind
{
    public class Territories
    {
        public Territories()
        {
            this.Employees = new HashSet<Employees>();
        }
        [Key]
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        [ForeignKey("RegionID")]
        public Region Region { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}