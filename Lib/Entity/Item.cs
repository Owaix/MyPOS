//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lib.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int IID { get; set; }
        public string IName { get; set; }
        public string Desc { get; set; }
        public Nullable<int> Unit_ID { get; set; }
        public Nullable<bool> Inv_YN { get; set; }
        public Nullable<int> CompID { get; set; }
        public string BarCode_ID { get; set; }
        public Nullable<int> SCatID { get; set; }
        public Nullable<int> CTN_PCK { get; set; }
        public Nullable<double> PurPrice { get; set; }
        public Nullable<double> SalesPrice { get; set; }
        public Nullable<double> RetailPrice { get; set; }
        public Nullable<int> ICP { get; set; }
        public Nullable<int> OP_Qty { get; set; }
        public Nullable<int> OP_Price { get; set; }
        public Nullable<int> DisContinue { get; set; }
        public Nullable<int> AC_Code_Inv { get; set; }
        public Nullable<int> AC_Code_Inc { get; set; }
        public Nullable<int> AC_Code_Cost { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> saleTax { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public Nullable<int> ArticleNoID { get; set; }
        public string BarcodeNo { get; set; }
        public Nullable<decimal> DisP { get; set; }
        public Nullable<decimal> DisR { get; set; }
        public Nullable<int> AveragePrice { get; set; }
        public Nullable<decimal> RetailPOne { get; set; }
        public Nullable<decimal> RetailPTwo { get; set; }
        public Nullable<decimal> RetailPThree { get; set; }
        public string Img { get; set; }
    }
}
