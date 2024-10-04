using CNF.Business.Repositories.Repository;
using CNF.Business.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNF.Business.Model.Report;
using CNF.Business.BusinessConstant;

namespace CNF.Business.Repositories
{
   
    class ReportRepository : IReportRepository
    {
        private CFADBEntities _contextManager;

        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public ReportRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }



        #region Get Printer History List Created by Pratyush
        public List<PrinterHDataModal> GetPrinterList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string PrinterType)
        {
            return GetPrinterList_pvt(BranchId, CompanyId, fromDate, toDate, PrinterType);
        }

        public List<PrinterHDataModal> GetPrinterList_pvt(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string PrinterType)
        {
            List<PrinterHDataModal> lst = new List<PrinterHDataModal>();
            try
            {
                using(CFADBEntities _contextManager= new CFADBEntities()) 
                {
                    lst = _contextManager.usp_GetPrinterHistory(BranchId, CompanyId, fromDate, toDate, PrinterType).Select(x => new PrinterHDataModal
                    {
                        InvId=Convert.ToInt64(x.InvId),
                        InvNo= x.InvNo,
                        InvCreatedDate = Convert.ToDateTime(x.InvCreatedDate),
                        BranchName = x.BranchName,
                        CompanyName = x.CompanyName,
                        Date = Convert.ToDateTime(x.date),
                        PrinterType =x.PrinterType
                        
                    }).ToList();
                }
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterList_pvt", "Get Printer List  " + "CompanyId:  " + CompanyId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return lst;
        }
        #endregion
    }

}
