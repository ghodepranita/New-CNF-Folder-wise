using CNF.Business.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
    public interface IReportRepository
    {
        List<PrinterHDataModal> GetPrinterList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string PrinterType);
    }
}
