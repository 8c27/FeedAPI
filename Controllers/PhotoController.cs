using FeedAPI.Models;
using FeedAPI.Services;
using FeedAPI.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FeedAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private FeedingContext _feedingContext;
        private readonly IHubContext<ChatHub> _hubContext;
        public PhotoController(FeedingContext feedingContext, IHubContext<ChatHub> hubContext)
        {
            _feedingContext = feedingContext;
            _hubContext = hubContext;
        }
        [HttpPost("UploadFile")]
        public string UploadFile([FromForm] ICollection<IFormFile> files, [FromForm] string feedNumber)
        {
            string path = @"D:\SideProject\Photo\" + feedNumber;
            // @"D:\SideProject\Photo"
            //file.sa
            // 過濾

            var dirInfo = new DirectoryInfo(path);
            dirInfo.Create();
            //排除不合法的檔案(副檔名，檔案大於15mb)
            string[] allowExtension = { ".jpg", ".png", ".pdf" };


            var readyToSave = files.Where(p => allowExtension.Contains(System.IO.Path.GetExtension(p.FileName).ToLower())).
                                    Where(s => s.Length < 15728640);

            foreach (var file in readyToSave)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(path, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //await file.CopyToAsync(fileStream);
                        file.CopyTo(fileStream);
                    }
                }
            }

            return $"got file: {readyToSave.Count()}";
        }
        [HttpGet("GetPhotoList")]
        public List<Photo> Photos([FromQuery] string feedNumber)
        {
            string path = @"D:\SideProject\Photo\";
            path = Path.Combine(path, feedNumber.ToString());
            DirectoryInfo di = new DirectoryInfo(path);
            // @"D:\photos\wip"
            List<Photo> list = new List<Photo>();

            // 如果沒有路徑就建立
            if (!di.Exists)
            {
                di.Create();
            }

            //排除不合法的檔案(副檔名，檔案大於15mb)
            string[] allowExtension = { ".jpg", ".png", ".pdf" };

            var files = di.EnumerateFiles("*.*");
            var readyToSave = files.Where(p => allowExtension.Contains(System.IO.Path.GetExtension(p.FullName).ToLower())).
                                    Where(s => s.Length < 15728640);
            int i = 0;
            foreach (var file in readyToSave)
            {

                if (file.Length > 0)
                {
                    Photo model = new Photo();
                    model.index = i;
                    model.imgName = file.Name;
                    model.fileType = file.Extension;

                    list.Add(model);
                    i = i++;
                }
            }

            return list;
        }

        [HttpGet("GetPhoto")]
        public IActionResult Get(string feedNumber, string fileName)
        {
            //string path = @"c:\123";
            //var path = @"c:\123\"+ fileName + fileType;
            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //result.Content = new StreamContent(stream);
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //return result;

            var path = @"D:\SideProject\Photo\" + feedNumber + '\\' + fileName; ;
            Byte[] b = System.IO.File.ReadAllBytes(path);   // You can use your own method over here.         
            return File(b, "image/jpeg");


        }
    }
}
