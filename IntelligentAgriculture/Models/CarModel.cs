using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CarModel
    {
        public int log_id { get; set; }
        public int result_num { get; set; }
        public List<CarResultContent> result { get; set; }
    }
    public class CarResultContent {
        public string name { get; set; }
        public double score { get; set; }
    }
}