using InstallDrawingProject.Tool;
using MvcApplication1.Models;
using MvcApplication1.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace IntelligentAgriculture.Controllers
{
    public class PlantApiController : ApiController
    {
        #region 重写返回的方法
        public HttpResponseMessage ReturnHttpResponse(string json)
        {
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        #endregion

        #region 值物比对的结果
        [HttpGet]
        public HttpResponseMessage BiDuiPlant(string imgpath)
        {
            string tupath = HelpTool.GetFilePath(imgpath);
            var tokenobj = JsonConvert.DeserializeObject<PlantModel>(BaiDuAITool.GetPlantResult(tupath));
            List<PlantContentModel> result = tokenobj.result;
            return ReturnHttpResponse(JsonConvert.SerializeObject(result));
        }
        #endregion
    }
}
