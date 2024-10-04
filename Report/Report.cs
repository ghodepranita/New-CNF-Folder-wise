using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CNF.Business.Model.Report
{
    //Created by Pratyush
    public class PrinterHValueModal
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int BranchId { get; set; } 

        public int CompanyId { get; set; }

        public string PrinterType { get; set; }

    }

    public class PrinterHDataModal
    {
        public long InvId { get; set; }

        public string InvNo { get; set; }

        public DateTime InvCreatedDate { get; set; }
        public string BranchName { get; set; }

        public string CompanyName { get; set; }

        public DateTime Date { get; set; }

        public string PrinterType { get; set; }
    }
}
