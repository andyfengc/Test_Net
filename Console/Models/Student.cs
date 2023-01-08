using System;

namespace Console.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public Int16 Age { get; set; }

        public virtual StudentDetail StudentDetail { get; set; }
    }
}
