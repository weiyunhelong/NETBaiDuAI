using InstallDrawingProject.Tool;
using MvcApplication1.Models;
using MvcApplication1.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace IntelligentAgriculture.Controllers
{
    public class AnimalApiController : ApiController
    {
        #region 重写返回的方法
        public HttpResponseMessage ReturnHttpResponse(string json)
        {
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        #endregion

        #region 上传动物图片
        [HttpPost]
        public HttpResponseMessage UploadZoonImg()
        {
            string virtualPath = "/UploadFile/ZoonImg/";
            string path = HttpContext.Current.Server.MapPath(virtualPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                Stream stream = System.Web.HttpContext.Current.Request.Files[0].InputStream;
                Bitmap bmp = new Bitmap(stream);
                Bitmap newbmp = new Bitmap(bmp, 300, 300);
                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(System.Web.HttpContext.Current.Request.Files[0].FileName);
                newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                //截图
                HelpTool.MakeThumbnail(path + fileName, path + "thumb_" + fileName, 300, 300);

                //删除原文件
                HelpTool.deleteFile(path + fileName);

                return ReturnHttpResponse(virtualPath + "thumb_" + fileName);
            }
            catch (Exception ex)
            {
                return ReturnHttpResponse("ABC");
            }

        }
        #endregion

        #region 动物比对的结果
        [HttpGet]
        public HttpResponseMessage BiDuiAnimal(string imgpath)
        {
            string tupath = HelpTool.GetFilePath(imgpath);
            var tokenobj = JsonConvert.DeserializeObject<AnimalModel>(BaiDuAITool.GetAnimalResult(tupath));
            List<AnimalRecontentModel> result = tokenobj.result;
            return ReturnHttpResponse(JsonConvert.SerializeObject(result));
        }
        #endregion           

     

    }
}
