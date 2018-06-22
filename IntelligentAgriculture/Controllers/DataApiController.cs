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
    public class DataApiController : ApiController
    {
        #region 重写返回的方法
        public HttpResponseMessage ReturnHttpResponse(string json)
        {
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        #endregion

       
        #region 上传菜图片
        [HttpPost]
        public HttpResponseMessage UploadCaiImg()
        {
            string virtualPath = "/UploadFile/CaiImg/";
            string path = HttpContext.Current.Server.MapPath(virtualPath);

            Stream filestream = System.Web.HttpContext.Current.Request.Files[0].InputStream;
          
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                Stream stream = System.Web.HttpContext.Current.Request.Files[0].InputStream;
                Bitmap bmp = new Bitmap(stream);
                Bitmap newbmp = new Bitmap(bmp, 600, 600);
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

        #region 菜名比对的结果
        [HttpGet]
        public HttpResponseMessage BiDuiImg(string imgpath)
        {
            #region 根据百度API得到菜名
            string tupath = HelpTool.GetFilePath(imgpath);
            var tokenobj = JsonConvert.DeserializeObject<CaiModel>(BaiDuAITool.GetCaiResult(tupath));
            List<CaiResultContent> result = tokenobj.result;
            return ReturnHttpResponse(JsonConvert.SerializeObject(result));
            #endregion
        }
        #endregion

        #region 根据菜名得到菜谱
        [HttpGet]
        public HttpResponseMessage GetCaipu(string name)
        {
            var Zuocai = BaiDuAITool.CaiPu(name);

            var caipuobj = JsonConvert.DeserializeObject<CaipuModel>(Zuocai);
           
            var result = caipuobj.result.list[0];

            return ReturnHttpResponse(JsonConvert.SerializeObject(result));
        }
        #endregion

     
    }
}
