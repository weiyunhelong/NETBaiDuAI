using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class AnimalModel
    {
        public int log_id { get; set; }
        public List<AnimalRecontentModel> result { get; set; }
    }
    public class AnimalRecontentModel {
        public string name { get; set; }
        public double score { get; set; }
    }
}