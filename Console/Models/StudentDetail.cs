using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Models
{
    public class StudentDetail
    {
        // way 1 to configure one-to-one, annotation
        //[Key, ForeignKey("Student")]
        public int Id { get; set; }
        public DateTime Dob { get; set; }
        public byte[] Photo { get; set; }

        public virtual Student Student { get; set; }
    }
}
