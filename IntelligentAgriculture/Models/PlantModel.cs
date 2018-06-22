using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PlantModel
    {
        public int number { get; set; }
        public List<PlantContentModel> result { get; set; }
    }
    public class PlantContentModel {
        public string name { get; set; }
        public double score { get; set; }
    }
}