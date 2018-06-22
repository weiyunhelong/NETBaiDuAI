using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class LogoModel
    {
        public int log_id { get; set; }
        public int result_num { get; set; }
        public List<LogoContentResult> result { get; set; }
    }
    public class LogoContentResult {
        public object location { get; set; }
        public string name { get; set; }
        public double probability { get; set; }
        public int type { get; set; }
    }
}