using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Schedulers
{
    public class PrintJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (var sw = File.AppendText("c:/delete/test.net.txt"))
            {
                await sw.WriteLineAsync($"printjob - {DateTime.Now}");
            }
        }
    }
}