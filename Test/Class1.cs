using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class MultiSelectViewModel<TProperty>
    {
        public IEnumerable<TProperty> PostedProperties { get; set; }
        public IEnumerable<TProperty> AllProperties { get; set; }
        public IEnumerable<TProperty> SelectedProperties { get; set; }

        public int Add(int x, int y)
        {
            int result = 0;
            try
            {
                System.Console.WriteLine("begin execution in try block of Add()...");
                var parameters = string.Format("input parameters x={0}, y={1}", x, y);
                System.Console.WriteLine(parameters);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                result = x + y;
                sw.Stop();
                System.Console.WriteLine("time elapsed {0}", sw.Elapsed);
                var returnValue = string.Format("result {0}", result);
                System.Console.WriteLine(returnValue);
                System.Console.WriteLine("end execution in try block of Add()");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("exception happens: {0}", ex.Message));
            }
            finally
            {
                System.Console.WriteLine("end execution in final block of Add()");
            }
            return result;
        }
    }
}
