using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Console = System.Console;

namespace Console.Csv
{
    public class CsvTest
    {
        public static IEnumerable<dynamic> GetAllRecords()
        {
            // get csv data
            var path = Directory.GetCurrentDirectory(); //c:\\Work\\Workspace\\Test_.Net\\Console\\bin\\Debug
            path = path.Replace(@"\bin\Debug", @"\Csv\data1.csv");
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.PrepareHeaderForMatch = header => header.ToUpper();
                csv.Configuration.RegisterClassMap<FactoryShippingClassMap>();
                var records = csv.GetRecords<FactoryShipping>();
                foreach (var record in records)
                {
                    System.Console.WriteLine(record);
                }
            }
            return null;
        } 
    }
}
