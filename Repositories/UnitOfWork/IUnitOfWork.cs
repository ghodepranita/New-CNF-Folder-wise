using CNF.Business.Repositories.Repository;
using CNF.Business.Resources.ExceptionLogging;
using System;

namespace CNF.Business.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get;}
        ILoginRepository loginRepository { get; }
        IAdminRepository adminRepository { get; }
        ILogger GetLoggerInstance { get; }
        IMastersRepository MastersRepository { get; }
        ILoggerRepository GetLoggerRepositoryInstance { get; }
        IOrderDispatchRepository OrderDispatchRepository { get; }
        IChequeAccountingRepository chequeAccountingRepository { get; }
        IConfigurationRepository configurationRepository { get; }
        IOrderReturnRepository OrderReturnRepository { get; }
        IInventoryInwardRepository inventoryInwardRepository { get; }
        IStockTransferRepository StockTransferRepository { get; }
        IAccountsRepository AccountsRepository { get; }
        IOCRRepository OCRRepository { get; }
        //Created by Pratyush
        IReportRepository ReportRepository { get; }
    }
}
