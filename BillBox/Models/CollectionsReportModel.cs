using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BillBox.Common;

namespace BillBox.Models
{
    public class CollectionsReportModel
    {   
        [Display(Name="Receipt Number")]
        public int BillId { get; set; }

        [Display(Name="Payment Date")]
        public DateTime Date { get; set; }

        [Display(Name="Amount")]
        public double Amount { get; set; }

        [Display(Name="Subscriber")]
        public string Subscriber { get; set; }

        [Display(Name="Agent")]
        public string Agent { get; set; }

        [Display(Name="Branch")]
        public string Branch { get; set; }

        [Display(Name="From Date")]
        public string DateRangeFrom { get; set; }

        [Display(Name = "To Date")]
        public string DateRangeTo { get; set; }

        [Display(Name = "Processing Fee")]
        public double ProcessingFee { get; set; }

        [Display(Name = "Processing Fee(GCT)")]
        public double ProcessingFeeGCT { get; set; }

        [Display(Name = "Commission")]
        public double Commission { get; set; }

        [Display(Name = "Commission GCT")]
        public double CommissionGCT { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public CollectionsReportModel()
        {
            this.PageNumber = 1;
            this.PageSize = Util.GetPageSize(Common.PagedList.CollectionsReport);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1) ? true : false;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return ((PageNumber * PageSize) < Count) ? true : false;
            }
        }
        
    }
}