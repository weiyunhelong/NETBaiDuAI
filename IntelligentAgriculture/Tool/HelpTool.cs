using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.RegularExpressions;

namespace InstallDrawingProject.Tool
{
    public static class HelpTool
    {
        public static int TrySetInt(this string str, int def)
        {
            int b = 0;
            if (String.IsNullOrEmpty(str)) return def;

            bool bo = int.TryParse(str, out b);
            if (bo)
                return b;
            else
                return def;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToString());
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string getMD5(string myString)
        {
            MD5 md5 = MD5.Create();//new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Default.GetBytes(myString);

            byte[] targetData = md5.ComputeHash(fromData);
            StringBuilder byte2String = new StringBuilder();

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String.AppendFormat("{0:x2}", targetData[i]);
            }

            return byte2String.ToString();

        }
        /// <summary>
        ///去掉第一个字符和最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrSubstring(string str)
        {
            return  str.TrimStart(',').TrimEnd(',');
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
        public static string RunShell(string strShellCommand)
        {
            //double spanMilliseconds = 0;
            //DateTime beginTime = DateTime.Now;

            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.Arguments = String.Format(@"/c {0}", strShellCommand);
            cmd.Start();

            string result = cmd.StandardOutput.ReadToEnd();

            cmd.WaitForExit();
            cmd.Close();

            //DateTime endTime = DateTime.Now;
            //TimeSpan timeSpan = endTime - beginTime;
            //spanMilliseconds = timeSpan.TotalMilliseconds;

            return result;
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="fullPath">需要访问的文件名</param>
        /// <returns>是否存在</returns>
        public static bool CheckFileExists(this string fullPath)
        {
            bool result = false;
            if (!File.Exists(fullPath))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        public static string GetPath(this string path, string rootPath)
        {
            return String.Format("{0}{1}", rootPath, path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void deleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!File.Exists(filePath))
                {
                    //FileStream fsWrite = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    //fsWrite.Close();
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);
                    //创建文件
                    FileStream fs = file.Create();
                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 将实体转成JSON字符串
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources">泛型实体</param>
        /// <param name="str">需要访问的字段名称</param>
        /// <returns>实体的JSON字符串</returns>
        public static string EntryToJson<TSource>(this TSource sources, string[] str)
        {
            string result = string.Empty;
            Type type = sources.GetType();
            StringBuilder jsonText = new StringBuilder();
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            if (str != null)
            {
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    object value = pi.GetValue(sources, null);//用pi.GetValue获得值   
                    string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作   
                    //获得属性的类型,进行判断然后进行以后的操作,例如判断获得的属性是整数

                    if (str.Contains(name))
                    {
                        writer.WritePropertyName(name);
                        writer.WriteValue(value);
                    }
                }
            }
            else
            {
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    object value = pi.GetValue(sources, null);//用pi.GetValue获得值   
                    string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作   
                    //获得属性的类型,进行判断然后进行以后的操作,例如判断获得的属性是整数
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            writer.WriteEndObject();
            writer.Flush();
            jsonText.Append(sw.GetStringBuilder().ToString());
            jsonText.Append(",");
            result += jsonText.ToString().TrimEnd(',');
            return result;
        }

        /// <summary>
        /// json字符串转实体类对象
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>

        public static T ScriptDeserialize<T>(this string strJson)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(strJson);
        }

        /// <summary>
        /// 存储信息到XML
        /// </summary>
        /// <param name="html">存储信息</param>
        /// <returns></returns>
        public static string CreateXml(string html)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("root");
            XmlNode myhtml = doc.CreateElement("html");
            myhtml.InnerText = html;
            root.AppendChild(myhtml);
            doc.AppendChild(root);
            doc.InsertBefore(xmldecl, root);
            return doc.InnerXml;
        }

        public static string CreateXml(string attributeName, string attributeValue)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("root");
            XmlNode XmlattributeName = doc.CreateElement("attributeName");
            XmlattributeName.InnerText = attributeName;
            root.AppendChild(XmlattributeName);
            XmlNode XmlattributeValue = doc.CreateElement("attributeValue");
            XmlattributeValue.InnerText = attributeValue;
            root.AppendChild(XmlattributeValue);
            doc.AppendChild(root);
            doc.InsertBefore(xmldecl, root);
            return doc.InnerXml;
        }

        public static string CreateXml(string optionOne, string optionTwo, string optionThree, string optionFour, string optionFive = "")
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("root");

            XmlNode XmloptionOne = doc.CreateElement("optionOne");
            XmloptionOne.InnerText = optionOne;
            root.AppendChild(XmloptionOne);

            XmlNode XmloptionTwo = doc.CreateElement("optionTwo");
            XmloptionTwo.InnerText = optionTwo;
            root.AppendChild(XmloptionTwo);


            XmlNode XmloptionThree = doc.CreateElement("optionThree");
            XmloptionThree.InnerText = optionThree;
            root.AppendChild(XmloptionThree);


            XmlNode XmloptionFour = doc.CreateElement("optionFour");
            XmloptionFour.InnerText = optionFour;
            root.AppendChild(XmloptionFour);


            XmlNode XmloptionFive = doc.CreateElement("optionFive");
            XmloptionFive.InnerText = optionFive;
            root.AppendChild(XmloptionFive);

            doc.AppendChild(root);
            doc.InsertBefore(xmldecl, root);
            return doc.InnerXml;
        }

        /// <summary>
        /// 分页信息XML
        /// </summary>
        /// <param name="CountXml">参与分页项个数</param>
        /// <param name="PageSizeXml">每页显示个数</param>
        /// <param name="PageXml">起始页</param>
        /// <returns></returns>
        public static string PageCreateXml(int CountXml, int PageSizeXml, int PageXml)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("root");

