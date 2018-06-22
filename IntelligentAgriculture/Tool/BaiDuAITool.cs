using MvcApplication1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MvcApplication1.Tool
{
    public class BaiDuAITool
    {
        #region 常用的参数
        public static string AppID = "10220597";
        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        public static string API_KEY = "SCgmGcdkMZhe5ukGmljTLZ94";
        // 百度云中开通对应服务应用的 Secret Key
        public static string SECRET_KEY = "PbBjyOun8lWyWgWvPF0Sc3OZ1QyYCVxL";        
        #endregion

        #region 得到Token的值
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
	    public static String GetAccessToken() 
       {
			String authHost = "https://aip.baidubce.com/oauth/2.0/token";
			HttpClient client = new HttpClient();
			List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
			paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", API_KEY));
            paraList.Add(new KeyValuePair<string, string>("client_secret", SECRET_KEY));

			HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
			String result = response.Content.ReadAsStringAsync().Result;
			
            var tokenobj=JsonConvert.DeserializeObject<TokenModel>(BaiDuAITool.GetAccessToken());
            string Token = tokenobj.access_token;
            return Token;
		}
        #endregion 

        #region 菜品识别(只识别一张图)
        public static String GetCaiResult(string imgPath) {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);

            var image = File.ReadAllBytes(imgPath);
            // 调用菜品识别
            var result = client.DishDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"top_num", 5 } 
            };
            // 带参数调用菜品识别
            //image:图像数据，base64编码，要求base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式
            //top_num:返回预测得分top结果数，默认为5
            result = client.DishDetect(image, options);  
            Console.WriteLine(result);
            return result.ToString();
        }
        #endregion

        #region 菜谱
        public static String CaiPu(string caiming) {
            String authHost = "http://api.jisuapi.com/recipe/search?keyword=" + caiming + "&num=1&appkey=bdb5f07f2b143047";
            HttpClient client = new HttpClient();
          
            HttpResponseMessage response = client.PostAsync(authHost, null).Result;
            String result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
        #endregion

        #region 车辆识别 (只识别一张图)
        public static String GetCarResult(string imgPath)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgPath);
            // 调用菜品识别
            var result = client.DishDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"top_num", 5 } 
            };
            // 带参数调用菜品识别
            //image:图像数据，base64编码，要求base64编码后大小不超过4M，最短边至少15px，最长边最大4096px,支持jpg/png/bmp格式
            //top_num:返回预测得分top结果数，默认为5
            result = client.CarDetect(image, options);
            Console.WriteLine(result);
            return result.ToString();
        }
        #endregion

        #region 车辆信息
        public static String GetCarBodyResult(string chename)
        {
            String authHost = "http://api.jisuapi.com/car/search?appkey=bdb5f07f2b143047&keyword=" + chename;
            HttpClient client = new HttpClient();
          
            HttpResponseMessage response = client.PostAsync(authHost, null).Result;
            String result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
        #endregion

        #region LOGO商标的识别
        public static String GetLogoResult(string imgPath)
        {

            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgPath);
            // 调用菜品识别
            var result = client.DishDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            //是否只使用自定义logo库的结果，默认false：返回自定义库+默认库的识别结果
            var options = new Dictionary<string, object>{
                {"custom_lib", "true" }  
            };
            // 带参数调用菜品识别
            result = client.DishDetect(image, options);
            Console.WriteLine(result);
            return result.ToString();
        }
        #endregion    

        #region 动物的识别
        public static String GetAnimalResult(string imgPath)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgPath);
            // 调用动物识别
            var result = client.AnimalDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"top_num", 5 } 
             };
            // 带参数调用动物识别
            result = client.AnimalDetect(image, options);
            Console.WriteLine(result);

            return result.ToString();
        }
        #endregion

        #region 植物的识别
        public static String GetPlantResult(string imgPath)
        {
            var client = new Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgPath);
            // 调用动物识别
            var result = client.PlantDetect(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"top_num",5 } 
             };
            // 带参数调用动物识别
            result = client.AnimalDetect(image, options);
            Console.WriteLine(result);

            return result.ToString();
        }
        #endregion

        #region 人脸检测
        public static String GetFaceDetectResult(string imgPath)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgPath);
            var options = new Dictionary<string, object>()
            {
             {"face_fields", "beauty,age"}
            };
            // 调用图像主体检测
            var result = client.FaceDetect(image, options);
            return result.ToString();
        }
        #endregion

        #region 人脸对比
        public static String GetFaceMatch(string imgpath1,string imgpath2) {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image1 = File.ReadAllBytes(imgpath1);
            var image2 = File.ReadAllBytes(imgpath2);

            var images = new byte[][] { image1, image2 };

            // 人脸对比
            var result = client.FaceMatch(images);
            return result.ToString();
        }
        #endregion

        #region 人脸识别
        public static String GetFaceIdentify(string imgpath, string groupId)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image1 = File.ReadAllBytes(imgpath);

            var result = client.User.Identify(image1, new []{groupId}, 1, 1);
            return result.ToString();
        }
        #endregion

        #region 人脸认证
        public static String GetFaceVerify(string imgpath, string uid, string groupId)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image1 = File.ReadAllBytes(imgpath);

            var result = client.User.Verify(image1, uid, new[] { groupId }, 1);
            return result.ToString();
        }
        #endregion

        #region 人脸注册
        public static String FaceRegister(string imgpath, string uid, string groupId)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image1 = File.ReadAllBytes(imgpath);

            var result = client.User.Register(image1, uid, "user info here", new[] { groupId});
            return result.ToString();
        }
        #endregion

        #region 人脸更新
        public static String FaceUpdate(string imgpath, string uid, string groupId)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var image1 = File.ReadAllBytes(imgpath);

            var result = client.User.Update(image1, uid, groupId, "new user info");
            return result.ToString();
        }
        #endregion

        #region 人脸删除
        public static String FaceDelete(string imgpath,string uid, string groupId)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var result = client.User.Delete(uid);
            result = client.User.Delete(uid, new[] { groupId });
            return result.ToString();
        }
        #endregion

        #region 用户信息查询
        public static String UserInfo(string uid)
        {
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var result = client.User.GetInfo(uid);
            return result.ToString();
        }
        #endregion

        #region 通用文字识别
        public static String GeneralBasic(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 通用文字识别
            var result = client.GeneralBasic(image, null);
            return result.ToString();
        }
        #endregion
                       
        #region 识别文字在图片中的位置
        public static String GeneralWithLocatin(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 带位置版本
            var result = client.GeneralWithLocatin(image, null);
            return result.ToString();
        }
        #endregion

        #region 识别生僻字
        public static String GeneralEnhanced(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 带生僻字版
            var result = client.GeneralEnhanced(image, null);
            return result.ToString();
        }
        #endregion

        #region 网络图片的识别
        public static String WebImage(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 网图识别
            var result = client.WebImage(image, null);
            return result.ToString();
        }
        #endregion

        #region  通用文字识别高精度版
        public static String Accurate(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 高精度识别
            var result = client.Accurate(image);
            return result.ToString();
        }
        #endregion

        #region 银行卡识别
        public static String BankCard(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 银行卡识别
            var result = client.BankCard(image);
            return result.ToString();
        }
        #endregion

        #region 身份证识别
        public static String Idcard(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);

            // 身份证正面识别
            var result = client.IdCardFront(image);
            // 身份证背面识别
            result = client.IdCardBack(image);
            return result.ToString();
        }
        #endregion

        #region 行驶证识别
        public static String VehicleLicense(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);
            var result = client.VehicleLicense(image);
            return result.ToString();
        }
        #endregion

        #region 车牌识别
        public static String PlateLicense(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);
            var result = client.PlateLicense(image);
            return result.ToString();
        }
        #endregion

        #region 通用票据识别
        public static String Receipt(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);
            var options = new Dictionary<string, object>()
            {
              {"recognize_granularity", "small"}  // 定位单字符位置
            };
            var result = client.Receipt(image, options);
            return result.ToString();
        }
        #endregion

        #region 营业执照识别
        public static String BusinessLicense(string imgpath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            var image = File.ReadAllBytes(imgpath);
            var result = client.BusinessLicense(image, null);
            return result.ToString();
        }
        #endregion

        #region 语言识别
        public static String AsrData(string pcmpath)
        {
            var client = new Baidu.Aip.Speech.Asr(API_KEY, SECRET_KEY);
            var data = File.ReadAllBytes(pcmpath);
            var result = client.Recognize(data, "pcm", 16000);
            Console.Write(result);
            return result.ToString();
        }
        #endregion
    }
}