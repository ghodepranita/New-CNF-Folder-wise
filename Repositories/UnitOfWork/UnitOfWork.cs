using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNF.Business.Model.Context;
using CNF.Business.Repositories.Repository;
using CNF.Business.Resources.ExceptionLogging;

namespace CNF.Business.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private CFADBEntities _contextManager;
        public UnitOfWork()
        {
           _contextManager = new CFADBEntities();
        }

        //User Repository
        private IUserRepository _UserRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_UserRepository == null)
                {
                    _UserRepository = new UserRepository(_contextManager);
                }
                return _UserRepository;
            }
        }

       
        private ILoginRepository _loginRepository;
        public ILoginRepository loginRepository
        {
            get
            {
                if (_loginRepository == null)
                {
                    _loginRepository = new LoginRepository(_contextManager);
                }
                return _loginRepository;
            }
        }

       
        private ILogger _GetLoggerInstance;
        public ILogger GetLoggerInstance
        {
            get
            {
                if (_GetLoggerInstance == null)
                {
                    _GetLoggerInstance = Logger.GetInstance;
                }
                return _GetLoggerInstance;
            }
        }

        private ILoggerRepository _GetLoggerRepositoryInstance;
        public ILoggerRepository GetLoggerRepositoryInstance
        {
            get
            {
                if (_GetLoggerRepositoryInstance == null)
                {
                    _GetLoggerRepositoryInstance = new LoggerRepository(_contextManager);
                }
                return _GetLoggerRepositoryInstance;
            }
        }

        private IAdminRepository _adminRepository;
        public IAdminRepository adminRepository
        {
            get
            {
                if (_adminRepository == null)
                {
                    _adminRepository = new AdminRepository(_contextManager);
                }
                return _adminRepository;
            }
        }


        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contextManager.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private bool disposed = false;
        #endregion


        // IMastersRepository Repository
        private IMastersRepository _mastersRepository;
        public IMastersRepository MastersRepository
        {
            get
            {
                if (_mastersRepository == null)
                {
                    _mastersRepository = new MastersRepository(_contextManager);
                }
                return _mastersRepository;
            }
        }

        // IOrderDispatchRepository Repository
        private IOrderDispatchRepository _orderDispatchRepository;
        public IOrderDispatchRepository OrderDispatchRepository
        {
            get
            {
                if (_orderDispatchRepository == null)
                {
                    _orderDispatchRepository = new OrderDispatchRepository(_contextManager);
                }
                return _orderDispatchRepository;
            }
        }

        //IChequeAccoutingRepository Repository
        private IChequeAccountingRepository _chequeAccoutingRepository;
        public IChequeAccountingRepository chequeAccountingRepository
        {
            get
            {
                if (_chequeAccoutingRepository == null)
                {
                    _chequeAccoutingRepository = new ChequeAccountingRepository(_contextManager);
                }
                return _chequeAccoutingRepository;
            }
        }

        //IConfiguration Repository
        private IConfigurationRepository _configurationRepository;
        public IConfigurationRepository configurationRepository
        {
            get
            {
                if (_configurationRepository == null)
                {
                    _configurationRepository = new ConfigurationRepository(_contextManager);
                }
                return _configurationRepository;
            }
        }

        //IOrderReturnRepository Repository
        private IOrderReturnRepository _OrderReturnRepository;
        public IOrderReturnRepository OrderReturnRepository
        {
            get
            {
                if (_OrderReturnRepository == null)
                {
                    _OrderReturnRepository = new OrderReturnRepository(_contextManager);
                }
                return _OrderReturnRepository;
            }
        }

        // IInventoryInwardRepository Repository
        private IInventoryInwardRepository _inventoryInwardRepository;
        public IInventoryInwardRepository inventoryInwardRepository
        {
            get
            {
                if (_inventoryInwardRepository == null)
                {
                    _inventoryInwardRepository = new InventoryInwardRepository(_contextManager);
                }
                return _inventoryInwardRepository;
            }
        }
        // IStockTransferRepository Repository
        private IStockTransferRepository _StockTransferRepository;
        public IStockTransferRepository StockTransferRepository
        {
            get
            {
                if (_StockTransferRepository == null)
                {
                    _StockTransferRepository = new StockTransferRepository(_contextManager);
                }
                return _StockTransferRepository;
            }
        }

        // IAccountsRepository Repository
        private IAccountsRepository _AccountsRepository;
        public IAccountsRepository AccountsRepository
        {
            get
            {
                if (_AccountsRepository == null)
                {
                    _AccountsRepository = new AccountsRepository(_contextManager);
                }
                return _AccountsRepository;
            }
        }

        // IOCRRepository Repository
        private IOCRRepository _OCRRepository;
        public IOCRRepository OCRRepository
        {
            get
            {
                if (_OCRRepository == null)
                {
                    _OCRRepository = new OCRRepository(_contextManager);
                }
                return _OCRRepository;
            }
        }

        //Created by Pratyush IReportRepository
        private IReportRepository _ReportRepository;

        public IReportRepository ReportRepository
        {
            get
            {
                if(_ReportRepository == null)
                {
                    _ReportRepository = new ReportRepository(_contextManager);
                }
                return _ReportRepository;
            }
        }

    }
}
