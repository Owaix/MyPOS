using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public class Barcode
    {
        public int IID { get; set; }
        public double? SalesPrice { get; set; }
        public string IName { get; set; }
        public int Quantity { get; set; }
        public string ArticleNo { get; set; } = "000";
        public string BarCodeNo { get; set; }
    }
}
