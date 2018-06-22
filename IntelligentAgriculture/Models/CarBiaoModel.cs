using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CarBiaoModel
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
        public CarModelClass result { get; set; } 
    }
    public class CarModelClass {
        public string keyword { get; set; }
        public List<CarBodyModelClass> list { get; set; } 
    }
    public class CarBodyModelClass {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// LOGO
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 年款
        /// </summary>
        public string yeartype { get; set; }
        /// <summary>
        /// 生产状态
        /// </summary>
        public string productionstate { get; set; }
        /// <summary>
        /// 销售状态
        /// </summary>
        public string salestate { get; set; }
        /// <summary>
        /// 车辆等级
        /// </summary>
        public string sizetype { get; set; }
    }
}