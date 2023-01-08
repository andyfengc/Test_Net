using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UploadController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Json("Ok");
        }

        [Route("upload-local")]
        [HttpPost]
        public HttpResponseMessage UploadFileToLocalServer()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    //var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    var filePath = "c:/delete/temp/" + postedFile.FileName;
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [Route("files")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadFilesAsFile()
        {
            // validate
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            // upload
            var streamProvider = new MultipartFormDataStreamProvider(@"c:\temp");
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            return new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };
        }

        [Route("upload-file-stream")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFileStream([FromUri]string name)
        {
            try
            {
                string file = Path.Combine(@"c:\delete\temp", name);
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write,
                    FileShare.None, 4096, useAsync: true))
                {
                    await Request.Content.CopyToAsync(fs);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
                return BadRequest("ERROR: could not be saved on server.");
            }
        }

        [Route("upload-memory")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<FileResult> UploadFilesAsMemory()
        {
            // validate
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            // upload
            var streamProvider = new MultipartFormDataStreamProvider(@"c:\delete\temp");
            try
            {
                Stream reqStream = Request.Content.ReadAsStreamAsync().Result;
                var tempStream = new MemoryStream();
                reqStream.CopyTo(tempStream);
                tempStream.Position = 0;

                var streamContent = new StreamContent(tempStream);
                foreach (var header in Request.Content.Headers)
                {
                    streamContent.Headers.Add(header.Key, header.Value);
                }

                await streamContent.ReadAsMultipartAsync(streamProvider);
                return new FileResult
                {
                    FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                    Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                    Description = streamProvider.FormData["description"],
                    CreatedTimestamp = DateTime.UtcNow,
                    UpdatedTimestamp = DateTime.UtcNow,
                };
            }
            catch (System.Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("upload-memory2")]
        public async Task<FileResult> UploadFilesViaMemory2()
        {
            // validate
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            // read as stream
            var streamProvider = await Request.Content.ReadAsMultipartAsync();
            foreach (var httpContent in streamProvider.Contents)
            {
                using (var stream = await httpContent.ReadAsStreamAsync())
                {
                    if (stream.Length == 0) // skip header
                    {
                        continue;
                    }
                    // upload
                    using (var fileStream = new FileStream(@"c:\delete\temp\" + httpContent.Headers.ContentDisposition.FileName.Trim('"'), FileMode.Create, FileAccess.Write))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                    }
                }
            }
            return new FileResult
            {
                FileNames = streamProvider.Contents.Select(entry => entry.Headers.ContentDisposition.FileName?.Trim('"') ?? ""),
                Names = streamProvider.Contents.Select(entry => entry.Headers.ContentDisposition.Name?.Trim('"') ?? ""),
                ContentTypes = streamProvider.Contents.Select(entry => entry.Headers.ContentType?.MediaType ?? ""),
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };
        }

        [HttpPost]
        [Route("upload-bufferless")]
        // todo
        public async Task<object> UploadBufferless(string param1, string param2)
        {
            try
            {
                var fileuploadPath = ConfigurationManager.AppSettings["FileUploadLocation"];

                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                await content.ReadAsMultipartAsync(provider);

                for (int i = 0; i < provider.Contents.Count; i++)
                {
                    //Code for renaming the random file to Original file name  
                    string uploadingFileName = provider.FileData[i].LocalFileName;
                    string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[i].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));

                    if (File.Exists(originalFileName))
                    {
                        File.Delete(originalFileName);
                    }

                    File.Move(uploadingFileName, originalFileName);
                    //Code renaming ends...  
                }
                return
                new
                {
                    Success = true,
                    Param1 = param1,
                    Param2 = param2
                };
            }
            catch (Exception ex)
            {
                throw ex;
                return new
                {
                    Success = false,
                    Param1 = param1,
                    Param2 = param2
                };
            }
        }

        [HttpPost]
        [Route("upload-bufferless2")]
        // todo
        public async Task<object> UploadBufferless2(string param1, string param2)
        {
            try
            {
                using (var reader = new StreamReader(HttpContext.Current.Request.GetBufferlessInputStream(true)))
                using (var filestream = new FileStream("c:/delete/temp/1", FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true))
                using (var writer = new StreamWriter(filestream))
                {
                    var dataToWrite = await reader.ReadToEndAsync();
                    await writer.WriteAsync(dataToWrite);

                    return true;
                }

                //var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                //foreach (var header in Request.Content.Headers)
                //{
                //    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                //}
                //var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

                //foreach (var stream in filesReadToProvider.Contents)
                //{
                //    var fileBytes = await stream.ReadAsByteArrayAsync();
                //    File.WriteAllBytes("c:/delete/temp", fileBytes);
                //}
                //return
                //    new
                //    {
                //        Success = true,
                //        Param1 = param1,
                //        Param2 = param2
                //    };
            }
            catch (Exception ex)
            {
                throw ex;
                return new
                {
                    Success = false,
                    Param1 = param1,
                    Param2 = param2
                };
            }
        }
    }

}
