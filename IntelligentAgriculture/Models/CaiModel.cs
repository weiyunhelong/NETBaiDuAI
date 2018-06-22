using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CaiModel
    {
        public int log_id { get; set; }
        public int result_num { get; set; }
        public List<CaiResultContent> result { get; set; }
    }
    public class CaiResultContent
    {
        public string name { get; set; }
        public double calorie { get; set; }
        public double probability { get; set; }
    }
}