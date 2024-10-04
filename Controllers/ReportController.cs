using CNF.Business.BusinessConstant;
using CNF.Business.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CNF.API.Controllers
{
    public class ReportController : BaseApiController
    {
        #region Get Printer History List Created by Pratyush
        [HttpPost]
        [Route("Report/GetPrinterList")]
        public List<PrinterHDataModal> GetPrinterList([FromBody] PrinterHValueModal modal)
        {
            List<PrinterHDataModal> lst = new List<PrinterHDataModal>();
            try
            {
                lst = _unitOfWork.ReportRepository.GetPrinterList(modal.BranchId, modal.CompanyId, modal.FromDate, modal.ToDate, modal.PrinterType);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterList", "Get  Data List " + "BranchId:  " + modal.BranchId + "CompanyId:  " + modal.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return lst;
        }

        #endregion
    }
}
