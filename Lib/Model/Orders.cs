using Lib.Entity;
using System;
using System.Collections.Generic;

namespace Lib.Model
{
    public class Orders : ReportsModel
    {
        public Orders()
        {

        }
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public string KOTID { get; set; }
        public string OrderNo { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<bool> isComplete { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> TblID { get; set; }
        public String Tbl { get; set; }
        public String ItemDetails { get; set; }
        public Nullable<int> WaiterID { get; set; }
        public String booker { get; set; }
        public String OrderType { get; set; }
        public String Address { get; set; }
        public String Cat { get; set; }
        public int CatID { get; set; }
        public int ItemID { get; set; }
        public Nullable<decimal> CashCard { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public int Rows { get; set; }
        public float RowHeight { get; set; }
        public string item { get; set; }
        public Nullable<decimal> GST { get; set; }
        public tbl_Order Order { get; set; } //= new tbl_Order();
        public List<tbl_OrderDetails> OrderDetailsModel { get; set; } //= new List<tbl_OrderDetails>();
        public decimal DeliveryCharges { get; set; } = 0;
    }

    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string KOTID { get; set; }
        public Nullable<int> itemID { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<int> CatID { get; set; }
        public string itemDtl { get; set; }
    }
}
