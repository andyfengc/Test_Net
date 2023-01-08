using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Mvc.Models.Northwind
{
    public class Region
    {
        public Region()
        {
            this.Territories = new HashSet<Territories>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // important
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<Territories> Territories { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }
            var anotherObj = obj as Region;
            return this.RegionDescription == anotherObj.RegionDescription;
        }

        public override int GetHashCode()
        {
            return new {this.RegionDescription}.GetHashCode();
        }
    }
}