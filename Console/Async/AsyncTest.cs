using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Json;

namespace Console.Async
{
    public class AsyncTest
    {
        public static void Test()
        {
            var rw = new ResearchWork();
            var e1 = rw.DoResearchAsync();
            System.Console.WriteLine("finish main thread");
            System.Console.ReadKey();
        }
        
    }

    public class ResearchWork
    {
        public async Task<Employee> DoResearchAsync()
        {
            System.Console.WriteLine("Start async research...");
            await Task.Delay(5000);
            System.Console.WriteLine("Finish async research...");
            return new Employee() {DateOfBirth = DateTime.Now, Name = "Julie", IsMale = false, Salary = 3000, Id = 1};
        }


    }
}
