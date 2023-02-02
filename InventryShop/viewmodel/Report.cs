using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace InventryShop.viewmodel
{
    public class Report
    {
        public int id { get; set; }
        public string CatName { get; set; }
        public string ProName { get; set; }
        public int price { get; set; }
        public string UserName { get; set; }
    }
}