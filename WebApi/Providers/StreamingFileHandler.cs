using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Providers
{
    //https://blogs.visoftinc.com/2013/03/26/streaming-large-files-asynchronously-using-net-4-5/
    public class StreamingFileHandler : HttpTaskAsyncHandler
    {
        public override bool IsReusable
        {
            get { return true; }
        }

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            //http://localhost:45505/postfile?param1=joey
            // get param context.Request.Params["param1"]
            //after this line, we depart the handling thread and continue work on a threadpool thread.
            var result = await TransferFileAsync(context);

            //we pick up here on another thread to return to the client
            context.Response.ContentType = "text/plain";
            using (var writer = new StreamWriter(context.Response.OutputStream))
            {
                writer.Write(result ? "Success" : "Fail");
                writer.Flush();
            }
        }

        private async Task<bool> TransferFileAsync(HttpContext context)
        {
            string tempFilePath = null;

            try
            {
                //if (context.Request.ContentType != "application/xml" && context.Request.ContentType != "text/xml")
                //throw new Exception("Content-Type must be either 'application/xml' or 'text/xml'.");

                tempFilePath = String.Format(@"c:/delete/temp/outputnewfile{0}.txt", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                using (var reader = new StreamReader(context.Request.GetBufferlessInputStream(true)))
                using (var filestream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true))
                using (var writer = new StreamWriter(filestream))
                {
                    var dataToWrite = await reader.ReadToEndAsync();
                    await writer.WriteAsync(dataToWrite);

                    return true;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                return false;
            }
        }
    }
}