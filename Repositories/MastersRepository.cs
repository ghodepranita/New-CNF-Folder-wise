using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.Master;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;

namespace CNF.Business.Repositories
{
    public class MastersRepository : IMastersRepository
    {
        private CFADBEntities _contextManager;

        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public MastersRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region General Master List
        public GeneralMasterList GetGeneralMaster(string CategoryName, string Status)
        {
            return GetGeneralMasterPvt(CategoryName, Status);
        }
        private GeneralMasterList GetGeneralMasterPvt(string CategoryName, string Status)
        {
            GeneralMasterList generalmasterlist = new GeneralMasterList();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    generalmasterlist.GeneralMasterParameter = ContextManager.usp_GeneralMasterList(CategoryName, Status).Select(x => new GeneralMasterDetail()
                    {
                        pkId = Convert.ToInt32(x.pkId),
                        CategoryName = x.CategoryName,
                        MasterName = x.MasterName,
                        DescriptionText = x.DescriptionText,
                        isActive = x.IsActive
                    }).OrderByDescending(x => x.pkId).ToList();
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGeneralMasterPvt", "Get General Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return generalmasterlist;
        }
        #endregion

        #region Get State List
        public GetStateList GetStateList(string Flag)
        {
            return GetStateDetailsPvt(Flag);
        }
        private GetStateList GetStateDetailsPvt(string Flag)
        {
            GetStateList getstateModel = new GetStateList();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    getstateModel.GetStateParameter = ContextManager.usp_GetStateList(Flag).Select(x => new GetStateDtls()
                    {
                        StateCode = x.StateCode,
                        StateName = x.StateName,
                        ActiveFlag = x.ActiveFlag,
                        LastUpdateBy = x.LastUpdateBy,
                        LastUpdateTime = Convert.ToDateTime(x.LastUpdateTime)
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStateDetailsPvt", "Get State Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return getstateModel;
        }
        #endregion

        #region Get Branch List
        public List<BranchList> GetBranchList(string Status)
        {
            return GetBranchListPvt(Status);
        }
        private List<BranchList> GetBranchListPvt(string Status)
        {
            List<BranchList> modelList = new List<BranchList>();
            try
            {
                modelList = _contextManager.usp_BranchMasterList(Status).Select(b => new BranchList
                {
                    BranchId = b.BranchId,
                    BranchCode = b.BranchCode,
                    BranchName = b.BranchName,
                    BranchAddress = b.BranchAddress,
                    City = b.City,
                    Pin = b.Pin,
                    ContactNo = b.ContactNo,
                    Email = b.Email,
                    Pan = b.Pan,
                    GSTNo = b.GSTNo,
                    IsActive = b.IsActive,
                    Addedby = b.Addedby,
                    CityName = b.CityName,
                    StateName = b.StateName,
                    AddedOn = Convert.ToDateTime(b.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(b.LastUpdatedOn)
                }).OrderByDescending(x => x.BranchId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchListPvt", "Get Branch List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get City List
        public GetCityList GetCityList(string StateCode, string districtCode, string Flag)
        {
            return GetCityDetailsPvt(StateCode, districtCode, Flag);
        }
        private GetCityList GetCityDetailsPvt(string StateCode, string districtCode, string Flag)
        {
            GetCityList getcityModel = new GetCityList();
            getcityModel.GetCityParameter = new List<GetCityDtls>();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    getcityModel.GetCityParameter = ContextManager.usp_GetCityList(StateCode, districtCode, Flag).Select(x => new GetCityDtls()
                    {
                        CityCode = x.CityCode,
                        CityName = x.CityName,
                        StateName = x.StateName,
                        StateCode = x.StateCode,
                        ActiveFlag = x.ActiveFlag,
                        LastUpdateBy = x.LastUpdateBy,
                        LastUpdateTime = Convert.ToDateTime(x.LastUpdateTime)
                    }).OrderByDescending(x => x.CityCode).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCityDetailsPvt", "Get City Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return getcityModel;
        }
        #endregion

        #region  Add Edit Branch Master
        public string BranchMasterAddEdit(BranchList model)
        {
            return BranchMasterAddEditPvt(model);
        }
        private string BranchMasterAddEditPvt(BranchList model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_BranchMasterAddEdit(model.BranchId, model.BranchCode, model.BranchName, model.BranchAddress, model.City, model.Pin, model.ContactNo, model.Email, model.Pan, model.GSTNo, model.StateCode, model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "BranchMasterAddEditPvt", "Branch Master AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Company Details
        public List<CompanyDtls> CompanyDtls(string Status)
        {
            return CompanyDtlsPvt(Status);
        }
        private List<CompanyDtls> CompanyDtlsPvt(string Status)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _contextManager.usp_CompanyMasterList(Status).Select(c => new CompanyDtls
                {
                    CompanyId = c.CompanyId,
                    CompanyCode = c.CompanyCode,
                    CompanyName = c.CompanyName,
                    CompanyEmail = c.CompanyEmail,
                    ContactNo = c.ContactNo,
                    CompanyAddress = c.CompanyAddress,
                    CompanyCity = c.CompanyCity,
                    CityName = c.CityName,
                    Pin = c.Pin,
                    CompanyPAN = c.CompanyPAN,
                    GSTNo = c.GSTNo,
                    IsPicklistAvailable = c.IsPicklistAvailable,
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn)
                }).OrderByDescending(x => x.CompanyId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CompanyDtlsPvt", "Get Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        public string CompanyDtlsAddEdit(CompanyDtls model)
        {
            return CompanyDtlsAddEditPvt(model);
        }
        private string CompanyDtlsAddEditPvt(CompanyDtls model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CompanyMasterAddEdit(model.CompanyId, model.CompanyCode, model.CompanyName, model.CompanyEmail, model.ContactNo, model.CompanyAddress, model.CompanyCity, model.Pin, model.CompanyPAN, model.GSTNo, model.IsPicklistAvailable, model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.CompanyId, "CompanyDtlsAddEditPvt", "Add Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Api for Update Employee and User Activation
        public string EmployeeMasterActivate(EmployeeActiveModel model)
        {
            return EmployeeMasterActivatePvt(model);
        }
        private string EmployeeMasterActivatePvt(EmployeeActiveModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_EmployeeMasterActivate(model.EmpId, model.IsActive, model.Addedby, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.EmpId, "EmployeeMasterActivatePvt", "Employee Activate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Add Employee masters
        public int AddEmployeeDtls(AddEmployeeModel model)
        {
            return AddEmployeeDtlsPvt(model);
        }
        private int AddEmployeeDtlsPvt(AddEmployeeModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_EmployeeMasterAdd(model.BranchId, model.EmpNo, model.EmpName, model.EmpPAN, model.EmpEmail,
                    model.EmpMobNo, model.EmpAddress, model.CityCode, model.DesignationId, model.BloodGroup, model.AadharNo, model.companyStr, model.Addedby, obj);

                RetValue = Convert.ToInt32(obj.Value);

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEmployeeDtlsPvt", "Add Employee", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Edit Employee Masters
        public int EditEmployeeDtls(AddEmployeeModel model)
        {
            return EditEmployeeDtlsPvt(model);
        }
        private int EditEmployeeDtlsPvt(AddEmployeeModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_EmployeeMasterEdit(model.EmpId, model.BranchId, model.EmpName, model.EmpPAN, model.EmpEmail,
                    model.EmpMobNo, model.EmpAddress, model.CityCode, model.DesignationId, model.BloodGroup, model.AadharNo, model.companyStr, model.Addedby, obj);

                RetValue = Convert.ToInt32(obj.Value);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EditEmployeeDtlsPvt", "Edit Employee", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Employee Detalis
        public List<EmployeeDtls> GetEmployeeDtls(int EmpId)
        {
            return GetEmployeeDtlsPvt(EmpId);
        }
        private List<EmployeeDtls> GetEmployeeDtlsPvt(int EmpId)
        {
            List<EmployeeDtls> EmpLst = new List<EmployeeDtls>();
            try
            {
                EmpLst = _contextManager.usp_EmployeeCompanyDetails(EmpId).Select(c => new EmployeeDtls
                {
                    EmpId = c.EmpId,
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                    BranchCode = c.BranchCode,
                    BranchName = c.BranchName,
                    CityCode = c.CityCode,
                    CityName = c.CityName,
                    CompanyCode = c.CompanyCode
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetEmployeeDtlsPvt", "Get Employee Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return EmpLst;
        }
        #endregion

        #region Get Category List
        public GetCategoryList GetCategoryList()
        {
            return GetCategoryListPvt();
        }
        private GetCategoryList GetCategoryListPvt()
        {
            GetCategoryList CategoryList = new GetCategoryList();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    CategoryList.CategoryParameter = ContextManager.usp_GetcategoryList().Select(x => new GetCategoryDetails()
                    {
                        CatId = Convert.ToInt32(x.CatId),
                        CategoryName = x.CategoryName,
                        isActive = x.isActive,
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCategoryList", "Get Category List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CategoryList;
        }
        #endregion

        #region Add Edit Division Master
        public string AddEditDivisionMaster(DivisionMasterLst model)
        {
            return AddEditDivisionMasterPvt(model);
        }
        private string AddEditDivisionMasterPvt(DivisionMasterLst model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_DivisionMasterAddEdit(model.BranchId, model.CompanyId, model.DivisionId, model.DivisionCode, model.DivisionName, model.FloorName, model.IsColdStorage, model.IsActive, model.AddedBy, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.DivisionId, "AddEditDivisionMaster", "Add Edit Division Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Get Division Master List
        public List<DivisionMasterLst> GetDivisionMasterList(string Status)
        {
            return GetDivisionMasterListPvt(Status);
        }
        private List<DivisionMasterLst> GetDivisionMasterListPvt(string Status)
        {
            List<DivisionMasterLst> divisionmasterList = new List<DivisionMasterLst>();
            try
            {
                divisionmasterList = _contextManager.usp_DivisionMasterList(Status).Select(x => new DivisionMasterLst
                {
                    BranchId = x.BranchId,
                    DivisionId = x.DivisionId,
                    DivisionCode = x.DivisionCode,
                    DivisionName = x.DivisionName,
                    FloorName = x.FloorName,
                    IsColdStorage = Convert.ToInt32(x.IsColdStorage),
                    SupplyType = x.SupplyType,
                    IsActive = x.IsActive,
                    AddedBy = x.AddedBy,
                    AddedOn = Convert.ToDateTime(x.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(x.LastUpdatedOn)
                }).OrderByDescending(x => x.DivisionId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDivisionMasterList", "Get Division Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return divisionmasterList;
        }
        #endregion

        #region Add Edit General Master
        public string AddEditGeneralMaster(GeneralMasterLst model)
        {
            return AddEditGeneralMasterPvt(model);
        }
        private string AddEditGeneralMasterPvt(GeneralMasterLst model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_GeneralMasterAddEdit(model.pkId, model.CategoryName, model.MasterName, model.DescriptionText, model.IsActive, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditGeneralMaster", "Add Edit General Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Add Edit Transporter Master
        public string AddEditTransporterMaster(TransporterMasterLst model)
        {
            return AddEditTransporterMasterPvt(model);
        }
        private string AddEditTransporterMasterPvt(TransporterMasterLst model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_TransporterMasterAddEdit(model.TransporterId, model.BranchId, model.TransporterNo, model.TransporterName, model.TransporterEmail, model.TransporterMobNo, model.TransporterAddress, model.CityCode, model.StateCode, model.DistrictCode, model.IsActive, model.RatePerBox, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditTransporterMaster", "Add Edit Transporter Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Get Transporter Master List
        public List<TransporterMasterLst> GetTransporterMasterList(string DistrictCode, string Status, int BranchId)
        {
            return GetTransporterMasterListPvt(DistrictCode, Status, BranchId);
        }
        private List<TransporterMasterLst> GetTransporterMasterListPvt(string DistrictCode, string Status, int BranchId)
        {
            List<TransporterMasterLst> transportermasterList = new List<TransporterMasterLst>();
            try
            {
                transportermasterList = _contextManager.usp_TransporterMasterList(DistrictCode, Status, BranchId).Select(x => new TransporterMasterLst
                {
                    TransporterId = x.TransporterId,
                    BranchId = Convert.ToInt32(x.BranchId),
                    BranchName = x.BranchName,
                    TransporterNo = x.TransporterNo,
                    TransporterName = x.TransporterName,
                    TransporterEmail = x.TransporterEmail,
                    TransporterMobNo = x.TransporterMobNo,
                    TransporterAddress = x.TransporterAddress,
                    CityCode = x.CityCode,
                    CityName = x.CityName,
                    StateCode = x.StateCode,
                    StateName = x.StateName,
                    DistrictCode = x.DistrictCode,
                    DistrictName = x.DistrictName,
                    RatePerBox = Convert.ToInt32(x.RatePerBox),
                    IsActive = x.IsActive,
                    DisplayName = x.DisplayName,
                    LastUpdatedOn = Convert.ToDateTime(x.LastUpdatedOn)
                }).OrderByDescending(x => x.TransporterId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterMasterList", "Get Transporter Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return transportermasterList;
        }
        #endregion

        #region Get Transporter Master List For Branch
        public List<TransporterMasterLst> GetTransporterMasterListForBranch(string DistrictCode, string Status, int BranchId)
        {
            return GetTransporterMasterListForBranchPvt(DistrictCode, Status, BranchId);
        }
        private List<TransporterMasterLst> GetTransporterMasterListForBranchPvt(string DistrictCode, string Status, int BranchId)
        {
            List<TransporterMasterLst> TransMstList = new List<TransporterMasterLst>();
            try
            {
                TransMstList = _contextManager.usp_TransporterMasterListForBranch(DistrictCode, Status, BranchId).Select(x => new TransporterMasterLst
                {
                    TransporterId = x.TransporterId,
                    BranchId = Convert.ToInt32(x.BranchId),
                    BranchName = x.BranchName,
                    TransporterNo = x.TransporterNo,
                    TransporterName = x.TransporterName,
                    TransporterEmail = x.TransporterEmail,
                    TransporterMobNo = x.TransporterMobNo,
                    TransporterAddress = x.TransporterAddress,
                    CityCode = x.CityCode,
                    CityName = x.CityName,
                    StateCode = x.StateCode,
                    StateName = x.StateName,
                    DistrictCode = x.DistrictCode,
                    DistrictName = x.DistrictName,
                    RatePerBox = Convert.ToInt32(x.RatePerBox),
                    IsActive = x.IsActive,
                    DisplayName = x.DisplayName,
                    LastUpdatedOn = Convert.ToDateTime(x.LastUpdatedOn)
                }).OrderByDescending(x => x.TransporterId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterMasterList", "Get Transporter Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransMstList;
        }
        #endregion

        #region Get Role List
        public List<RoleModel> GetRoleLst()
        {
            return GetRoleLstPvt();
        }
        private List<RoleModel> GetRoleLstPvt()
        {
            List<RoleModel> RoleLst = new List<RoleModel>();
            try
            {
                RoleLst = _contextManager.usp_GetRoleList().Select(c => new RoleModel
                {
                    RoleId = c.RoleId,
                    RoleName = c.RoleName,
                    ActiveStatus = c.ActiveStatus
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleLstPvt", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Get Role List For User
        public List<RoleModel> GetRoleLstForUser()
        {
            return GetRoleLstForUserPvt();
        }
        private List<RoleModel> GetRoleLstForUserPvt()
        {
            List<RoleModel> RoleLst = new List<RoleModel>();
            try
            {
                RoleLst = _contextManager.usp_GetRoleListForUser().Select(c => new RoleModel
                {
                    RoleId = c.RoleId,
                    RoleName = c.RoleName,
                    ActiveStatus = c.ActiveStatus
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleLstForUserPvt", "Get Role List For User", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Stockist
        public List<StockistModel> GetStockistLst(int BranchId, int CompanyId, string Status)
        {
            return GetStockistPvt(BranchId, CompanyId, Status);
        }
        private List<StockistModel> GetStockistPvt(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            try
            {
                StockLst = _contextManager.usp_StockistMasterList(BranchId, CompanyId, Status).Select(c => new StockistModel
                {
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    StockistPAN = c.StockistPAN,
                    Emailid = c.Emailid,
                    MobNo = c.MobNo,
                    StockistAddress = c.StockistAddress,
                    CityCode = c.CityCode,
                    CityName = c.CityName,
                    GSTNo = c.GSTNo,
                    //BankId = Convert.ToInt16(c.BankId),
                    //IFSCCode = c.IFSCCode,
                    //BankAccountNo = c.BankAccountNo,
                    LocationId = c.LocationId,
                    MasterName = c.LocationName,
                    Pincode = c.Pincode,
                    DLNo = c.DLNo,
                    DLExpDate = DateTime.Parse(c.DLExpDate.ToString()), // Convert.ToDateTime(c.DLExpDate),
                    FoodLicNo = c.FoodLicNo,
                    FoodLicExpDate = DateTime.Parse(c.FoodLicExpDate.ToString()), // Convert.ToDateTime(c.FoodLicExpDate),
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                    DLExpDateCount = c.DLExpDateCount,
                    FoodLicExpDateCount = c.FoodLicExpDateCount
                }).OrderByDescending(x => Convert.ToInt64(x.StockistId)).OrderByDescending(x => x.DLExpDateCount == 1 || x.FoodLicExpDateCount == 1).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockiestPvt", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }

        public string StockistDtlsAddEdit(StockistModel model)
        {
            return StockistDtlsAddEditPvt(model);
        }
        private string StockistDtlsAddEditPvt(StockistModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            List<BankModel> BankList = new List<BankModel>();

            for (int i = 0; i < model.BnkDtls.Count; i++)
            {
                BankModel bankmodel = new BankModel();
                bankmodel.StockistId = model.StockistId;
                bankmodel.BankId = model.BnkDtls[i].BankId;
                bankmodel.IFSCCode = model.BnkDtls[i].IFSCCode;
                bankmodel.AccountNo = model.BnkDtls[i].AccountNo;
                BankList.Add(bankmodel);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("StockistId");
            dt.Columns.Add("BankId");
            dt.Columns.Add("IFSCCode");
            dt.Columns.Add("AccountNo");

            foreach (var item in BankList)
            {
                dt.Rows.Add(item.StockistId, item.BankId, item.IFSCCode, item.AccountNo);
            }

            try
            {
                using (var db = new CFADBEntities())
                {
                    SqlConnection connection = (SqlConnection)db.Database.Connection;
                    SqlCommand cmd = new SqlCommand("CFA.usp_StockistMasterAddEdit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter StockistIdParameter = cmd.Parameters.AddWithValue("@StockistId", model.StockistId);
                    StockistIdParameter.SqlDbType = SqlDbType.Int;
                    SqlParameter StockistNoParameter = cmd.Parameters.AddWithValue("@StockistNo", model.StockistNo);
                    StockistNoParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter StockistNameParameter = cmd.Parameters.AddWithValue("@StockistName", model.StockistName);
                    StockistNameParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter StockistPANParameter = cmd.Parameters.AddWithValue("@StockistPAN", model.StockistPAN);
                    StockistPANParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter EmailidParameter = cmd.Parameters.AddWithValue("@Emailid", model.Emailid);
                    EmailidParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter MobNoParameter = cmd.Parameters.AddWithValue("@MobNo", model.MobNo);
                    MobNoParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter StockistAddressParameter = cmd.Parameters.AddWithValue("@StockistAddress", model.StockistAddress);
                    StockistAddressParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter CityCodeParameter = cmd.Parameters.AddWithValue("@CityCode", model.CityCode);
                    CityCodeParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter GSTNoParameter = cmd.Parameters.AddWithValue("@GSTNo", model.GSTNo);
                    GSTNoParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter PincodeParameter = cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                    PincodeParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter DLNoParameter = cmd.Parameters.AddWithValue("@DLNo", model.DLNo);
                    DLNoParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter DLExpDateParameter = cmd.Parameters.AddWithValue("@DLExpDate", model.DLExpDate);
                    DLExpDateParameter.SqlDbType = SqlDbType.DateTime;
                    SqlParameter FoodLicNoParameter = cmd.Parameters.AddWithValue("@FoodLicNo", model.FoodLicNo);
                    FoodLicNoParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter FoodLicExpDateParameter = cmd.Parameters.AddWithValue("@FoodLicExpDate", model.FoodLicExpDate);
                    FoodLicExpDateParameter.SqlDbType = SqlDbType.DateTime;
                    SqlParameter IsActiveParameter = cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    IsActiveParameter.SqlDbType = SqlDbType.Char;
                    SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@Addedby", model.Addedby);
                    AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter BnkDtlsParameter = cmd.Parameters.AddWithValue("@BnkDtls", dt);
                    BnkDtlsParameter.SqlDbType = SqlDbType.Structured;
                    SqlParameter ActionParameter = cmd.Parameters.AddWithValue("@Action", model.Action);
                    ActionParameter.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter RetValueParameter = cmd.Parameters.AddWithValue("@RetValue", 0);
                    RetValueParameter.Direction = ParameterDirection.Output;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    RetValue = cmd.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.CompanyId, "StockistDtlsAddEditPvt", "Add Edit Stockiest Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Get Stockist by Id
        public List<BankModel> GetStockistBankList(int StockistId)
        {
            return GetStockistBankListPvt(StockistId);
        }
        private List<BankModel> GetStockistBankListPvt(int StockistId)
        {
            List<BankModel> BankModel = new List<BankModel>();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    BankModel = ContextManager.usp_StockistBankListById(StockistId).Select(x => new BankModel()
                    {
                        StockistId = x.StockistId,
                        BankId = x.BankId,
                        BankName = x.BankName,
                        AccountNo = x.AccountNo,
                        IFSCCode = x.IFSCCode
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistBankListPvt", "Get Stockist Bank List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return BankModel;
        }
        #endregion

        #region Get Stokist Transport Mapping List
        public List<StokistTransportModel> GetStokistTransportMappingList(int BranchId, int CompanyId)
        {
            return GetStokistTransportMappingListPvt(BranchId, CompanyId);
        }
        private List<StokistTransportModel> GetStokistTransportMappingListPvt(int BranchId, int CompanyId)
        {
            List<StokistTransportModel> StockMapLst = new List<StokistTransportModel>();
            try
            {
                StockMapLst = _contextManager.usp_StokistTransportMappingList(BranchId, CompanyId).Select(c => new StokistTransportModel
                {
                    Mappingid = c.Mappingid,
                    BranchId = c.BranchId,
                    CompanyId = c.CompanyId,
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    Emailid = c.Emailid,
                    MobNo = c.MobNo,
                    CityCode = c.CityCode,
                    LocationId = Convert.ToInt32(c.LocationId),
                    TransporterId = c.TransporterId,
                    TransporterNo = c.TransporterNo,
                    TransporterName = c.TransporterName,
                    TransitDays = c.TransitDays,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    SupplyTypeId = c.SupplyTypeId,
                    MasterName = c.MasterName

                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStokistTransportMappingListPvt", "Get Stokist Transport Mapping List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockMapLst;
        }
        #endregion

        #region Stokist Transport Mapping AddEdit
        public string StokistTransportMappingAddEdit(StokistTransportModel model)
        {
            return StokistTransportMappingAddEditPvt(model);
        }
        private string StokistTransportMappingAddEditPvt(StokistTransportModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_StokistTransportMappingAddEdit(model.BranchId, model.CompanyId, model.StockistId, model.TransporterId, model.TransitDays, model.SupplyTypeId, model.Addedby, model.AddedOn, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "StokistTransportMappingAddEditPvt", "Stokist Transport Mapping AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Carting Agent Details
        public List<cartingAgentmodel> GetCartingAgentLst(string Status, int BranchId)
        {
            return GetCartingAgentLstPvt(Status, BranchId);
        }
        private List<cartingAgentmodel> GetCartingAgentLstPvt(string Status, int BranchId)
        {
            List<cartingAgentmodel> CAList = new List<cartingAgentmodel>();

            try
            {
                CAList = _contextManager.usp_CartingAgentMasterList(Status, BranchId).Select(c => new cartingAgentmodel
                {
                    CAId = c.CAId,
                    BranchId = Convert.ToInt32(c.BranchId),
                    BranchName = c.BranchName,
                    CAName = c.CAName,
                    CAMobNo = c.CAMobNo,
                    CAEmail = c.CAEmail,
                    CAPan = c.CAPan,
                    GSTNo = c.GSTNo,
                    CAAddress = c.CAAddress,
                    StateCode = c.StateCode,
                    StateName = c.StateName,
                    DistrictCode = c.DistrictCode,
                    DistrictName = c.DistrictName,
                    CityCode = c.CityCode,
                    CityName = c.CityName,
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)
                }).OrderByDescending(x => x.CAId).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCartingAgentLstPvt", "Get carting Agent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CAList;
        }

        public string CartingAgentMasterAddEdit(cartingAgentmodel model)
        {
            return CartingAgentMasterAddEditPvt(model);
        }
        private string CartingAgentMasterAddEditPvt(cartingAgentmodel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CartingAgentMasterAddEdit(model.CAId, model.BranchId, model.CAName, model.CAMobNo, model.CAEmail, model.CAPan, model.GSTNo, model.CAAddress, model.StateCode, model.DistrictCode, model.CityCode, model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CartingAgentMasterAddEditPvt", "Carting Agent AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Get District List
        public GetDistrictList GetDistrictList(string StateCode, string Flag)
        {
            return GetDistrictDtlsPvt(StateCode, Flag);
        }
        private GetDistrictList GetDistrictDtlsPvt(string StateCode, string Flag)
        {
            GetDistrictList getdistModel = new GetDistrictList();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    getdistModel.GetDistrictParameter = ContextManager.usp_GetDistrictList(StateCode, Flag).Select(x => new GetDistrictDtls()
                    {
                        DistrictCode = x.DistrictCode,
                        DistrictName = x.DistrictName,
                        StateName = x.StateName,
                        StateCode = x.StateCode,
                        ActiveFlag = x.ActiveFlag,
                        LastUpdateBy = x.LastUpdateBy,
                        LastUpdateTime = Convert.ToDateTime(x.LastUpdateTime)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDistrictDtlsPvt", "Get District List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return getdistModel;
        }
        #endregion

        #region Get Employee Master List
        public List<EmployeeMasterList> GetEmployeeMasterList(int BranchId, string Status)
        {
            return GetEmployeeMasterListPvt(BranchId, Status);
        }
        private List<EmployeeMasterList> GetEmployeeMasterListPvt(int BranchId, string Status)
        {
            List<EmployeeMasterList> objList = new List<EmployeeMasterList>();

            try
            {
                objList = _contextManager.usp_EmployeeMasterList(BranchId, Status).Select(e => new EmployeeMasterList
                {
                    EmpId = e.EmpId,
                    BranchId = Convert.ToInt32(e.BranchId),
                    EmpNo = e.EmpNo,
                    BranchCode = e.BranchCode,
                    BranchName = e.BranchName,
                    EmpName = e.EmpName,
                    EmpPAN = e.EmpPAN,
                    EmpEmail = e.EmpEmail,
                    EmpMobNo = e.EmpMobNo,
                    EmpAddress = e.EmpAddress,
                    CityCode = e.CityCode,
                    CityName = e.CityName,
                    DesignationId = Convert.ToInt32(e.DesignationId),
                    DesignationName = e.DesignationName,
                    pkId = Convert.ToInt32(e.pkId),
                    BloodGroupName = e.BloodGroupName,
                    AadharNo = e.AadharNo,
                    IsUser = e.IsUser,
                    IsActive = e.IsActive,
                    Addedby = e.Addedby,
                    AddedOn = Convert.ToDateTime(e.AddedOn).ToString(BusinessCont.DateFormat),
                    LastUpdatedOn = Convert.ToDateTime(e.LastUpdatedOn).ToString(BusinessCont.DateFormat),
                    RoleId = Convert.ToInt32(e.RoleId),
                    UserName = e.UserName,
                    Password = e.Password,
                    UserStatus = e.UserStatus
                }).OrderByDescending(x => x.EmpId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetEmployeeMasterListPvt", "Get Employee Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return objList;
        }
        #endregion

        #region Api for Update User Activation
        public string UserActiveDeactive(EmployeeActiveModel model)
        {
            return UserActiveDeactivePvt(model);
        }
        private string UserActiveDeactivePvt(EmployeeActiveModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_UserActiveDeactive(model.EmpId, model.IsActive, model.Addedby, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.EmpId, "UserActiveDeactivePvt", "Update User Activate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Courier Master
        public string AddEditCourierMaster(CourierMasterLst model)
        {
            return AddEditCourierMasterPvt(model);
        }
        private string AddEditCourierMasterPvt(CourierMasterLst model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CourierMasterAddEdit(model.CourierId, model.BranchId, model.CourierName, model.CourierEmail, model.CourierMobNo, model.CourierAddress, model.CityCode, model.StateCode, model.DistrictCode, model.RatePerBox, model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCourierMaster", "Add Edit Courier Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }

        public List<CourierMasterLst> GetcourierMasterList(int BranchId, string DistrictCode, string Status)
        {
            return GetcourierMasterListPvt(BranchId, DistrictCode, Status);
        }
        private List<CourierMasterLst> GetcourierMasterListPvt(int BranchId, string DistrictCode, string Status)
        {
            List<CourierMasterLst> CourierList = new List<CourierMasterLst>();
            try
            {
                CourierList = _contextManager.usp_CourierMasterList(BranchId, DistrictCode, Status).Select(x => new CourierMasterLst
                {
                    CourierId = x.CourierId,
                    BranchId = Convert.ToInt32(x.BranchId),
                    BranchName = x.BranchName,
                    CourierName = x.CourierName,
                    CourierEmail = x.CourierEmail,
                    CourierMobNo = x.CourierMobNo,
                    CourierAddress = x.CourierAddress,
                    CityCode = x.CityCode,
                    CityName = x.CityName,
                    StateCode = x.StateCode,
                    StateName = x.StateName,
                    DistrictCode = x.DistrictCode,
                    RatePerBox = Convert.ToInt32(x.RatePerBox),
                    DistrictName = x.DistrictName,
                    IsActive = x.IsActive,
                    Addedby = x.Addedby,
                    LastUpdatedOn = Convert.ToDateTime(x.LastUpdatedOn),
                    DisplayName = x.DisplayName
                }).OrderByDescending(x => x.CourierId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "usp_CourierMasterList", "Get courier Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CourierList;
        }
        #endregion

        #region Get User Details
        public UserDtls GetUserDtls(int UserId)
        {
            return GetUserDtlsPvt(UserId);
        }
        private UserDtls GetUserDtlsPvt(int UserId)
        {
            UserDtls UserLst = new UserDtls();
            try
            {
                UserLst = _contextManager.usp_GetUserDetailsForChangePwd(UserId).Select(x => new UserDtls
                {
                    UserId = x.UserId,
                    EmpId = x.EmpId,
                    DisplayName = x.DisplayName,
                    UserName = x.UserName,
                    EmpEmail = x.EmpEmail,
                    Password = x.Password,
                    EncryptPassword = x.EncryptPassword,
                    Designation = x.Designation,
                    BloodGroup = x.BloodGroup,
                    AadharNo = x.AadharNo,
                    EmpMobNo = x.EmpMobNo,
                    IsActive = x.IsActive,
                    EmpNo = x.EmpNo
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetUserDtlsPvt", " Get User Dtls", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return UserLst;
        }
        #endregion

        #region Get Transporter By Id
        public TransporterMasterLst GetTransporterById(int TransporterId)
        {
            return GetTransporterByIdPvt(TransporterId);
        }
        private TransporterMasterLst GetTransporterByIdPvt(int TransporterId)
        {
            TransporterMasterLst transporterMasterList = new TransporterMasterLst();
            try
            {
                transporterMasterList = _contextManager.usp_TransporterById(TransporterId).Select(x => new TransporterMasterLst
                {
                    TransporterId = x.TransporterId,
                    BranchId = Convert.ToInt32(x.BranchId),
                    TransporterNo = x.TransporterNo,
                    TransporterName = x.TransporterName,
                    TransporterEmail = x.TransporterEmail,
                    TransporterMobNo = x.TransporterMobNo,
                    TransporterAddress = x.TransporterAddress,
                    CityCode = x.CityCode,
                    CityName = x.CityName,
                    StateCode = x.StateCode,
                    StateName = x.StateName,
                    DistrictCode = x.DistrictCode,
                    DistrictName = x.DistrictName,
                    IsActive = x.IsActive
                }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterByIdPvt", "Get Transporter By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return transporterMasterList;
        }
        #endregion

        #region Get Branch Details By BranchId
        public List<BranchIdDtls> GetBranchByIdDtls(int BranchId)
        {
            return GetBranchByIdDtlsPvt(BranchId);
        }
        private List<BranchIdDtls> GetBranchByIdDtlsPvt(int BranchId)
        {
            List<BranchIdDtls> model = new List<BranchIdDtls>();
            try
            {
                model = _contextManager.usp_GetBranchById(BranchId).Select(c => new BranchIdDtls
                {
                    BranchId = c.BranchId,
                    BranchName = c.BranchName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetBranchByIdDtlsPvt", "Get Branch Details By BranchId", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion


        #region Add Stockist Company Relation
        public string AddEditStockistCompanyRelation(StockistRelation model)
        {
            return AddEditStockistCompanyRelationPvt(model);
        }
        private string AddEditStockistCompanyRelationPvt(StockistRelation model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_StockiestCompanyRelationAddEdit(model.pkid, model.Stockieststr, model.CompId, model.AddedBy, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditStockistCompanyRelation", "Add Edit Stockist Company Relation", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Add Stockist Branch Relation
        public string AddEditStockistBranchRelation(StockistRelation model)
        {
            return AddEditStockistBranchRelationPvt(model);
        }
        private string AddEditStockistBranchRelationPvt(StockistRelation model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_StockiestBranchRelationAddEdit(model.pkid, model.Stockieststr, model.BranchId, model.AddedBy, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditStockistBranchRelation", "Add Edit Stockist Branch Relation", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Branch Stockist Relation List
        public List<StockistRelation> GetStockistBranchRelationList(int BranchId)
        {
            return GetStockistBranchRelationListPvt(BranchId);
        }
        private List<StockistRelation> GetStockistBranchRelationListPvt(int BranchId)
        {
            List<StockistRelation> model = new List<StockistRelation>();
            try
            {
                model = _contextManager.usp_StockistBranchRelationList(BranchId).Select(c => new StockistRelation
                {
                    pkid = c.PkId,
                    StockistId = c.StockiestId,
                    StockistName = c.StockistName,
                    BranchId = c.BranchId,
                    BranchName = c.BranchName,
                    StockistNo = c.StockistNo,
                    BranchCode = c.BranchCode
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetStockistBranchListPvt", "Get Stockist Branch Relation List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Stockist Company Relation List
        public List<StockistRelation> GetStockistCompanyRelationList(int CompId)
        {
            return GetStockistCompanyRelationListPvt(CompId);
        }
        private List<StockistRelation> GetStockistCompanyRelationListPvt(int CompId)
        {
            List<StockistRelation> model = new List<StockistRelation>();
            try
            {
                model = _contextManager.usp_StockistCompRelationList(CompId).Select(c => new StockistRelation
                {
                    pkid = c.PkId,
                    StockistId = c.StockiestId,
                    StockistName = c.StockistName,
                    CompId = c.CompId,
                    CompName = c.CompanyName,
                    StockistNo = c.StockistNo,
                    CompanyCode = c.CompanyCode

                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompId, "GetStockistCompanyRelationListPvt", "Get Stockist Company Relation List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check Username Available
        public CheckUsernameAvailableModel GetCheckUsernameAvailable(string Username)
        {
            return GetCheckUsernameAvailablePvt(Username);
        }

        private CheckUsernameAvailableModel GetCheckUsernameAvailablePvt(string Username)
        {
            CheckUsernameAvailableModel model = new CheckUsernameAvailableModel();

            try
            {
                model = _contextManager.usp_checkUsernameAvailable(Username).Select(c => new CheckUsernameAvailableModel
                {
                    UserId = c.UserId,
                    BranchId = Convert.ToInt32(c.BranchId),
                    RoleId = c.RoleId,
                    EmpId = c.EmpId,
                    DisplayName = c.DisplayName,
                    UserName = c.UserName,
                    Password = c.Password,
                    EncryptPassword = c.EncryptPassword,
                    IsActive = c.IsActive,
                    AddedBy = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckUsernameAvailablePvt", "Get Check Username Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Stockist List By Branch 
        public List<StockistModel> GetStockistListByBranch(int BranchId, string Status)
        {
            return GetStockisListByBranchPvt(BranchId, Status);
        }
        private List<StockistModel> GetStockisListByBranchPvt(int BranchId, string Status)
        {
            List<StockistModel> model = new List<StockistModel>();
            try
            {
                model = _contextManager.usp_StockistMasterListByBranchId(BranchId, Status).Select(c => new StockistModel
                {
                    BranchId = Convert.ToInt32(c.BranchId),
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    Checked = c.Checked,
                    CityName = c.CityName,
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetStockisListByBranchPvt", "Get Stockist List By Branch ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Stockist List By Company 
        public List<StockistModel> GetStockistListByCompany(int CompanyId, string Status)
        {
            return GetStockistListByCompanyPvt(CompanyId, Status);
        }
        private List<StockistModel> GetStockistListByCompanyPvt(int CompanyId, string Status)
        {
            List<StockistModel> model = new List<StockistModel>();
            try
            {
                model = _contextManager.usp_StockistMasterListByCompanyId(CompanyId, Status).Select(c => new StockistModel
                {
                    CompanyId = Convert.ToInt32(c.CompId),
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    Checked = c.Checked,
                    CityName = c.CityName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompanyId, "GetStockistListByCompanyPvt", "Get Stockist List By Company", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Roles Detalis
        public List<RolesModel> GetRolesls(int EmpId)
        {
            return GetRoleDtlsPvt(EmpId);
        }
        private List<RolesModel> GetRoleDtlsPvt(int EmpId)
        {
            List<RolesModel> RoleLst = new List<RolesModel>();
            try
            {
                RoleLst = _contextManager.usp_UserRoleDetails(EmpId).Select(c => new RolesModel
                {
                    EmpId = c.EmpId,
                    RoleId = Convert.ToInt32(c.RoleId),
                    RoleName = c.RoleName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleDtlsPvt", "Get Roles Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Get Guard Details
        public List<GuardDetails> GetGuardDetails(int BranchId, int CompId)
        {
            return GetGuardDetailsPvt(BranchId, CompId);
        }
        private List<GuardDetails> GetGuardDetailsPvt(int BranchId, int CompId)
        {
            List<GuardDetails> guardDetails = new List<GuardDetails>();
            try
            {
                guardDetails = _contextManager.usp_GetGuardDetails(BranchId, CompId).Select(c => new GuardDetails
                {
                    EmpId = c.EmpId,
                    EmpName = c.EmpName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGuardDetailsPvt", "Get Guard Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return guardDetails;
        }
        #endregion

        #region Create User
        public string CreateUser(CreateUserModel model)
        {
            return CreateUserPvt(model);
        }
        private string CreateUserPvt(CreateUserModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_UserAdd(model.BranchId, model.EmpId, model.RoleIdStr, model.UserName,
                    model.Password, model.EncryptPassword, model.Addedby, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CreateUserPvt", "Create User", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.msg_exist;
            }
        }
        #endregion

        #region Get Check Stockist Already Available
        public StockistModel GetStockistNoAvailables(string StockistNo)
        {
            return GetStockistNoAvailablesPvt(StockistNo);
        }
        private StockistModel GetStockistNoAvailablesPvt(string StockistNo)
        {
            StockistModel model = new StockistModel();
            try
            {
                model = _contextManager.usp_checkStockistNo(StockistNo).Select(c => new StockistModel
                {
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    Flag = c.Flag
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistNoAvailablesPvt", "Get Check StocksitNo Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check TransporterNo is Available
        public TransporterMasterLst GetTransporterNoAvailables(string TransporterNo)
        {
            return GetTransporterNoAvailablesPvt(TransporterNo);
        }
        private TransporterMasterLst GetTransporterNoAvailablesPvt(string TransporterNo)
        {
            TransporterMasterLst model = new TransporterMasterLst();
            try
            {
                model = _contextManager.usp_checkTransporterMaster(TransporterNo).Select(c => new TransporterMasterLst
                {
                    TransporterNo = c.TransporterNo,
                    TransporterName = c.TransporterName,
                    Flag = c.Flag
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterNoAvailablesPvt", "Get Check StockistNo Already Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check Employee Number,Email,Mobile Available
        public AddEmployeeModel GetCheckEmployeeNumberAvilable(int EmpId, string EmpNo, string EmpEmail, string EmpMobNo)
        {
            return GetCheckEmployeeNumberAvilablePvt(EmpId, EmpNo, EmpEmail, EmpMobNo);
        }
        private AddEmployeeModel GetCheckEmployeeNumberAvilablePvt(int EmpId, string EmpNo, string EmpEmail, string EmpMobNo)
        {
            AddEmployeeModel model = new AddEmployeeModel();
            try
            {
                model = _contextManager.usp_checkEmpNo(EmpId, EmpNo, EmpEmail, EmpMobNo).Select(c => new AddEmployeeModel
                {
                    flag = c.Flag,
                    EmpId = c.Empid,
                    EmpNo = c.EmpNo,
                    EmpName = c.EmpName
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckEmployeeNumberAvilablePvt", "Get Check Employee Number Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check carting Agent Name
        public cartingAgentmodel GetCheckCartingAgentAvilable(string CAName)
        {
            return GetCheckCartingAgentAvilablePvt(CAName);
        }
        private cartingAgentmodel GetCheckCartingAgentAvilablePvt(string CAName)
        {
            cartingAgentmodel model = new cartingAgentmodel();
            try
            {
                model = _contextManager.usp_checkCAName(CAName).Select(c => new cartingAgentmodel
                {
                    flag = c.Flag,
                    CAId = c.CAId,
                    CAName = c.CAName
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckCartingAgentAvilablePvt", "Get check Carting Agent Name", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check Courier Name
        public CourierMasterLst GetCheckCourierNameAvilable(string CourierName)
        {
            return GetCheckCourierNameAvilablePvt(CourierName);
        }
        private CourierMasterLst GetCheckCourierNameAvilablePvt(string CourierName)
        {
            CourierMasterLst model = new CourierMasterLst();
            try
            {
                model = _contextManager.usp_checkCourierName(CourierName).Select(c => new CourierMasterLst
                {
                    flag = c.Flag,
                    CourierId = c.CourierId,
                }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckCourierNameAvilablePvt", "Get check Courier Name", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Edit City Master
        public string AddEditCityMaster(CityMaster model)
        {
            return AddEditCityMasterPvt(model);
        }
        private string AddEditCityMasterPvt(CityMaster model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CityMastersAddEdit(model.CityCode, model.StateCode, model.CityName, model.ActiveFlag, model.Action, model.AddedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCityMaster", "Add Edit City Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "STATUS")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.msg_stsChange;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }

        }
        #endregion

        #region Get Threshold value List
        public List<ThresholdValueDtls> GetThresholdvalueDtls(int BranchId, int CompanyId)
        {
            return GetThresholdvaluePvt(BranchId, CompanyId);
        }

        private List<ThresholdValueDtls> GetThresholdvaluePvt(int BranchId, int CompanyId)
        {
            List<ThresholdValueDtls> ThresholdList = new List<ThresholdValueDtls>();
            try
            {
                ThresholdList = _contextManager.usp_GetThresholdSLAMasterList(BranchId, CompanyId).Select(c => new ThresholdValueDtls
                {
                    PkId = c.PkId,
                    BranchId = Convert.ToInt32(c.BranchId),
                    BranchName = c.BranchName,
                    CompanyId = Convert.ToInt32(c.CompanyId),
                    CompanyName = c.CompanyName,
                    ThresholdValue = Convert.ToInt32(c.ThresholdValue),
                    RaiseClaimDay = Convert.ToInt32(c.RaiseClaimDay),
                    ClaimSettlementDay = Convert.ToInt32(c.ClaimSettlementDay),
                    InStateAmt = Convert.ToInt64(c.InStateAmt),
                    OutStateAmt = Convert.ToInt64(c.OutStateAmt),
                    SaleSettlePeriod = Convert.ToInt32(c.SaleSettlePeriod),
                    NonSaleSettlePeriod = Convert.ToInt32(c.NonSaleSettlePeriod),
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn).ToString(BusinessCont.DateFormat),
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetThresholdvaluePvt", "Get Threshold value ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ThresholdList;
        }
        #endregion

        #region Add Edit Threshold Value Master
        public int AddEditThresholdValueMaster(ThresholdValueDtls model)
        {
            return AddEditThresholdValueMasterPvt(model);
        }
        private int AddEditThresholdValueMasterPvt(ThresholdValueDtls model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(string));
            try
            {
                RetValue = _contextManager.usp_ThresholdValueAddEdit(model.PkId, model.BranchId, model.CompanyId, model.ThresholdValue, model.RaiseClaimDay,
                model.ClaimSettlementDay, model.InStateAmt, model.OutStateAmt, model.SaleSettlePeriod, model.NonSaleSettlePeriod, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditThresholdValueMaster", "Add Edit Threshold Value Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Add/Edit Checklist Masters
        public int ChecklistMastersAddEdit(ChecklistMastersAddEditModel model)
        {
            return ChecklistMastersAddEditPvt(model);
        }
        private int ChecklistMastersAddEditPvt(ChecklistMastersAddEditModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_ChecklistMastersAddEdit(model.ChecklistTypeId, model.BranchId, model.CompId,
                model.QuestionName, model.ControlType, model.SeqNo, model.Addedby, model.Action, model.IsActive, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChecklistMastersAddEditPvt", "Checklist Masters Add Edit Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Checklist Master List
        public List<ChecklistMastersAddEditModel> GetChecklistMasterList(int BranchId, int CompId, string Status)
        {
            return GetChecklistMasterListPvt(BranchId, CompId, Status);
        }
        private List<ChecklistMastersAddEditModel> GetChecklistMasterListPvt(int BranchId, int CompId, string Status)
        {
            List<ChecklistMastersAddEditModel> GetChecklistMasterLst = new List<ChecklistMastersAddEditModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    GetChecklistMasterLst = _contextManager.usp_GetChecklistMasterList(BranchId, CompId, Status)
                        .Select(c => new ChecklistMastersAddEditModel
                        {
                            ChecklistTypeId = c.ChecklistTypeId,
                            CompanyName = c.CompanyName,
                            BranchId = Convert.ToInt32(c.BranchId),
                            CompanyId = Convert.ToInt32(c.CompanyId),
                            QuestionName = c.QuestionName,
                            ControlType = c.ControlType,
                            SeqNo = Convert.ToInt32(c.SeqNo),
                            Addedby = c.Addedby,
                            IsActive = c.IsActive,
                            BranchName = c.BranchName
                        }).OrderBy(x => x.ChecklistTypeId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChecklistMasterListPvt", "Get Checklist MasterList Pvt" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetChecklistMasterLst;
        }
        #endregion

        #region Get Sequence No Already Available
        public ChecklistMastersAddEditModel GetcheckSequenceNoAvailable(int SeqNo, int BranchId, int CompId)
        {
            return GetcheckSequenceNoAvailablePvt(SeqNo, BranchId, CompId);
        }
        private ChecklistMastersAddEditModel GetcheckSequenceNoAvailablePvt(int SeqNo, int BranchId, int CompId)
        {
            ChecklistMastersAddEditModel model = new ChecklistMastersAddEditModel();
            try
            {
                model = _contextManager.usp_checkSequenceNo(SeqNo, BranchId, CompId).Select(c => new ChecklistMastersAddEditModel
                {
                    SeqNo = Convert.ToInt32(c.SeqNo),
                    QuestionName = c.QuestionName,
                    Flag = c.Flag
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetcheckSequenceNoAvailablePvt", "Get check SequenceNo Available Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Other CNF Master Add Edit
        public int OtherCNFMasterAddEdit(OtherCNFDtlsModel model)
        {
            return OtherCNFMasterAddEditPvt(model);
        }
        private int OtherCNFMasterAddEditPvt(OtherCNFDtlsModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_OtherCNFMasterAddEdit(model.CNFId, model.BranchId, model.CompId, model.CNFCode, model.CNFName, model.CityCode, model.CNFEmail, model.ContactPerson, model.ContactNo, model.CNFAddress, model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OtherCNFMasterAddEditPvt", "Other CNF Master Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Other CNF List
        public List<OtherCNFDtlsModel> GetOtherCNFList(int BranchId, int CompId, string Status)
        {
            return GetOtherCNFListPvt(BranchId, CompId, Status);
        }
        private List<OtherCNFDtlsModel> GetOtherCNFListPvt(int BranchId, int CompId, string Status)
        {
            List<OtherCNFDtlsModel> modelList = new List<OtherCNFDtlsModel>();
            try
            {
                modelList = _contextManager.usp_OtherCNFMasterList(BranchId, CompId, Status).Select(c => new OtherCNFDtlsModel
                {
                    CNFId = c.CNFId,
                    CNFCode = c.CNFCode,
                    CNFName = c.CNFName,
                    CityCode = c.CityCode,
                    CNFEmail = c.CNFEmail,
                    CityName = c.CityName,
                    ContactPerson = c.ContactPerson,
                    ContactNo = c.ContactNo,
                    CNFAddress = c.CNFAddress,
                    IsActive = c.IsActive,
                    Addedby = c.Addedby
                }).OrderByDescending(x => x.CNFId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOtherCNFListPvt", "Get Other CNF List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Add Version Details (Mobile & Web Application)
        public string AddVersionDetails(VersionDetailsModel model)
        {
            return AddVersionDetailsPvt(model);
        }

        private string AddVersionDetailsPvt(VersionDetailsModel model)
        {
            string message = string.Empty;
            ObjectParameter RetVal = new ObjectParameter("RetVal", typeof(string));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    var result = contextManager.usp_AddVersionDetails(model.VersionNo, Convert.ToDateTime(model.VersionDateTime), RetVal).FirstOrDefault();
                    message = Convert.ToString(result);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddVersionDetailsPvt", "Add Version Details (Mobile & Web Application) - BranchId:  " + model.BranchId + " CompanyId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message;
        }
        #endregion

        #region Get Version Details
        public List<VersionDetailsModel> GetVersionDetails()
        {
            return GetVersionDetailsPvt();
        }

        private List<VersionDetailsModel> GetVersionDetailsPvt()
        {
            List<VersionDetailsModel> modelList = new List<VersionDetailsModel>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modelList = contextManager.usp_GetVersionDetails().Select(v => new VersionDetailsModel
                    {
                        VersionId = v.VersionId,
                        VersionNo = v.VersionNo,
                        IsActive = v.IsActive,
                        VersionDate = Convert.ToDateTime(v.VersionDate).ToString("yyyy-MM-dd"),
                        AddedOn = Convert.ToDateTime(v.AddedOn),
                        LastUpdatedDate = Convert.ToDateTime(v.LastUpdatedDate)
                    }).OrderByDescending(c => c.VersionId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVersionDetailsPvt", "Get Version Details (Web Application)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region To Check Version Number
        public CheckVersionNoModel CheckVersionNo(string VersionNo)
        {
            return CheckVersionNoPvt(VersionNo);
        }

        private CheckVersionNoModel CheckVersionNoPvt(string VersionNo)
        {
            CheckVersionNoModel model = new CheckVersionNoModel();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    model = contextManager.usp_checkVersionNo(VersionNo).Select(c => new CheckVersionNoModel
                    {
                        VersionNo = c.VersionNo,
                        Flag = c.Flag
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckVersionNoPvt", "To Check Version Number (Web Application)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Version Details By Id
        public string GetLatestVersionDetails()
        {
            return GetLatestVersionDetailsPvt();
        }

        private string GetLatestVersionDetailsPvt()
        {
            string message = string.Empty;
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    var response = contextManager.usp_GetLatestVersionDetails().FirstOrDefault();
                    message = response.VersionNo;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVersionDetailsByIdPvt", "Get Version Details By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message;
        }
        #endregion

        #region Get Branch Company Relation List
        public List<CompanyRelation> GetComapnyBranchRelationList(int BranchId)
        {
            return GetComapnyBranchRelationListPvt(BranchId);
        }
        private List<CompanyRelation> GetComapnyBranchRelationListPvt(int BranchId)
        {
            List<CompanyRelation> model = new List<CompanyRelation>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_CompanyBranchRelationList(BranchId).Select(c => new CompanyRelation
                    {
                        PkId = Convert.ToInt32(c.PkId),
                        CompanyId = Convert.ToInt32(c.CompanyId),
                        CompanyName = Convert.ToString(c.CompanyName),
                        BranchId = Convert.ToInt32(c.BranchId),
                        BranchName = Convert.ToString(c.BranchName),
                        CompanyCode = Convert.ToString(c.CompanyCode),
                        BranchCode = Convert.ToString(c.BranchCode)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetComapnyBranchRelationListPvt", "Get Comapny Branch Relation List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region  Get Company List By Branch 
        public List<CompanyDtls> GetCompanyListByBranch(int BranchId, string Status)
        {
            return GetCompanyListByBranchPvt(BranchId, Status);
        }
        private List<CompanyDtls> GetCompanyListByBranchPvt(int BranchId, string Status)
        {
            List<CompanyDtls> model = new List<CompanyDtls>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_CompanyMasterListByBranchId(BranchId, Status).Select(c => new CompanyDtls
                    {
                        CompanyId = Convert.ToInt32(c.CompanyId),
                        CompanyCode = Convert.ToString(c.CompanyCode),
                        CompanyName = Convert.ToString(c.CompanyName),
                        Checked = Convert.ToInt32(c.Checked)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetCompanyListByBranchPvt", "Get Company List By Branch Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Company Branch Relation
        public int AddEditCompanyBranchRelation(CompBranchRelation model)
        {
            return AddEditCompanyBranchRelationPvt(model);
        }
        private int AddEditCompanyBranchRelationPvt(CompBranchRelation model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var response = _contextManager.usp_CompanyBranchRelationAddEdit(model.CompanyIdstr, model.BranchId, model.AddedBy, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(response.RetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCompanyBranchRelation", "Add Edit Company Branch Relation", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Add Vendor Details
        public int AddVendorDetails(VendorDetailsModel model)
        {
            return AddVendorDetailsPvt(model);
        }

        private int AddVendorDetailsPvt(VendorDetailsModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_VendorMasterAddEdit(model.VendorId, model.BranchId, model.VendorName, model.Email, model.ContactNumber,
                        model.PANNumber, model.IsGST, model.GSTNumber, model.City, model.Address, model.IsActive, model.AddedBy, model.Action, model.IsTDS, model.TDSPer, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddVersionDetailsPvt", "Add Version Details (Mobile & Web Application) - BranchId:  " + model.BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Vendor List
        public List<VendorDetailsModel> GetVendorList(int Branch, string Status)
        {
            return GetVendorListPvt(Branch, Status);
        }

        private List<VendorDetailsModel> GetVendorListPvt(int Branch, string Status)
        {
            List<VendorDetailsModel> model = new List<VendorDetailsModel>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    model = contextManager.usp_GetVendorMasterList(Branch, Status).Select(v => new VendorDetailsModel
                    {
                        VendorId = v.VendorId,
                        BranchId = v.Branch,
                        VendorName = v.VendorName,
                        Email = v.Email,
                        ContactNumber = v.ContactNumber,
                        PANNumber = v.PANNumber,
                        GSTNumber = v.GSTNumber,
                        City = Convert.ToInt32(v.City),
                        CityName = v.CityName,
                        Address = v.Address,
                        IsActive = v.IsActive,
                        IsGST = v.IsGST,
                        IsTDS = v.IsTDS,
                        TDSPer = Convert.ToInt32(v.TDSPer),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVendorListPvt", "Get Vendor List - BranchId:  " + Branch, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Vendor Delete Deactive
        public int VendorDeleteDeactivate(VendorDetailsModel model)
        {
            return VendorDeleteDeactivatePvt(model);
        }

        private int VendorDeleteDeactivatePvt(VendorDetailsModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_VendorMasterDeleteDeactivate(model.VendorId, model.IsActive, model.AddedBy, model.Action, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "VendordeletestatusPvt", "Delete Vendor or Deactivate Vendor", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Add Edit Tax Master
        public int TaxMasterAddEdit(TaxMastermodel model)
        {
            return TaxMasterAddEditpvt(model);
        }
        private int TaxMasterAddEditpvt(TaxMastermodel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_TAXMasterAddEdit(model.TaxId, model.BranchId, model.GSTType, model.CGST, model.SGST, model.AddedBy, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TaxMasterAddEditpvt", "Add Edit Tax Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get TaxMaster List
        public List<TaxMastermodel> GetTaxMaster(int BranchId)
        {
            return GetTaxMasterPvt(BranchId);
        }
        private List<TaxMastermodel> GetTaxMasterPvt(int BranchId)
        {
            List<TaxMastermodel> TaxLst = new List<TaxMastermodel>();
            try
            {
                TaxLst = _contextManager.usp_GetTaxMasterList(BranchId).Select(c => new TaxMastermodel
                {
                    TaxId = c.TaxId,
                    GSTType = c.GSTType,
                    CGST = Convert.ToInt32(c.CGST),
                    SGST = Convert.ToInt32(c.SGST),
                    AddedBy = c.AddedBy,
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleLstPvt", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TaxLst;
        }
        #endregion

        #region Add EditHead Master
        public int HeadMasterAddEdit(HeadMasterModel model)
        {
            return HeadMasterAddEditPvt(model);
        }
        private int HeadMasterAddEditPvt(HeadMasterModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_HeadMasterAddEdit(model.pkId, model.BranchId, model.HeadName, model.HeadTypeId,
                    model.IsActiveStatus, model.Addedby, model.AddedOn, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "HeadMasterAddEditPvt", "Head Master Add Edit Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Head Master List
        public List<HeadMasterModel> HeadMasterList(int BranchId)
        {
            return HeadMasterListPvt(BranchId);
        }
        private List<HeadMasterModel> HeadMasterListPvt(int BranchId)
        {
            List<HeadMasterModel> HeadMasterList = new List<HeadMasterModel>();
            try
            {
                HeadMasterList = _contextManager.usp_GetHeadMasterList(BranchId).Select(c => new HeadMasterModel
                {
                    pkId = c.pkId,
                    BranchId = c.BranchId,
                    HeadName = c.HeadName,
                    HeadTypeId = c.HeadTypeId,
                    HeadType = c.HeadType,
                    IsActiveStatus = Convert.ToString(c.IsActiveStatus),
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                }).OrderByDescending(x => x.pkId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "HeadMasterListPvt", "Add Cheque Register", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return HeadMasterList;
        }
        #endregion

        #region Transporter Parent Add Edit
        public int TransporterParentAddEdit(TransporterParentModel model)
        {
            return TransporterParentAddEditPvt(model);
        }
        private int TransporterParentAddEditPvt(TransporterParentModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = Convert.ToInt32(_contextManager.usp_TransporterParentAddEdit(model.Tid, model.BranchId, model.ParentTranspNo, model.ParentTranspName, model.ParentTranspEmail, model.ParentTranspMobNo, model.IsTDS, model.TDSPer, model.IsGST, model.GSTNumber,
                    model.IsActive, model.Addedby, model.Action).FirstOrDefault());
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransporterParentAddEditPvt", "Transporter Parent Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Transporter Parent List
        public List<TransporterParentModel> GetTransporterParentList(int BranchId, string Status)
        {
            return GetTransporterParentListPvt(BranchId, Status);
        }
        private List<TransporterParentModel> GetTransporterParentListPvt(int BranchId, string Status)
        {
            List<TransporterParentModel> transportermasterList = new List<TransporterParentModel>();
            try
            {
                transportermasterList = _contextManager.usp_GetTransporterParentList(BranchId, Status).Select(x => new TransporterParentModel
                {
                    Tid = x.Tpid,
                    BranchId = Convert.ToInt32(x.BranchId),
                    ParentTranspNo = x.ParentTranspNo,
                    ParentTranspName = x.ParentTranspName,
                    ParentTranspEmail = x.ParentTranspEmail,
                    ParentTranspMobNo = x.ParentTranspMobNo,
                    IsTDS = x.IsTDS,
                    TDSPer = Convert.ToInt32(x.TDSPer),
                    IsGST = x.IsGST,
                    GSTNumber = x.GSTNumber,
                    IsActive = x.IsActive
                }).OrderByDescending(x => x.Tid).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterParentListPvt", "Get Transporter Parent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return transportermasterList;
        }
        #endregion

        #region Get Transporter Parent
        public TransporterParentModel GetTransporterParent(int Tpid)
        {
            return GetTransporterParentPvt(Tpid);
        }
        private TransporterParentModel GetTransporterParentPvt(int Tpid)
        {
            TransporterParentModel transporter = new TransporterParentModel();
            try
            {
                transporter = _contextManager.usp_GetParentTransporter(Tpid).Select(x => new TransporterParentModel
                {
                    Tid = x.Tpid,
                    BranchId = Convert.ToInt32(x.BranchId),
                    ParentTranspNo = x.ParentTranspNo,
                    ParentTranspName = x.ParentTranspName,
                    ParentTranspEmail = x.ParentTranspEmail,
                    ParentTranspMobNo = x.ParentTranspMobNo,
                    IsTDS = x.IsTDS,
                    TDSPer = Convert.ToInt32(x.TDSPer),
                    IsGST = x.IsGST,
                    GSTNumber = x.GSTNumber,
                    IsActive = x.IsActive
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterParentListPvt", "Get Transporter Parent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return transporter;
        }
        #endregion

        #region Parent Transporter Mapping Add Edit
        public int ParentTransporterMappingAddEdit(ParentTranporterMappingModel model)
        {
            return ParentTransporterMappingAddEditPvt(model);
        }
        private int ParentTransporterMappingAddEditPvt(ParentTranporterMappingModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var response = _contextManager.usp_TransporterParentMapping(model.BranchId, model.Tid, model.TransporterId, model.AddedBy, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(response.RetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ParentTransporterMappingAddEditPvt", "Parent Transporter Mapping Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region  Get Parent Transport Mapped List
        public List<ParentTransportMappList> GetParentTransportMappedList(int Tpid, string Status,int BranchId)
        {
            return GetParentTransportMappedListPvt(Tpid, Status, BranchId);
        }
        private List<ParentTransportMappList> GetParentTransportMappedListPvt(int Tpid, string Status,int BranchId)
        {
            List<ParentTransportMappList> model = new List<ParentTransportMappList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_ParentTransporterMappedList(Tpid, Status, BranchId).Select(c => new ParentTransportMappList
                    {
                        TransporterId = c.TransporterId,
                        TransporterNo = c.TransporterNo,
                        TransporterName = c.TransporterName,
                        Checked = c.Checked
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, Tpid, "GetParentTransportMappedListPvt", "Get Parent Transport Mapped List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Courier Parent List
        public List<CourierParentModel> GetCourierParentList(int BranchId, string Status)
        {
            return GetCourierParentListPvt(BranchId, Status);
        }
        private List<CourierParentModel> GetCourierParentListPvt(int BranchId, string Status)
        {
            List<CourierParentModel> CourierList = new List<CourierParentModel>();
            try
            {
                CourierList = _contextManager.usp_GetCourierParentList(BranchId, Status).Select(x => new CourierParentModel
                {
                    Cpid = x.Cpid,
                    BranchId = Convert.ToInt32(x.BranchId),
                    ParentCourierName = x.ParentCourierName,
                    ParentCourierEmail = x.ParentCourierEmail,
                    ParentCourierMobNo = x.ParentCourierMobNo,
                    IsTDS = x.IsTDS,
                    TDSPer = Convert.ToInt32(x.TDSPer),
                    IsGST = x.IsGST,
                    GSTNumber = x.GSTNumber,
                    IsActive = x.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterParentListPvt", "Get Transporter Parent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CourierList;
        }
        #endregion

        #region Get Courier Parent
        public CourierParentModel GetCourierParent(int Cpid)
        {
            return GetCourierParentPvt(Cpid);
        }
        private CourierParentModel GetCourierParentPvt(int Cpid)
        {
            CourierParentModel Courier = new CourierParentModel();
            try
            {
                Courier = _contextManager.usp_GetCourierParent(Cpid).Select(x => new CourierParentModel
                {
                    Cpid = x.Cpid,
                    BranchId = Convert.ToInt32(x.BranchId),
                    ParentCourierName = x.ParentCourierName,
                    ParentCourierEmail = x.ParentCourierEmail,
                    ParentCourierMobNo = x.ParentCourierMobNo,
                    IsTDS = x.IsTDS,
                    TDSPer = Convert.ToInt32(x.TDSPer),
                    IsGST = x.IsGST,
                    GSTNumber = x.GSTNumber,
                    IsActive = x.IsActive
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Get Courier Parent", "Get Courier Parent", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Courier;
        }
        #endregion

        #region  Courier Parent Add Edit
        public int CourierParentAddEdit(CourierParentModel model)
        {
            return CourierParentAddEditPvt(model);
        }
        private int CourierParentAddEditPvt(CourierParentModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CourierParentAddEdit(model.Cpid, model.BranchId, model.ParentCourierName, model.ParentCourierEmail, model.ParentCourierMobNo, model.IsTDS, model.TDSPer, model.IsGST, model.GSTNumber,
                    model.IsActive, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CourierParentAddEditPvt", "Courier Parent Add Edit Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }

        #endregion

        #region Get Parent Courier Mapped List
        public List<ParentCourierMappList> GetParentCourierMappList(int CPid, string Status, int BranchId)
        {
            List<ParentCourierMappList> courMappLst = new List<ParentCourierMappList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    courMappLst = _contextManager.usp_ParentCourierMappedList(CPid, Status, BranchId).Select(R => new ParentCourierMappList
                    {
                        CourierId = R.CourierId,
                        CourierName = R.CourierName,
                        CityCode = R.CityCode,
                        CourierEmail = R.CourierEmail,
                        CourierMobNo = R.CourierMobNo,
                        Checked = R.Checked
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CPid, "GetParentCourierMappList", "Get Parent Courier Mapp List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return courMappLst;
        }
        #endregion

        #region Parent Courier Mapping Add Edit
        public int ParentCourierMappingAddEdit(ParentCourierMappingModel model)
        {
            return ParentCourierMappingAddEditPvt(model);
        }
        private int ParentCourierMappingAddEditPvt(ParentCourierMappingModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var response = _contextManager.usp_CourierParentMappingAdd(model.BranchId, model.CPid, model.CourierId, model.AddedBy, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(response.RetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ParentTransporterMappingAddEditPvt", "Parent Transporter Mapping Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;

        }
        #endregion

        #region  Get Vendor List By Company
        public List<vendordtlsMapping> GetVendorListByCompany(int CompanyId, string Status, int BranchId)
        {
            return GetVendorListByCompanyPvt(CompanyId, Status, BranchId);
        }
        private List<vendordtlsMapping> GetVendorListByCompanyPvt(int CompanyId, string Status, int BranchId)
        {
            List<vendordtlsMapping> model = new List<vendordtlsMapping>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_VendorMasterListByCompany(CompanyId, Status, BranchId).Select(v => new vendordtlsMapping
                    {
                        VendorId = Convert.ToInt32(v.VendorId),
                        VendorName = Convert.ToString(v.VendorName),
                        Email = Convert.ToString(v.Email),
                        ContactNumber = Convert.ToString(v.ContactNumber),
                        Checked = Convert.ToInt32(v.Checked)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompanyId, "GetCompanyListByBranchPvt", "Get Company List By Branch Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Edit Company Vendor Mapping
        public int AddEditCompanyVendorMapping(CompVendorAddEditMapping model)
        {
            return AddEditCompanyVendorMappingPvt(model);
        }
        private int AddEditCompanyVendorMappingPvt(CompVendorAddEditMapping model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var response = _contextManager.usp_CompanyVendorRelationAddEdit(model.VendorIdStr, model.CompanyId, model.AddedBy, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(response.RetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCompanyVendorMapping", "Add Edit Company Vendor Mapping", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region  Get Vendor List By Branch
        public List<vendordBranchMapping> GetVendorListByBranch(int CompanyId, string Status)
        {
            return GetVendorListByBranchPvt(CompanyId, Status);
        }
        private List<vendordBranchMapping> GetVendorListByBranchPvt(int CompanyId, string Status)
        {
            List<vendordBranchMapping> model = new List<vendordBranchMapping>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_VendorMasterListByBranch(CompanyId, Status).Select(v => new vendordBranchMapping
                    {
                        VendorId = Convert.ToInt32(v.VendorId),
                        VendorName = Convert.ToString(v.VendorName),
                        Email = Convert.ToString(v.Email),
                        ContactNumber = Convert.ToString(v.ContactNumber),
                        Checked = Convert.ToInt32(v.Checked)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompanyId, "GetCompanyListByBranchPvt", "Get Company List By Branch Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Edit Branch Vendor Mapping
        public int AddEditBranchVendorMapping(VendorBranchAddEditMapping model)
        {
            return AddEditBranchVendorMappingPvt(model);
        }
        private int AddEditBranchVendorMappingPvt(VendorBranchAddEditMapping model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var response = _contextManager.usp_BranchVendorRelationAddEdit(model.VendorIdStr, model.BranchId, model.AddedBy, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(response.RetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditBranchVendorMappingPvt", "Add Edit Branch Vendor Mapping", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Expiry Stockist Notification Dashboard
        public List<ExpiryStockistNotidashModel> ExpiryStockistNotificationDashboard(int BranchId, int CompId, string Flag)
        {
            return ExpiryStockistNotificationDashboardPvt(BranchId, CompId, Flag);
        }
        private List<ExpiryStockistNotidashModel> ExpiryStockistNotificationDashboardPvt(int BranchId, int CompId, string Flag)
        {
            List<ExpiryStockistNotidashModel> ExpListNoti = new List<ExpiryStockistNotidashModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    ExpListNoti = _contextManager.usp_ExpiryStockistNotificationDashboard(BranchId, CompId, Flag).Select(v => new ExpiryStockistNotidashModel
                    {
                        DLFoodNotiCnt = Convert.ToInt32(v.DLFoodNotiCnt),
                        BranchId = v.BranchId,
                        CompId = v.CompId
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompId, "ExpiryStockistNotificationDashboardPvt", "Expiry Stockist Notification Dashboard Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ExpListNoti;
        }

        #endregion

        #region Expiry List For Notification Dashboard
        public List<ExpiryListForNotiListModel> ExpiryListForNotificationList(int BranchId, int CompId)
        {
            return ExpiryListForNotificationListPvt(BranchId, CompId);
        }
        private List<ExpiryListForNotiListModel> ExpiryListForNotificationListPvt(int BranchId, int CompId)
        {
            List<ExpiryListForNotiListModel> ExpListlst = new List<ExpiryListForNotiListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    ExpListlst = _contextManager.usp_ExpiryStockistNotiList(BranchId, CompId).Select(v => new ExpiryListForNotiListModel
                    {
                        StockistId = v.StockistId,
                        StockistNo = v.StockistNo,
                        StockistName = v.StockistName,
                        DLNo = v.DLNo,
                        DLExpDate = Convert.ToDateTime(v.DLExpDate).ToString("yyyy-MM-dd"),
                        FoodLicNo = v.FoodLicNo,
                        FoodLicExpDate = Convert.ToDateTime(v.FoodLicExpDate).ToString("yyyy-MM-dd"),
                        Addedby = v.Addedby,
                        LastUpdatedOn = Convert.ToString(v.LastUpdatedOn),
                        DLExpDateCount = v.DLExpDateCount,
                        FoodLicExpDateCount = v.FoodLicExpDateCount
                    }).OrderByDescending(x => x.DLExpDateCount == 1 || x.FoodLicExpDateCount == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompId, "ExpiryListForNotificationListPvt", "Expiry List For Notification List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ExpListlst;
        }


        #endregion

        #region Get GST Type List
        public GSTTypeModel GetGSTTypeList(int TaxId, int BranchId)
        {
            return GetGSTTypeListPvt(TaxId , BranchId);
        }
        private GSTTypeModel GetGSTTypeListPvt(int TaxId,int BranchId)
        {
            GSTTypeModel GSTTypeList = new GSTTypeModel();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    GSTTypeList.GSTType = ContextManager.usp_GetGSTTypeList(TaxId,BranchId).Select(x => new GSTTypeListModel()
                    {
                        BranchId = Convert.ToInt32(x.BranchId),
                        TaxId = Convert.ToInt32(x.TaxId),
                        GSTType = x.GSTType,
                        CGST = Convert.ToInt32(x.CGST),
                        SGST = Convert.ToInt32(x.SGST),
                        AddedBy = x.AddedBy
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGSTTypeListPvt", "Get GST Type List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GSTTypeList;
        }
        #endregion

        #region Company Details
        public List<CompanyListByBRModel> GetCompanyListByBRIdForEMP(int BranchId, string Status)
        {
            return GetCompanyListByBRIdForEMPPvt(BranchId, Status);
        }
        private List<CompanyListByBRModel> GetCompanyListByBRIdForEMPPvt(int BranchId, string Status)
        {
            List<CompanyListByBRModel> modelList = new List<CompanyListByBRModel>();
            try
            {
                modelList = _contextManager.usp_GetCompanyListByBRId(BranchId, Status).Select(c => new CompanyListByBRModel
                {
                    CompanyId = c.CompanyId,
                    CompanyCode = c.CompanyCode,
                    CompanyName = c.CompanyName
                }).OrderByDescending(x => x.CompanyId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyListByBRIdForEMPPvt", "Get Company List By BR Id For EMP Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region SLA Master List For Instate Amt and Out OutSate Amt For Mobile
        public ThresholdValueDtls SLAMasterlist(int BranchId, int CompId)
        {
            return SLAMasterlistPvt(BranchId, CompId);
        }
        private ThresholdValueDtls SLAMasterlistPvt(int BranchId, int CompId)
        {
            ThresholdValueDtls model = new ThresholdValueDtls();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {

                    model = _contextManager.usp_GetThresholdSLAMasterList(BranchId, CompId).Select(a => new ThresholdValueDtls
                    {
                        PkId = a.PkId,
                        BranchId = Convert.ToInt32(a.BranchId),
                        BranchName = a.BranchName,
                        CompanyId = Convert.ToInt32(a.CompanyId),
                        CompanyName = a.CompanyName,
                        ThresholdValue = Convert.ToInt32(a.ThresholdValue),
                        RaiseClaimDay = Convert.ToInt32(a.RaiseClaimDay),
                        ClaimSettlementDay = Convert.ToInt32(a.ClaimSettlementDay),
                        InStateAmt = Convert.ToInt64(a.InStateAmt),
                        OutStateAmt = Convert.ToInt64(a.OutStateAmt),
                        SaleSettlePeriod = Convert.ToInt32(a.SaleSettlePeriod),
                        NonSaleSettlePeriod = Convert.ToInt32(a.NonSaleSettlePeriod),
                        Addedby = a.Addedby,
                        AddedOn = Convert.ToDateTime(a.AddedOn).ToString(BusinessCont.DateFormat),
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SLAMasterlistPvt", "SLA Master list Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Company Details For Login
        public List<CompanyDtls> GetCompanyListForLogin(string Status)
        {
            return GetCompanyListForLoginPvt(Status);
        }
        private List<CompanyDtls> GetCompanyListForLoginPvt(string Status)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _contextManager.usp_CompanyMasterList(Status).Select(c => new CompanyDtls
                {
                    CompanyId = c.CompanyId,
                    CompanyCode = c.CompanyCode,
                    CompanyName = c.CompanyName,
                    CompanyEmail = c.CompanyEmail,
                    ContactNo = c.ContactNo,
                    CompanyAddress = c.CompanyAddress,
                    CompanyCity = c.CompanyCity,
                    CityName = c.CityName,
                    Pin = c.Pin,
                    CompanyPAN = c.CompanyPAN,
                    GSTNo = c.GSTNo,
                    IsPicklistAvailable = c.IsPicklistAvailable,
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn)
                }).OrderBy(x => x.CompanyName).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CompanyDtlsForLoginPvt", "Company Dtls For Login Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Transport List with Stockist For Mob
        public List<TranLstwithStkModel> TransportListwithStockies(int BranchId, int CompanyId, int StockiesId, string Status)
        {
            return TransportListwithStockiesPvt(BranchId, CompanyId, StockiesId, Status);
        }
        private List<TranLstwithStkModel> TransportListwithStockiesPvt(int BranchId, int CompanyId, int StockiesId, string Status)
        {
            List<TranLstwithStkModel> StockMapLst = new List<TranLstwithStkModel>();
            try
            {
                StockMapLst = _contextManager.usp_TransportListwithStockiesForMob(BranchId, CompanyId, StockiesId, Status).Select(c => new TranLstwithStkModel
                {
                    BranchId = Convert.ToInt32(c.BranchId),
                    CompanyId = c.CompanyId,
                    TransporterId = c.TransporterId,
                    TransporterNo = c.TransporterNo,
                    TransporterName = c.TransporterName,
                    StockistId = c.StockistId,
                    isActive = c.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransportListwithStockiesPvt", "Transport List with Stockies Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockMapLst;
        }
        #endregion

        #region Get Owner Dashboard Count List
        public OwnerLoginDashCnt GetOwnerDashboardCountList(int BranchId, int CompanyId)
        {
            return GetOwnerDashboardCountListPvt(BranchId, CompanyId);
        }
        private OwnerLoginDashCnt GetOwnerDashboardCountListPvt(int BranchId, int CompanyId)
        {
            OwnerLoginDashCnt cntlist = new OwnerLoginDashCnt();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    cntlist = _contextManager.usp_OwnerDashbordAllCntNew(BranchId, CompanyId).Select(c => new OwnerLoginDashCnt
                    {
                        OPrioPending = Convert.ToInt32(c.PrioPending),
                        OStkrPending = Convert.ToInt32(c.StkrPending),
                        OStkrPendingAmt = Convert.ToDecimal(c.StkrPendingAmt),
                        OGPPending = Convert.ToInt32(c.GPPending),
                        OGPPendingAmt = Convert.ToDecimal(c.GPPendingAmt),
                        OTPBox = Convert.ToInt32(c.TPBox),
                        OTotalChqBounced = Convert.ToInt32(c.TotalBounce),
                        ODueforFirstNotice = Convert.ToInt32(c.DueforFirstNotice),
                        ODueforLegalNotice = Convert.ToInt32(c.DueforLegalNotice),
                        OOverDueStk = Convert.ToInt32(c.OverDueStk),
                        OOverDueAmt = Convert.ToString(c.OverDueAmt),
                        OPendSANCnt = Convert.ToInt32(c.PendSANCnt),
                        OPendClaimCnt = Convert.ToInt32(c.PendClaimCnt),
                        OConsignPending = Convert.ToInt32(c.ConsignPending),
                        OSalebleCN2_7 = Convert.ToInt32(c.SalelablePen2),
                        OMore11Days = Convert.ToInt32(c.NonSalelablePen45),
                        OStkStickerPending = Convert.ToInt32(c.StkStickerPending),
                        OStkGPPendingAmt = Convert.ToDecimal(c.StkGPPendingAmt),
                        OStkSticerPendingAmt = Convert.ToDecimal(c.StkSticerPendingAmt),
                        OStkGPPending = Convert.ToInt32(c.StkGPPending),
                        ONoOfBoxes = Convert.ToInt32(c.NoOfBoxes)
                      

                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerDashboardCountListPvt", "Get Owner Dashboard Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return cntlist;
        }
        #endregion

        #region Get Stockist List for Verify Data
        public List<StockistModel> GetStockistListByBranchCompany(int BranchId, int CompanyId, string Status)
        {
            return GetStockistListByBranchCompanyPvt(BranchId, CompanyId, Status);
        }
        private List<StockistModel> GetStockistListByBranchCompanyPvt(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> model = new List<StockistModel>();
            try
            {
                model = _contextManager.usp_StockisDataForVerifyList(BranchId, CompanyId, Status).Select(c => new StockistModel
                {
                    CompanyId = Convert.ToInt32(c.CompId),
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    BranchId = Convert.ToInt32(c.BranchId),
                    StockistPAN = c.StockistPAN,
                    MobNo = c.MobNo,
                    MappedWithBR = c.MappedWithBR,
                    MappedWithCMp = c.MappedWithCMp,
                    BRCRNotMap = c.BRCRNotMap
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, CompanyId, "GetStockistListByBranchCompanyPvt", "Get Stockist List By Branch Company Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Stockist List for Verify Data
        public List<StockistModel> GetStockistListforVerifyDataList(int BranchId, int CompanyId, string Status)
        {
            return GetStockistListforVerifyDataListPvt(BranchId, CompanyId, Status);
        }
        private List<StockistModel> GetStockistListforVerifyDataListPvt(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            try
            {
                StockLst = _contextManager.usp_StockistMasterList(BranchId, CompanyId, Status).Select(c => new StockistModel
                {
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    StockistPAN = c.StockistPAN,
                    Emailid = c.Emailid,
                    MobNo = c.MobNo,
                    StockistAddress = c.StockistAddress,
                    CityCode = c.CityCode,
                    CityName = c.CityName,
                    GSTNo = c.GSTNo,
                    //BankId = Convert.ToInt16(c.BankId),
                    //IFSCCode = c.IFSCCode,
                    //BankAccountNo = c.BankAccountNo,
                    LocationId = c.LocationId,
                    MasterName = c.LocationName,
                    Pincode = c.Pincode,
                    DLNo = c.DLNo,
                    DLExpDate = DateTime.Parse(c.DLExpDate.ToString()),
                    FoodLicNo = c.FoodLicNo,
                    FoodLicExpDate = DateTime.Parse(c.FoodLicExpDate.ToString()),
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                    DLExpDateCount = c.DLExpDateCount,
                    FoodLicExpDateCount = c.FoodLicExpDateCount
                }).OrderByDescending(x => Convert.ToInt64(x.StockistId)).OrderByDescending(x => x.DLExpDateCount == 1 || x.FoodLicExpDateCount == 1).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockiestPvt", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Import Type List
        public List<ImportTypeList> GetImportTypeList()
        {
            return GetImportTypeListPvt();
        }
        private List<ImportTypeList> GetImportTypeListPvt()
        {
            List<ImportTypeList> modelList = new List<ImportTypeList>();
            try
            {
                modelList = _contextManager.usp_ImportTypeList().Select(b => new ImportTypeList
                {
                    ImportId = b.ImportId,
                    ImportType = b.ImportType
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportTypeListPvt", "Get Import Type List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Import File and Column Relation List
        public List<ImportFileColumnRelList> GetImportFileandColumnRelList(int BranchId, int CompId, int ImportId)
        {
            return GetImportFileandColumnRelListPvt(BranchId, CompId, ImportId);
        }
        private List<ImportFileColumnRelList> GetImportFileandColumnRelListPvt(int BranchId, int CompId, int ImportId)
        {
            List<ImportFileColumnRelList> model = new List<ImportFileColumnRelList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_ImportFileandColumnRelList(BranchId, CompId, ImportId).Select(c => new ImportFileColumnRelList
                    {
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        FieldName = c.FieldName,
                        ColumnDatatype = c.ColumnDatatype,
                        ImportId = Convert.ToInt32(c.ImportId),
                        ImpFor = c.ImpFor,
                        ImpId = c.ImpId,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetImportFileandColumnRelListPvt", "Get Import File and Column Rel List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Import Col Field List Relation List
        public List<ImportFileColumnRelList> OnChangeColFieldList(int BranchId, int CompId, int ImpId)
        {
            return OnChangeColFieldListPvt(BranchId, CompId, ImpId);
        }
        private List<ImportFileColumnRelList> OnChangeColFieldListPvt(int BranchId, int CompId, int ImpId)
        {
            List<ImportFileColumnRelList> model = new List<ImportFileColumnRelList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_ImpOnchangeField(BranchId, CompId, ImpId).Select(c => new ImportFileColumnRelList
                    {
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        FieldName = c.FieldName,
                        ColumnDatatype = c.ColumnDatatype,
                        ImportId = Convert.ToInt32(c.ImportId),
                        ImpFor = c.ImpFor,
                        ImpId = c.ImpId,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "OnChangeColFieldList", "On Change Col Field List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add/Edit Import Dynamic Fields Masters
        public int ImportDymAddEdit(ImportDymAddEditModel model)
        {
            return ImportDymAddEditPvt(model);
        }
        private int ImportDymAddEditPvt(ImportDymAddEditModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_ImportDymAddEdit(model.pkId, model.BranchId, model.CompId,
                model.ImpFor, model.FieldName, model.ExcelColName, model.ColumnDatatype, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ImportDymAddEditPvt", "Import Dynamic Add Edit Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region  Get Import Data Dynamically Master List
        public List<ImportDynaListModel> GetImportDyanamically(int BranchId, int CompId)
        {
            return GetImportDyanamicallyPvt(BranchId, CompId);
        }
        private List<ImportDynaListModel> GetImportDyanamicallyPvt(int BranchId, int CompId)
        {
            List<ImportDynaListModel> ImporDynaLst = new List<ImportDynaListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    ImporDynaLst = _contextManager.usp_GetImportDynamicallyList(BranchId, CompId)
                        .Select(c => new ImportDynaListModel
                        {
                            pkId = Convert.ToInt32(c.pkId),
                            BranchId = c.BranchId,
                            CompId = c.CompId,
                            ImpFor = c.ImpFor,
                            FieldName = c.FieldName,
                            ExcelColName = c.ExcelColName,
                            ColumnDatatype = c.ColumnDatatype,
                            UpdatedBy = Convert.ToInt32(c.UpdatedBy),
                            UpdatedOn = Convert.ToDateTime(c.UpdatedOn),
                            BranchName = c.BranchName,
                            CompanyName = c.CompanyName
                        }).OrderByDescending(c => c.pkId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportDyanamicallyPvt", "Get Import Dyanamically Pvt" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImporDynaLst;
        }
        #endregion

        #region  Get Branch Comp List For Mob
        public List<BranchDtlsList> GetBranchCompListForMob(string Status)
        {
            return GetBranchCompListForMobPvt(Status);
        }
        private List<BranchDtlsList> GetBranchCompListForMobPvt(string Status)
        {
            List<BranchDtlsList> branchLst = new List<BranchDtlsList>();
            List<CompDtlsList> complst = new List<CompDtlsList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    branchLst = _contextManager.usp_GetBranchListForMob(Status).Select(b => new BranchDtlsList
                    {
                        BranchId = b.BranchId,
                        BranchCode = b.BranchCode,
                        BranchName = b.BranchName,
                        IsActive = b.IsActive
                    }).ToList();

                    for (int i = 0; i < branchLst.Count(); i++)
                    {
                        complst = _contextManager.usp_GetCompListByIdForMob(branchLst[i].BranchId, Status)
                            .Select(c => new CompDtlsList
                            {
                                CompanyId = c.CompanyId,
                                CompanyCode = c.CompanyCode,
                                CompanyName = c.CompanyName,
                                IsActive = c.IsActive
                            }).ToList();
                        branchLst[i].compList = complst;
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchCompListForMobPvt", "Get Branch Comp List For Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return branchLst;
        }
        #endregion

        #region Get Expense RegisterTds
        public List<VenderIsTds> GetExpenseRegisterTds(int Branch, int VendorId)
        {
            return GetExpenseRegisterTdsPvt(Branch, VendorId);
        }

        private List<VenderIsTds> GetExpenseRegisterTdsPvt(int Branch, int VendorId)
        {
            List<VenderIsTds> model = new List<VenderIsTds>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    model = contextManager.usp_GetExpenseRegisterTds(Branch, VendorId).Select(v => new VenderIsTds
                    {
                        BranchId = Convert.ToInt32(v.Branch),
                        VendorId = Convert.ToInt32(v.VendorId),
                        VendorName = v.VendorName,
                        IsGST = v.IsGST,
                        GSTNumber = v.GSTNumber,
                        IsActive = v.IsActive,
                        IsTDS = v.IsTDS,
                        TDSPer = Convert.ToInt32(v.TDSPer),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseRegisterTdsPvt", "Get Vendor List - BranchId:  " + Branch, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Company vendor Dropdown List
        public List<CompanyDtls> GetCompanyLstForAccount(string Status, int BranchId)
        {
            return GetCompanyLstForAccountPvt(Status, BranchId);
        }
        private List<CompanyDtls> GetCompanyLstForAccountPvt(string Status, int BranchId)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _contextManager.usp_GetCompLstForAccByBranch(Status, BranchId).Select(c => new CompanyDtls
                {
                    CompanyId = c.CompanyId,
                    CompanyCode = c.CompanyCode,
                    CompanyName = c.CompanyName,
                    CompanyEmail = c.CompanyEmail,
                    ContactNo = c.ContactNo,
                    CompanyAddress = c.CompanyAddress,
                    CompanyCity = c.CompanyCity,
                    CityName = c.CityName,
                    Pin = c.Pin,
                    CompanyPAN = c.CompanyPAN,
                    GSTNo = c.GSTNo,
                    IsPicklistAvailable = c.IsPicklistAvailable,
                    IsActive = c.IsActive,
                    Addedby = c.Addedby,
                    AddedOn = Convert.ToDateTime(c.AddedOn)
                }).OrderByDescending(x => x.CompanyId).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Get Company Lst For Account Pvt", "Get Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Expense RegisterTds
        public List<TransporterIsTds> GetExpenseTransporterTds(int BranchId, int TransporterId)
        {
            return GetExpenseTransporterTdsPvt(BranchId, TransporterId);
        }

        private List<TransporterIsTds> GetExpenseTransporterTdsPvt(int BranchId, int TransporterId)
        {
            List<TransporterIsTds> modellist = new List<TransporterIsTds>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modellist = contextManager.usp_GetExpenseTransporterTds(BranchId, TransporterId).Select(v => new TransporterIsTds
                    {
                        BranchId = Convert.ToInt32(v.BranchId),
                        TransporterId = Convert.ToInt32(v.Tpid),
                        ParentTranspName = v.ParentTranspName,
                        IsGST = v.IsGST,
                        GSTNumber = v.GSTNumber,
                        IsActive = v.IsActive,
                        IsTDS = v.IsTDS,
                        TDSPer = Convert.ToInt32(v.TDSPer),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseTransporterTdsPvt", "Get Transporter List - BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modellist;
        }
        #endregion

        #region Get Expense Courier Tds
        public List<CourierIsTds> GetExpenseCourierTds(int BranchId, int CourierId)
        {
            return GetExpenseCourierTdsPvt(BranchId, CourierId);
        }

        private List<CourierIsTds> GetExpenseCourierTdsPvt(int BranchId, int CourierId)
        {
            List<CourierIsTds> modellist = new List<CourierIsTds>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modellist = contextManager.usp_GetExpenseCourierTds(BranchId, CourierId).Select(v => new CourierIsTds
                    {
                        BranchId = Convert.ToInt32(v.BranchId),
                        CourierId = Convert.ToInt32(v.Cpid),
                        CourierName = v.ParentCourierName,
                        IsGST = v.IsGST,
                        GSTNumber = v.GSTNumber,
                        IsActive = v.IsActive,
                        IsTDS = v.IsTDS,
                        TDSPer = Convert.ToInt32(v.TDSPer),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseCourierTdsPvt", "Get Transporter List - BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modellist;
        }
        #endregion

        #region  BraGetnch Details By BranchId By Hrishikesh
        public List<BranchDetails> GetBranchDetailsById(int BranchId)
        {
            return GetBranchDetailsByIdPvt(BranchId);
        }

        private List<BranchDetails> GetBranchDetailsByIdPvt(int BranchId)
        {
            List<BranchDetails> list = new List<BranchDetails>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    list = _contextManager.usp_GetByIdBranch(BranchId).Select(x => new BranchDetails
                    {
                        BranchId = x.BranchId,
                        BranchCode = x.BranchCode,
                        BranchName = x.BranchName,
                        BranchAddress = x.BranchAddress,
                        IsActive = x.IsActive
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetBranchDetailsByIdPvt", "Get Branch Details By BranchId Hrishi", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(e.InnerException));
            }
            return list;
        }
        #endregion

        #region Get Branch Company Relation List By Hrishi
        public List<CompanyRelationHrishi> GetBranchWiseCompany(int BranchId)
        {
            return GetBranchWiseCompanyPvt(BranchId);
        }
        private List<CompanyRelationHrishi> GetBranchWiseCompanyPvt(int BranchId)
        {
            List<CompanyRelationHrishi> model = new List<CompanyRelationHrishi>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_BranchCompanyRelation(BranchId).Select(c => new CompanyRelationHrishi
                    {
                        CompanyId = Convert.ToInt32(c.CompanyId),
                        CompanyName = Convert.ToString(c.CompanyName),
                        BranchId = Convert.ToInt32(c.BranchId),
                        BranchName = Convert.ToString(c.BranchName),
                        
                      
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetComapnyBranchRelationListPvt", "Get Comapny Branch Relation List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Printer Date List created by Pratyush
        public List<PrinterDataModal> GetPrinterDataList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string flag)
        {
            return GetPrinterDataList_pvt(BranchId, CompanyId, fromDate, toDate, flag);
        }
        private List<PrinterDataModal> GetPrinterDataList_pvt(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string flag)
        {
            List<PrinterDataModal> list = new List<PrinterDataModal>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    list = _contextManager.usp_GetPrinterDetails(BranchId, CompanyId, fromDate, toDate, flag).Select(x => new PrinterDataModal
                    {
                        pkId = Convert.ToInt32(x.pkId),
                        BranchName = Convert.ToString(x.BranchName),
                        CompanyName = Convert.ToString(x.CompanyName),
                        InvId = Convert.ToInt64(x.InvId),
                        InvCreatedDate = Convert.ToDateTime(x.InvCreatedDate),
                        InvNo = Convert.ToString(x.InvNo),
                        Status = Convert.ToString(x.Status),
                        StokistNo = Convert.ToString(x.StockistNo),
                        StokistName = Convert.ToString(x.StockistName),
                        NoOfBox = Convert.ToInt32(x.NoOfBox),
                        IsStockTransfer =Convert.ToInt32(x.IsStockTransfer)

                    }).OrderByDescending(x => x.pkId).ToList();
                };
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterDataList_pvt", "Get Printer Data List  " + "CompanyId:  " + CompanyId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return list;
        }
        #endregion

        #region Update Printer Details Hrishi
        public string UpdatePrinterDetails(PrinterValueModal model)
        {
            return UpdatePrinterDetailsPvt(model);
        }
        private string UpdatePrinterDetailsPvt(PrinterValueModal model)
        {
            int ReturnValue = 0;
            ObjectParameter obj = new ObjectParameter("ReturnVal", typeof(int));

            try
            {
                ReturnValue = _contextManager.usp_UpdatePrinterDetails(model.InvId, model.BranchId, model.CompanyId, model.Flag, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.BranchId, "UpdatePrinterDetailsPvt", " Printer PDF Details Update", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (ReturnValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Delete Version Details (Mobile & Web Application)
        public int DeleteVersionDetails(int VersionId)
        {
            return DeleteVersionDetailsPvt(VersionId);
        }
        private int DeleteVersionDetailsPvt(int VersionId)
        {
            int result = 0;
            ObjectParameter RetVal = new ObjectParameter("ReturnVal", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    result = contextManager.usp_DeleteVersionDetails(VersionId, RetVal);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteVersionDetailsPvt", "Delete Version Details (Mobile & Web Application) - BranchId:  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

    }
}