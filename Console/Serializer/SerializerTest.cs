using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Console.Context;

namespace Console.Serializer
{
    public class SerializerTest
    {
        private const string filepath = "c:/delete/students.xml";
        public static void Serialize()
        {
            var data = new List<Student>();
            data.Add(new Student() { Name = "John", Age = 39 });
            data.Add(new Student() {Name = "Andy" , Age = 25});
            using (var writer = new StreamWriter(filepath, false))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(writer, data);
            }
        }

        public static void Deserialize()
        {
            using (var writer = new StreamReader(filepath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                var students = (List<Student>) serializer.Deserialize(writer);
                foreach (var student in students)
                {
                    System.Console.WriteLine(student);
                }
            }
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return this.Name + " " + this.Age;
        }
    }

}