            XmlNode Count = doc.CreateElement("Count");
            Count.InnerText = CountXml.ToString();
            root.AppendChild(Count);

            XmlNode PageSize = doc.CreateElement("PageSize");
            PageSize.InnerText = PageSizeXml.ToString();
            root.AppendChild(PageSize);

            XmlNode Page = doc.CreateElement("Page");
            Page.InnerText = PageXml.ToString();
            root.AppendChild(Page);

            doc.AppendChild(root);
            doc.InsertBefore(xmldecl, root);
            return doc.InnerXml;
        } 
        /// 生成缩略图    
        /// 源图路径（物理路径）  
        /// 缩略图路径（物理路径）  
        /// 缩略图宽度  
        /// 缩略图高度  
        /// 生成缩略图的方式   
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode="Cut")
        {
            try
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
                System.Drawing.Imaging.ImageFormat thisFormat = originalImage.RawFormat;
                int towidth = 0;
                int toheight = 0;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case "HW"://指定高宽缩放（可能变形）   
                        break;
                    case "W"://指定宽，高按比例   
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例  
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://指定高宽裁减（不变形）
                        if (oh > height || ow > width)
                        {
                            var bilv = false;
                            if (ow > oh)
                            {
                                bilv = true;
                            }
                            if (bilv == true && ow != 0)
                            {
                                double bliu = width / (ow * 1.0);
                                towidth = width;
                                toheight = (int)Math.Round(oh * bliu);
                            }
                            else if (bilv == false && oh != 0)
                            {
                                double bliu = height / (oh * 1.0);
                                towidth = (int)Math.Round(ow * bliu);
                                toheight = height;
                            }
                        }
                        else
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                        break;
                    default:
                        break;
                }

                //新建一个bmp图片  
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

                //新建一个画板  
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法  
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度  
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充  
                g.Clear(System.Drawing.Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分  
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),

                System.Drawing.GraphicsUnit.Pixel);

                try
                {
                    bitmap.Save(thumbnailPath, thisFormat);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// base64编码的文本转为
        /// </summary>
        /// <param name="txtBase"></param>
        /// <returns></returns>
        public static string Base64StringToImage(string txtBase64, string virtualPath, int height, int width)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(virtualPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                byte[] arr = Convert.FromBase64String(txtBase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                Bitmap newbmp = new Bitmap(bmp, height, width);
                string fileName = DateTime.Now.ToString("ffffff") + ".jpg";
                newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="txtBase"></param>
        /// <returns></returns>
        public static int Base64String2Image(string txtBase64, string virtualPath, string fileName, int height, int width)
        {
            MemoryStream ms = null;
            try
            {
                string path = HttpContext.Current.Server.MapPath(virtualPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                byte[] arr = Convert.FromBase64String(txtBase64);
                ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                Bitmap newbmp = new Bitmap(bmp, height, width);
                string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
                switch (fileExtension)
                {
                    case ".gif": newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ".jpg": newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".png": newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    case ".bmp": newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    default: newbmp.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                ms.Flush();
                ms.Close();
            }
            return 1;
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        #region 检查DataTable 是否有数据行
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;

            return false;
        }
        #endregion

        #region 手机有效性
        /// <summary>
        /// 手机有效性
        /// </summary>
        /// <param name="strMobile"></param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            Regex rx = new Regex(@"^(13|14|15|17|18)\d{9}$", RegexOptions.None);
            Match m = rx.Match(mobile);
            return m.Success;
        }
        #endregion

        #region int有效性
        /// <summary>
        /// int有效性
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public bool IsValidInt(string val)
        {
            //return Regex.IsMatch(val, @"^[1-9]\d*\.?[0]*$");
            return Regex.IsMatch(val, @"^[0-9]*$");
        }
        #endregion

        #region 生成订单号
        public static string GeneratorOrderNO(int num) {
            string result = "";
            result += DateTime.Now.ToString("yyyyMMddhhmmssSSS");
            result += num;
            return result;
        }
        #endregion

        #region 生成商户的订单号
        public static string GeneratorMachNO(int num,string mch_id)
        {
            string result = mch_id;
            result += DateTime.Now.ToString("yyyyMMddhhmmssSSS");
            result += num;
            return result;
        }
        #endregion

        #region 根据经纬度计算距离
        public const double EARTH_RADIUS = 6378.137;//地球半径
        public static double GetDistance(double beginjingdu, double beginweidu, double endjingdu, double endweidu)
        {
            //经度
            double lng = (beginjingdu * Math.PI / 180) - (endjingdu * Math.PI / 180);
            //纬度
            double lat = (beginweidu * Math.PI / 180) - (endweidu * Math.PI / 180); ;

            double dis = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(lat / 2), 2)
                + Math.Cos(beginweidu * Math.PI / 180) * Math.Cos(endweidu * Math.PI / 180)
                * Math.Pow(Math.Sin(lng / 2), 2)));
            dis = dis * EARTH_RADIUS;
            dis = Math.Round(dis * 1e4) / 1e4;

            //计算出来的原本是 公里
            return dis;

        }
        #endregion

        #region 根路径
        public static string GetFilePath(string filepath)
        {
            return HttpContext.Current.Server.MapPath(filepath);
        }
        #endregion

        #region 得到周几(星期)
        public static string GetWeekOfDayTxtXing(DateTime time) {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = Day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week; 
        }
        #endregion

        #region 得到周几(周)
        public static string GetWeekOfDayTxtWeek(DateTime time)
        {
            string[] Day = new string[] { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            string week = Day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week;
        }
        #endregion

        #region 得到周几(数字)
        public static string GetWeekOfDayNum(DateTime time)
        {
            string[] Day = new string[] { "七", "一", "二", "三", "四", "五", "六" };
            string week = Day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week;
        }
        #endregion
    }
}