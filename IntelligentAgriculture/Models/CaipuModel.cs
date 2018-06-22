using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CaipuModel
    {
        /// <summary>
        /// 状态吗
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public CaipuClass result { get; set; }        
    }
    public class CaipuClass {
        /// <summary>
        /// 菜谱数量
        /// </summary>
        public int num { get; set; }

        public List<CaipuContentClass> list { get; set; }
    }
    public class CaipuContentClass {
        /// <summary>
        /// 菜谱ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int classid { get; set; }
        /// <summary>
        /// 菜谱名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 用餐人数
        /// </summary>
        public string peoplenum { get; set; }
        /// <summary>
        /// 准备时间
        /// </summary>
        public string preparetime { get; set; }
        /// <summary>
        /// 烹饪时间
        /// </summary>
        public string cookingtime { get; set; }
        /// <summary>
        /// 菜谱说明
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 菜谱图片
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        ///    标签
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        ///  材料
        /// </summary>
        public List<CailiaoClass> material { get; set; }
       
        /// <summary>
        /// 步骤
        /// </summary>
        public List<BuzhouClass> process { get; set; }
    }
    public class CailiaoClass {
        /// <summary>
        /// 材料名称
        /// </summary>
        public string mname { get; set; }
        /// <summary>
        ///  材料类型 0辅料 1主料
        /// </summary>
        public int type { get; set; }
        /// <summary>
        ///    数量
        /// </summary>
        public string amount { get; set; }
    }

    public class BuzhouClass {

        /// <summary>
        /// 步骤内容
        /// </summary>
        public string pcontent { get; set; }
        /// <summary>
        /// 步骤图片
        /// </summary>
        public string pic { get; set; }
    }
}