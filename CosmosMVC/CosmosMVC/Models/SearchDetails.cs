using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmosMVC.Models
{
    public class SearchDetails
    {
        public string id { get; set; }
        public string PageID { get; set; }
        public string Security { get; set; }
        public string ViewName { get; set; }

        public string ViewType { get; set; }
        public bool IsModel { get; set; }
    }
}