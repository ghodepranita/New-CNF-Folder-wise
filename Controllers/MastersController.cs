using CNF.Business.BusinessConstant;
using System;
using System.Web.Http;
using CNF.Business.Model.Master;
using System.Collections.Generic;
using CNF.Business.Model.Context;

namespace CNF.API.Controllers
{
    public class MastersController : BaseApiController
    {
        # region Get General Master List
        [HttpGet]
        [Route("Masters/GetGeneralMasterList/{CategoryName}/{Status}")]
        public GeneralMasterList GetGeneralMasterList(string CategoryName, string Status)
        {
            GeneralMasterList generalmasterlist = new GeneralMasterList();
            try
            {
                {
                    generalmasterlist = _unitOfWork.MastersRepository.GetGeneralMaster(CategoryName, Status);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGeneralMasterList", "Get General Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return generalmasterlist;
        }
        #endregion

        # region AddEdit Branch Master 
        [HttpPost]
        [Route("Masters/BranchMasterAddEdit")]
        public string BranchMasterAddEdit([FromBody] BranchList model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.BranchMasterAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "BranchMasterAddEdit", "AddEdit Branch Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get State List
        [HttpGet]
        [Route("Masters/GetStateList/{Flag}")]
        public GetStateList GetStateList(string Flag)
        {
            GetStateList getstatelist = new GetStateList();
            try
            {
                {
                    getstatelist = _unitOfWork.MastersRepository.GetStateList(Flag);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStateList", "Get State List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return getstatelist;
        }
        #endregion

        #region Get City List
        [HttpGet]
        [Route("Masters/GetCityList/{StateCode}/{districtCode}/{Flag}")]
        public GetCityList GetCityList(string StateCode, string districtCode, string Flag)
        {
            GetCityList getCitylist = new GetCityList();
            try
            {
                getCitylist = _unitOfWork.MastersRepository.GetCityList(StateCode, districtCode, Flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCityList", "Get City List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return getCitylist;
        }
        #endregion

        #region Get Branch List
        [HttpGet]
        [Route("Masters/GetBranchList/{Status}")]
        public List<BranchList> GetBranchList(string Status)
        {
            List<BranchList> branchList = new List<BranchList>();
            try
            {
                branchList = _unitOfWork.MastersRepository.GetBranchList(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchList", " Get Branch List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return branchList;
        }
        #endregion

        #region Company Detalis
        [HttpGet]
        [Route("Masters/GetCompanyList/{Status}")]
        public List<CompanyDtls> GetCompanyList(string Status)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _unitOfWork.MastersRepository.CompanyDtls(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyList", "Get Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }

        [HttpPost]
        [Route("Masters/CompanyDtlsAddEdit")]
        public string CompanyDtlsAddEdit([FromBody] CompanyDtls model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.CompanyDtlsAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.CompanyId, "CompanyDtlsAddEdit", "Company Details AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Update user Activation
        [HttpPost]
        [Route("Masters/EmployeeMasterActivate")]
        public string EmployeeMasterActivate([FromBody] EmployeeActiveModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.EmployeeMasterActivate(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.EmpId != 0 ? model.EmpId : 0), "EmployeeMasterActivate", "Employee Activate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Add Employee Details
        [HttpPost]
        [Route("Masters/AddEmployeeDtls")]
        public int AddEmployeeDtls([FromBody] AddEmployeeModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.MastersRepository.AddEmployeeDtls(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.EmpId != 0 ? model.EmpId : 0), "AddEmployeeDtls", "Employee Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Edit Employee Detalis
        [HttpPost]
        [Route("Masters/EditEmployeeDtls")]
        public int EditEmployeeDtls([FromBody] AddEmployeeModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.MastersRepository.EditEmployeeDtls(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.EmpId != 0 ? model.EmpId : 0), "EditEmployeeDtls", "Add Employee", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Employee Details
        [HttpGet]
        [Route("Masters/GetEmpCmpDtls/{EmpId}")]
        public List<EmployeeDtls> GetEmpCmpDtls(int EmpId)
        {
            List<EmployeeDtls> Emplist = new List<EmployeeDtls>();
            try
            {
                Emplist = _unitOfWork.MastersRepository.GetEmployeeDtls(EmpId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, EmpId, "GetEmpCmpDtls", "Get Employee Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Emplist;
        }
        #endregion

        # region Get Category List
        [HttpGet]
        [Route("Masters/GetCategoryList")]
        public GetCategoryList GetCategoryList()
        {
            GetCategoryList CategoryList = new GetCategoryList();
            try
            {
                {
                    CategoryList = _unitOfWork.MastersRepository.GetCategoryList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGeneralMasterList", "Get General Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return CategoryList;
        }
        #endregion

        # region Add Edit Division Master
        [HttpPost]
        [Route("Masters/AddEditDivisionMaster")]
        public string AddEditDivisionMaster([FromBody] DivisionMasterLst model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditDivisionMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.DivisionId, "AddEditDivisionMaster", "Add Edit Division Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Division Master List
        [HttpGet]
        [Route("Masters/GetDivisionMasterList/{Status}")]
        public List<DivisionMasterLst> GetDivisionMasterList(string Status)
        {
            List<DivisionMasterLst> DivisionMasterList = new List<DivisionMasterLst>();
            try
            {
                DivisionMasterList = _unitOfWork.MastersRepository.GetDivisionMasterList(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchList", "Get Division Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return DivisionMasterList;

        }
        #endregion

        # region Add Edit General Master
        [HttpPost]
        [Route("Masters/AddEditGeneralMaster")]
        public string AddEditGeneralMaster([FromBody] GeneralMasterLst model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditGeneralMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditGeneralMaster", "Add Edit General Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        # region Add Edit Transporter Master
        [HttpPost]
        [Route("Masters/AddEditTransporterMaster")]
        public string AddEditTransporterMaster([FromBody] TransporterMasterLst model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditTransporterMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditTransporterMaster", "Add Edit Transporter Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Transporter Master List
        [HttpGet]
        [Route("Masters/GetTransporterMasterList/{DistrictCode}/{Status}/{BranchId}")]
        public List<TransporterMasterLst> GetTransporterMasterList(string DistrictCode, string Status, int BranchId)
        {
            List<TransporterMasterLst> GeneralMasterList = new List<TransporterMasterLst>();
            try
            {
                GeneralMasterList = _unitOfWork.MastersRepository.GetTransporterMasterList(DistrictCode, Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterMasterList", "Get Transporter Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GeneralMasterList;

        }
        #endregion

        #region Get Transporter Master List For Branch
        [HttpGet]
        [Route("Masters/GetTransporterMasterListForBranch/{DistrictCode}/{Status}/{BranchId}")]
        public List<TransporterMasterLst> GetTransporterMasterListForBranch(string DistrictCode, string Status, int BranchId)
        {
            List<TransporterMasterLst> GeneralMasterList = new List<TransporterMasterLst>();
            try
            {
                GeneralMasterList = _unitOfWork.MastersRepository.GetTransporterMasterListForBranch(DistrictCode, Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterMasterListForBranch", "Get Transporter Master List For Branch", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GeneralMasterList;

        }
        #endregion

        #region Get Role List For User
        [HttpGet]
        [Route("Masters/GetRoleList")]
        public List<RoleModel> GetRoleList()
        {
            List<RoleModel> RoleLst = new List<RoleModel>();
            try
            {
                RoleLst = _unitOfWork.MastersRepository.GetRoleLstForUser();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleList", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Get Stockist List
        [HttpGet]
        [Route("Masters/GetStockistList/{BranchId}/{CompanyId}/{Status}")]
        public List<StockistModel> GetStockistList(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistLst(BranchId, CompanyId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistList", "Get Stockist List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Stockist Master AddEdit
        [HttpPost]
        [Route("Masters/StockistDtlsAddEdit")]
        public string StockistDtlsAddEdit([FromBody] StockistModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.StockistDtlsAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.CompanyId != 0 ? model.CompanyId : 0), "StockistDtlsAddEdit", "Stockis Details AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Stockist Details by Id
        [HttpGet]
        [Route("Masters/GetStockistBankList/{StockistId}")]
        public List<BankModel> StockistDtlsbyId(int StockistId)
        {
            List<BankModel> BankModel = new List<BankModel>();
            try
            {
                BankModel = _unitOfWork.MastersRepository.GetStockistBankList(StockistId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, StockistId, "GetStockistBankList", "Get Stockist Bank List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return BankModel;
        }
        #endregion

        #region Get Stokist Transport Mapping List
        [HttpGet]
        [Route("Masters/GetStokistTransportMappingList/{BranchId}/{CompanyId}")]
        public List<StokistTransportModel> GetStokistTransportMappingList(int BranchId, int CompanyId)
        {
            List<StokistTransportModel> StockMapLst = new List<StokistTransportModel>();
            try
            {
                StockMapLst = _unitOfWork.MastersRepository.GetStokistTransportMappingList(BranchId, CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStokistTransportMappingList", "Get Stokist Transport Mapping List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockMapLst;
        }
        #endregion

        #region Stokist Transport Mapping AddEdit
        [HttpPost]
        [Route("Masters/StokistTransportMappingAddEdit")]
        public string StokistTransportMappingAddEdit([FromBody] StokistTransportModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.StokistTransportMappingAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.CompanyId != 0 ? model.CompanyId : 0), "StokistTransportMappingAddEdit", "Stokist Transport Mapping AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get District List
        [HttpGet]
        [Route("Masters/GetDistrictList/{StateCode}/{Flag}")]
        public GetDistrictList GetDistrictList(string StateCode, string Flag)
        {
            GetDistrictList getDistrictlist = new GetDistrictList();
            try
            {
                getDistrictlist = _unitOfWork.MastersRepository.GetDistrictList(StateCode, Flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCityList", "Get City List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return getDistrictlist;
        }
        #endregion

        #region Carting Agent Details
        [HttpGet]
        [Route("Masters/GetCartingAgentLst/{Status}/{BranchId}")]
        public List<cartingAgentmodel> GetCartingAgentLst(string Status, int BranchId)
        {
            List<cartingAgentmodel> CALst = new List<cartingAgentmodel>();
            try
            {
                CALst = _unitOfWork.MastersRepository.GetCartingAgentLst(Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetCartingAgentLst", "Get carting agent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CALst;
        }

        [HttpPost]
        [Route("Masters/CartingAgentMasterAddEdit")]
        public string CartingAgentMasterAddEdit([FromBody] cartingAgentmodel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.CartingAgentMasterAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CartingAgentMasterAddEdit", "Stockis Details AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Employee Master List
        [HttpGet]
        [Route("Masters/GetEmployeeMasterList/{BranchId}/{Status}")]
        public List<EmployeeMasterList> GetEmployeeMasterList(int BranchId, string Status)
        {
            List<EmployeeMasterList> objList = new List<EmployeeMasterList>();
            try
            {
                objList = _unitOfWork.MastersRepository.GetEmployeeMasterList(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetEmployeeMasterList", "Get Employee Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return objList;

        }
        #endregion

        #region Update User Only Activation
        [HttpPost]
        [Route("Masters/UserActiveDeactive")]
        public string UserActiveDeactive([FromBody] EmployeeActiveModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.UserActiveDeactive(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, model.EmpId, "UserActiveDeactive", "Update User Activate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Add Edit Courier Master
        [HttpPost]
        [Route("Masters/AddEditCourierMaster")]
        public string AddEditCourierMaster([FromBody] CourierMasterLst model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditCourierMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCourierMaster", "Add Edit courier Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Courier Master List
        [HttpGet]
        [Route("Masters/GetCourierMasterList/{BranchId}/{DistrictCode}/{Status}")]
        public List<CourierMasterLst> GetCourierMasterList(int BranchId, string DistrictCode, string Status)
        {
            List<CourierMasterLst> GetList = new List<CourierMasterLst>();
            try
            {
                GetList = _unitOfWork.MastersRepository.GetcourierMasterList(BranchId, DistrictCode, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCourierMasterList", "Get courier Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetList;

        }
        #endregion

        #region Get User Details
        [HttpGet]
        [Route("Masters/GetUserDtls/{UserId}")]
        public UserDtls GetUserDtls(int UserId)
        {
            UserDtls UserLst = new UserDtls();
            try
            {
                UserLst = _unitOfWork.MastersRepository.GetUserDtls(UserId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetUserDtls", "Get Use Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return UserLst;
        }
        #endregion

        #region Get Transporter By Id
        [HttpGet]
        [Route("Masters/TransporterById/{TransporterId}")]
        public TransporterMasterLst GetTransporterById(int TransporterId)
        {
            TransporterMasterLst transporterMasterList = new TransporterMasterLst();
            try
            {
                transporterMasterList = _unitOfWork.MastersRepository.GetTransporterById(TransporterId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterById", "Get Transporter By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return transporterMasterList;
        }
        #endregion

        #region Get Branch Details By BranchId
        [HttpGet]
        [Route("Masters/GetBranchByIdDtls/{BranchId}")]
        public List<BranchIdDtls> GetBranchByIdDtls(int BranchId)
        {
            List<BranchIdDtls> model = new List<BranchIdDtls>();
            try
            {
                model = _unitOfWork.MastersRepository.GetBranchByIdDtls(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetBranchByIdDtls", "Get Branch Details By BranchId", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Edit Stockist Company Relation
        [HttpPost]
        [Route("Masters/AddEditStockistCompanyRelation")]
        public string AddEditStockistCompanyRelation([FromBody] StockistRelation model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditStockistCompanyRelation(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditTransporterMaster", "Add Edit Transporter Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        # region Add Edit Stockist Branch Relation
        [HttpPost]
        [Route("Masters/AddEditStockistBranchRelation")]
        public string AddEditStockistBranchRelation([FromBody] StockistRelation model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditStockistBranchRelation(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditTransporterMaster", "Add Edit Transporter Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Stockist Branch Relation List
        [HttpGet]
        [Route("Masters/GetStockistBranchRelationList/{BranchId}")]
        public List<StockistRelation> GetStockistBranchRelationList(int BranchId)
        {
            List<StockistRelation> StockLst = new List<StockistRelation>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistBranchRelationList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistList", "Get Stockist List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Stockist Company List
        [HttpGet]
        [Route("Masters/GetStockistCompanyRelationList/{CompId}")]
        public List<StockistRelation> GetStockistCompanyRelationList(int CompId)
        {
            List<StockistRelation> StockLst = new List<StockistRelation>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistCompanyRelationList(CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistList", "Get Stockist List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Check Username Available
        [HttpGet]
        [Route("Masters/GetCheckUsernameAvailable/{Username}")]
        public CheckUsernameAvailableModel GetCheckUsernameAvailable(string Username)
        {
            CheckUsernameAvailableModel model = new CheckUsernameAvailableModel();
            try
            {
                model = _unitOfWork.MastersRepository.GetCheckUsernameAvailable(Username);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckUsernameAvailable", "Get Check Username Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Stockist List By Branch 
        [HttpGet]
        [Route("Masters/GetStockistListByBranch/{BranchId}/{Status}")]
        public List<StockistModel> GetStockistListByBranch(int BranchId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistListByBranch(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistBranchRelationListByBranchId", "Get Stockist Branch RelationList By BranchId", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Stockist List By Company 
        [HttpGet]
        [Route("Masters/GetStockistListByCompany/{CompanyId}/{Status}")]
        public List<StockistModel> GetStockistListByCompany(int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistListByCompany(CompanyId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistBranchRelationListByCompanyId", "Get Stockist Company RelationList By CompanyId", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Company Detalis for Login
        [HttpGet]
        [Route("Master/GetCompanyListForLogIn/{Status}")]
        [AllowAnonymous]
        public List<CompanyDtls> GetCompanyListForLogin(string Status)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetCompanyListForLogin(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyList", "Get Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Role List for Login
        [HttpGet]
        [Route("Masters/GetRoleListForLogIn")]
        [AllowAnonymous]
        public List<RoleModel> GetRoleListForLogIn()
        {
            List<RoleModel> RoleLst = new List<RoleModel>();
            try
            {
                RoleLst = _unitOfWork.MastersRepository.GetRoleLst();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRoleList", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Get Roles Details by EmpId
        [HttpGet]
        [Route("Masters/GetRolesdls/{EmpId}")]
        public List<RolesModel> GetRolesls(int EmpId)
        {
            List<RolesModel> Rolelist = new List<RolesModel>();
            try
            {
                Rolelist = _unitOfWork.MastersRepository.GetRolesls(EmpId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, EmpId, "GetRolesdls", "Get Roles Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Rolelist;
        }
        #endregion

        #region Get Guard Details
        [HttpGet]
        [Route("Masters/GetGuardDetails/{BranchId}/{CompId}")]
        public List<GuardDetails> GetGuardDetails(int BranchId, int CompId)
        {
            List<GuardDetails> RoleLst = new List<GuardDetails>();
            try
            {
                RoleLst = _unitOfWork.MastersRepository.GetGuardDetails(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGuardDetails", "Get Guard Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RoleLst;
        }
        #endregion

        #region Create User
        [HttpPost]
        [Route("Masters/CreateUser")]
        public string CreateUser([FromBody] CreateUserModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.CreateUser(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.EmpId != 0 ? model.EmpId : 0), "CreateUser", "Create User", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region get Print List
        [HttpGet]
        [Route("Masters/GetPrintCompanyList/{Status}")]
        [AllowAnonymous]
        public List<CompanyDtls> GetPrintCompanyList(string Status)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _unitOfWork.MastersRepository.CompanyDtls(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintCompanyList", "Get Print Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Check Employee Number Available
        [HttpPost]
        [Route("Masters/GetCheckEmployeeNumberAvilable")]
        public AddEmployeeModel GetCheckEmployeeNumberAvilable(AddEmployeeModel Model)
        {
            AddEmployeeModel model = new AddEmployeeModel();

            try
            {
                model = _unitOfWork.MastersRepository.GetCheckEmployeeNumberAvilable(Model.EmpId, Model.EmpNo, Model.EmpEmail, Model.EmpMobNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckEmployeeNumberAvilable", "Get Check Employee Number Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check Carting Agent Available
        [HttpPost]
        [Route("Masters/GetCheckCartingAgentAvilable")]
        public cartingAgentmodel GetCheckCartingAgentAvilable(cartingAgentmodel Model)
        {
            cartingAgentmodel model = new cartingAgentmodel();
            try
            {
                model = _unitOfWork.MastersRepository.GetCheckCartingAgentAvilable(Model.CAName);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckCartingAgentAvilable", "Get check Carting Agent Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check Courier Name Available
        [HttpPost]
        [Route("Masters/GetCheckCourierNameAvilable")]
        public CourierMasterLst GetCheckCourierNameAvilable(CourierMasterLst Model)
        {
            CourierMasterLst model = new CourierMasterLst();
            try
            {
                model = _unitOfWork.MastersRepository.GetCheckCourierNameAvilable(Model.CourierName);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckCourierNameAvilable", "Get check Courier Name Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Check StocksitNo is Available
        [HttpGet]
        [Route("Masters/GetStockistNoAvailable/{StockistNo}")]
        public StockistModel GetStockistNoAvailable(string StockistNo)
        {
            StockistModel stockist = new StockistModel();
            try
            {
                stockist = _unitOfWork.MastersRepository.GetStockistNoAvailables(StockistNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistNoAvailable", "Get Check StocksitNo Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return stockist;
        }
        #endregion

        #region Get Check TransporterNo is Available
        [HttpGet]
        [Route("Masters/GetTransporterNoAvailable/{TransporterNo}")]
        public TransporterMasterLst GetTransporterNoAvailable(string TransporterNo)
        {
            TransporterMasterLst Transporter = new TransporterMasterLst();
            try
            {
                Transporter = _unitOfWork.MastersRepository.GetTransporterNoAvailables(TransporterNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterNoAvailable", "Get Check TransporterNo is Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Transporter;
        }
        #endregion

        #region Get Print Branch List
        [HttpGet]
        [Route("Masters/GetPrintBranchList/{Status}")]
        [AllowAnonymous]
        public List<BranchList> GetGetPrintBranchList(string Status)
        {
            List<BranchList> branchList = new List<BranchList>();
            try
            {
                branchList = _unitOfWork.MastersRepository.GetBranchList(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintBranchList", " Get Print Branch List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return branchList;
        }
        #endregion

        #region Add Edit City Master
        [HttpPost]
        [Route("Masters/AddEditCityMaster")]
        public string AddEditCityMaster([FromBody] CityMaster model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditCityMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditGeneralMaster", "Add Edit General Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Threshold value details
        [HttpGet]
        [Route("Masters/GetThresholdvalueDtls/{BranchId}/{CompanyId}")]
        public List<ThresholdValueDtls> GetThresholdvalueDtls(int BranchId, int CompanyId)
        {
            List<ThresholdValueDtls> ThresholdList = new List<ThresholdValueDtls>();
            try
            {
                ThresholdList = _unitOfWork.MastersRepository.GetThresholdvalueDtls(BranchId, CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetThresholdvalueDtls", "Get Threshold Value Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ThresholdList;
        }
        #endregion

        #region Add Edit and Delete Threshold value Master
        [HttpPost]
        [Route("Masters/AddEditThresholdValueMaster")]
        public int AddEditThresholdValueMaster([FromBody] ThresholdValueDtls model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditThresholdValueMaster(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditThresholdValueMaster", "Add Edit Threshold Value Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Add/Edit Checklist Masters
        [HttpPost]
        [Route("Masters/ChecklistMastersAddEdit")]
        public int ChecklistMastersAddEdit([FromBody] ChecklistMastersAddEditModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.MastersRepository.ChecklistMastersAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChecklistMastersAddEdit", "Checklist Masters Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Checklist Master List
        [HttpGet]
        [Route("Masters/GetChecklistMasterList/{BranchId}/{CompId}/{Status}")]
        public List<ChecklistMastersAddEditModel> GetChecklistMasterList(int BranchId, int CompId, string Status)
        {
            List<ChecklistMastersAddEditModel> ChecklistMasterList = new List<ChecklistMastersAddEditModel>();
            try
            {
                ChecklistMasterList = _unitOfWork.MastersRepository.GetChecklistMasterList(BranchId, CompId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChecklistMasterList", "GetChecklistMasterList" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChecklistMasterList;
        }
        #endregion

        #region Get Sequence No Already Available
        [HttpGet]
        [Route("Masters/checkSequenceNo/{SeqNo}/{BranchId}/{CompId}")]
        public ChecklistMastersAddEditModel GetcheckSequenceNoAvailable(int SeqNo, int BranchId, int CompId)
        {
            ChecklistMastersAddEditModel SeqNol = new ChecklistMastersAddEditModel();
            try
            {
                SeqNol = _unitOfWork.MastersRepository.GetcheckSequenceNoAvailable(SeqNo, BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetcheckSequenceNoAvailable", "Get check SequenceNo Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SeqNol;
        }
        #endregion

        #region Other CNF Master Add Edit
        [HttpPost]
        [Route("Masters/OtherCNFMasterAddEdit")]
        public int OtherCNFMasterAddEdit([FromBody] OtherCNFDtlsModel model)
        {
            int Result = 0;
            try
            {
                Result = _unitOfWork.MastersRepository.OtherCNFMasterAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OtherCNFMasterAddEdit", "Other CNF Master Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Other CNF List
        [HttpGet]
        [Route("Masters/GetOtherCNFList/{BranchId}/{CompId}/{Status}")]
        public List<OtherCNFDtlsModel> GetOtherCNFList(int BranchId, int CompId, string Status)
        {
            List<OtherCNFDtlsModel> ListCount = new List<OtherCNFDtlsModel>();
            try
            {
                ListCount = _unitOfWork.MastersRepository.GetOtherCNFList(BranchId, CompId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOtherCNFList", "Get Other CNF List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ListCount;
        }
        #endregion

        #region Add Version Details (Mobile & Web Application)
        [HttpPost]
        [Route("Masters/AddVersionDetails")]
        public string AddVersionDetails([FromBody] VersionDetailsModel model)
        {
            string message = string.Empty;
            try
            {
                message = _unitOfWork.MastersRepository.AddVersionDetails(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddVersionDetails", "Add Version Details (Mobile & Web Application) - BranchId:  " + model.BranchId + " CompanyId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message;
        }
        #endregion

        #region Delete Version Details (Mobile & Web Application)
        [HttpGet]
        [Route("Masters/DeleteVersionDetails/{VersionId}")]
        public int DeleteVersionDetails(int VersionId)
        {
            int lst = 0;
            try
            {
                lst = _unitOfWork.MastersRepository.DeleteVersionDetails(VersionId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteVersionDetails", "Delete Version Details (Mobile & Web Application) - BranchId:  " , BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return lst;
        }

        #endregion

        #region Get Version Details
        [HttpGet]
        [Route("Masters/GetVersionDetails")]
        public List<VersionDetailsModel> GetVersionDetails()
        {
            List<VersionDetailsModel> modelList = new List<VersionDetailsModel>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetVersionDetails();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVersionDetails", "Get Version Details (Web Application)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region To Check Version Number
        [HttpPost]
        [Route("Masters/CheckVersionNo")]
        public CheckVersionNoModel CheckVersionNo([FromBody] CheckVersionNoModel model)
        {
            CheckVersionNoModel res = new CheckVersionNoModel();
            try
            {
                res = _unitOfWork.MastersRepository.CheckVersionNo(model.VersionNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckVersionNo", "To Check Version Number (Web Application) - BranchId:  " + model.BranchId + " CompanyId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return res;
        }
        #endregion

        #region Get Version Details By Id
        [HttpGet]
        [Route("Masters/GetLatestVersionDetails")]
        public string GetVersionDetailsById()
        {
            string message = string.Empty;
            try
            {
                message = _unitOfWork.MastersRepository.GetLatestVersionDetails();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVersionDetailsById", "Get Version Details By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message;
        }
        #endregion

        #region Get Company Branch Relation List
        [HttpGet]
        [Route("Masters/GetCompanyBranchRelationList/{BranchId}")]
        public List<CompanyRelation> GetComapnyBranchRelationList(int BranchId)
        {
            List<CompanyRelation> CompLst = new List<CompanyRelation>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetComapnyBranchRelationList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetComapnyBranchRelationList", "Get Comapny Branch Relation List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Get Company List By Branch 
        [HttpGet]
        [Route("Masters/GetCompanyListByBranch/{BranchId}/{Status}")]
        public List<CompanyDtls> GetCompanyListByBranch(int BranchId, string Status)
        {
            List<CompanyDtls> CompLst = new List<CompanyDtls>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetCompanyListByBranch(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyListByBranch", "Get Company List By Branch", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Add Edit Company Branch Relation
        [HttpPost]
        [Route("Masters/AddEditComapanyBranchRelation")]
        public int AddEditCompanyBranchRelation([FromBody] CompBranchRelation model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditCompanyBranchRelation(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCompanyBranchRelation", "Add Edit Company Branch Relation", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Add Edit Vendor Details
        [HttpPost]
        [Route("Masters/AddVendorDetails")]
        public int AddVendorDetails([FromBody] VendorDetailsModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.AddVendorDetails(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddVendorDetails", "Add Edit Vendor Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Vendor List
        [HttpGet]
        [Route("Masters/GetVendorList/{Branch}/{Status}")]
        public List<VendorDetailsModel> GetVendorList(int Branch, string Status)
        {
            List<VendorDetailsModel> model = new List<VendorDetailsModel>();
            try
            {
                model = _unitOfWork.MastersRepository.GetVendorList(Branch, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVendorList", "Get Vendor List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return model;
        }
        #endregion

        #region Vendor Delete Deactivate
        [HttpPost]
        [Route("Masters/VendorDeleteDeactivate")]
        public int VendorDeleteDeactivate([FromBody] VendorDetailsModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.VendorDeleteDeactivate(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "VendorDeleteDeactivate", "Vendor Delete Deactivate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Add Edit Tax Master
        [HttpPost]
        [Route("Masters/AddEditTaxMaster")]
        public int AddEditTaxMaster([FromBody] TaxMastermodel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.TaxMasterAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditGeneralMaster", "Add Edit General Master", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Tax Master
        [HttpGet]
        [Route("Masters/GetTaxMasterList/{BranchId}")]
        public List<TaxMastermodel> GetTaxMasterList(int BranchId)
        {
            List<TaxMastermodel> modelList = new List<TaxMastermodel>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetTaxMaster(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTaxMasterList", "Get Tax Master List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Add EditHead Master
        [HttpPost]
        [Route("Masters/HeadMasterAddEdit")]
        public int HeadMasterAddEdit([FromBody]HeadMasterModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.MastersRepository.HeadMasterAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "HeadMasterAddEdit", "Head Master Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Head Master List
        [HttpGet]
        [Route("Masters/HeadMasterList/{BranchId}")]
        public List<HeadMasterModel> HeadMasterList(int BranchId)
        {
            List<HeadMasterModel> HeadMasterList = new List<HeadMasterModel>();
            try
            {
                HeadMasterList = _unitOfWork.MastersRepository.HeadMasterList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "HeadMasterList", "Head Master List" + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return HeadMasterList;
        }
        #endregion

        #region Transporter Parent Add Edit
        [HttpPost]
        [Route("Masters/TransporterParentAddEdit")]
        public int TransporterParentAddEdit([FromBody] TransporterParentModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.TransporterParentAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransporterParentAddEdit", "Transporter Parent Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Transporter Parent List
        [HttpGet]
        [Route("Masters/GetTransporterParentList/{BranchId}/{Status}")]
        public List<TransporterParentModel> GetTransporterParentList(int BranchId, string Status)
        {
            List<TransporterParentModel> TransporterParent = new List<TransporterParentModel>();
            try
            {
                TransporterParent = _unitOfWork.MastersRepository.GetTransporterParentList(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterParentList", "Get Transporter Parent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return TransporterParent;
        }
        #endregion

        #region Get Transporter Parent
        [HttpGet]
        [Route("Masters/GetTransporterParent/{Tpid}")]
        public TransporterParentModel GetTransporterParent(int Tpid)
        {
            TransporterParentModel TransporterParent = new TransporterParentModel();
            try
            {
                TransporterParent = _unitOfWork.MastersRepository.GetTransporterParent(Tpid);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterParent", "Get Transporter Parent", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return TransporterParent;
        }
        #endregion

        #region Parent Transporter Mapping Add Edit
        [HttpPost]
        [Route("Masters/ParentTransporterMappingAddEdit")]
        public int ParentTransporterMappingAddEdit([FromBody] ParentTranporterMappingModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.ParentTransporterMappingAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ParentTransporterMappingAddEdit", "Parent Transporter Mapping Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Parent Transport Mapped List
        [HttpGet]
        [Route("Masters/GetParentTransportMappedList/{Tid}/{Status}/{BranchId}")]
        public List<ParentTransportMappList> GetParentTransportMappedList(int Tid, string Status,int BranchId)
        {
            List<ParentTransportMappList> CompLst = new List<ParentTransportMappList>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetParentTransportMappedList(Tid, Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetParentTransportMappedList", "Get Parent Transport Mapped List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Get Courier Parent List
        [HttpGet]
        [Route("Masters/GetCourierParentList/{BranchId}/{Status}")]
        public List<CourierParentModel> GetCourierParentList(int BranchId, string Status)
        {
            List<CourierParentModel> CourierParent = new List<CourierParentModel>();
            try
            {
                CourierParent = _unitOfWork.MastersRepository.GetCourierParentList(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCourierParentList", "Get Courier Parent List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return CourierParent;
        }
        #endregion

        #region Get Courier Parent
        [HttpGet]
        [Route("Masters/GetCourierParent/{Cpid}")]
        public CourierParentModel GetCourierParent(int Cpid)
        {
            CourierParentModel CourierParent = new CourierParentModel();
            try
            {
                CourierParent = _unitOfWork.MastersRepository.GetCourierParent(Cpid);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCourierParent", "Get Courier Parent", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return CourierParent;
        }
        #endregion

        #region Courier Parent Add Edit
        [HttpPost]
        [Route("Masters/CourierParentAddEdit")]
        public int CourierParentAddEdit([FromBody] CourierParentModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.CourierParentAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CourierParentAddEdit", "Courier Parent Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Parent Courier Mapped List
        [HttpGet]
        [Route("Masters/GetParentCourierMappedList/{CPid}/{Status}/{BranchId}")]
        public List<ParentCourierMappList> GetParentCourierMappList(int CPid, string Status, int BranchId)
        {
            List<ParentCourierMappList> courMappLst = new List<ParentCourierMappList>();
            try
            {
                courMappLst = _unitOfWork.MastersRepository.GetParentCourierMappList(CPid, Status,BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetParentCourierMappList", "Get Parent Courier Mapp List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return courMappLst;
        }

        #endregion

        #region Parent Courier Mapping ADD Edit
        [HttpPost]
        [Route("Masters/ParentCourierMappingAddEdit")]
        public int ParentCourierMappingAddEdit([FromBody] ParentCourierMappingModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.ParentCourierMappingAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ParentCourierAddEdit", "Parent Courier Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }

        #endregion

        #region Get Branch List For Login
        [HttpGet]
        [Route("Masters/GetBranchListForLogin/{Status}")]
        [AllowAnonymous]
        public List<BranchList> GetBranchListForLogin(string Status)
        {
            List<BranchList> branchList = new List<BranchList>();
            try
            {
                branchList = _unitOfWork.MastersRepository.GetBranchList(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchList", " Get Branch List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return branchList;
        }
        #endregion

        #region Get Vendor List By Company
        [HttpGet]
        [Route("Masters/GetVendorListByCompany/{CompanyId}/{Status}/{BranchId}")]
        public List<vendordtlsMapping> GetVendorListByCompany(int CompanyId, string Status,int BranchId)
        {
            List<vendordtlsMapping> VendorList = new List<vendordtlsMapping>();
            try
            {
                VendorList = _unitOfWork.MastersRepository.GetVendorListByCompany(CompanyId, Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVendorListByCompany", "Get Vendor List By Company", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return VendorList;
        }
        #endregion

        #region Add Edit Company Vendor Mapping
        [HttpPost]
        [Route("Masters/AddEditCompanyVendorMapping")]
        public int AddEditCompanyVendorMapping([FromBody] CompVendorAddEditMapping model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditCompanyVendorMapping(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCompanyVendorMapping", "Add Edit Company Vendor Mapping", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Vendor List By Branch
        [HttpGet]
        [Route("Masters/GetVendorListByBranch/{BranchId}/{Status}")]
        public List<vendordBranchMapping> GetVendorListByBranch(int BranchId, string Status)
        {
            List<vendordBranchMapping> VendorList = new List<vendordBranchMapping>();
            try
            {
                VendorList = _unitOfWork.MastersRepository.GetVendorListByBranch(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVendorListByBranch", "Get Vendor List By Branch", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return VendorList;
        }
        #endregion

        #region Add Edit Branch Vendor Mapping
        [HttpPost]
        [Route("Masters/AddEditBranchVendorMapping")]
        public int AddEditBranchVendorMapping([FromBody] VendorBranchAddEditMapping model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.MastersRepository.AddEditBranchVendorMapping(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditBranchVendorMapping", "Add Edit Branch Vendor Mapping", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Expiry Stockist Notification Dashboard
        [HttpGet]
        [Route("Masters/ExpiryStockistNotificationDashboard/{BranchId}/{CompId}/{Flag}")]
        public List<ExpiryStockistNotidashModel> ExpiryStockistNotificationDashboard(int BranchId, int CompId, string Flag)
        {
            List<ExpiryStockistNotidashModel> ExpListNoti = new List<ExpiryStockistNotidashModel>();
            try
            {
                ExpListNoti = _unitOfWork.MastersRepository.ExpiryStockistNotificationDashboard(BranchId, CompId, Flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpiryStockistNotificationDashboard", "Expiry Stockist Notification Dashboard", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ExpListNoti;
        }
        #endregion

        #region Expiry List For Notification Dashboard
        [HttpGet]
        [Route("Masters/ExpiryListForNotificationList/{BranchId}/{CompId}")]
        [AllowAnonymous]
        public List<ExpiryListForNotiListModel> ExpiryListForNotificationList(int BranchId, int CompId)
        {
            List<ExpiryListForNotiListModel> ExpListlst = new List<ExpiryListForNotiListModel>();
            try
            {
                ExpListlst = _unitOfWork.MastersRepository.ExpiryListForNotificationList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpiryListForNitificationList", "Expiry List For Notification List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ExpListlst;
        }
        #endregion

        # region Get GST Type List
        [HttpGet]
        [Route("Masters/GetGSTTypeList/{TaxId}/{BranchId}")]
        public GSTTypeModel GetGSTTypeList(int TaxId, int BranchId)
        {
            GSTTypeModel GSTTypeList = new GSTTypeModel();
            try
            {
                GSTTypeList = _unitOfWork.MastersRepository.GetGSTTypeList(TaxId,BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGSTTypeList", "Get GST Type List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GSTTypeList;
        }
        #endregion

        #region Company Detalis
        [HttpGet]
        [Route("Masters/GetCompanyListByBRIdForEMP/{BranchId}/{Status}")]
        public List<CompanyListByBRModel> GetCompanyListByBRIdForEMP(int BranchId, string Status)
        {
            List<CompanyListByBRModel> modelList = new List<CompanyListByBRModel>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetCompanyListByBRIdForEMP(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyList", "Get Company List By BR Id For EMP", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region SLA Master List For Instate Amt and Out OutSate Amt For Mobile
        [HttpGet]
        [Route("Masters/SLAMasterlist/{BranchId}/{CompId}")]
        public ThresholdValueDtls SLAMasterlist(int BranchId , int CompId)
        {
            ThresholdValueDtls sllist = new ThresholdValueDtls();
            try
            {
                sllist = _unitOfWork.MastersRepository.SLAMasterlist(BranchId, CompId);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SLAMasterlist", "SLA Master list", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return sllist;
        }
        #endregion

        #region Get Transport List with Stockist For Mob
        [HttpGet]
        [Route("Masters/TransportListwithStockies/{BranchId}/{CompanyId}/{StockiesId}/{Status}")]
        public List<TranLstwithStkModel> TransportListwithStockies(int BranchId, int CompanyId, int StockiesId,string Status)
        {
            List<TranLstwithStkModel> TransLststk = new List<TranLstwithStkModel>();
            try
            {
                TransLststk = _unitOfWork.MastersRepository.TransportListwithStockies(BranchId, CompanyId, StockiesId,Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransportListwithStockies", "Transport List with Stockies", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransLststk;
        }
        #endregion

        #region INSERT, UPDATE and DELETE Query Builder
        [HttpPost]
        [Route("Masters/Savequerybuilder")]
        public string Savequerybuilder(QueryBuilderModel model)
        {
            try
            {
                int NoOfRowAffected;

                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    NoOfRowAffected = _contextManager.Database.ExecuteSqlCommand(model.Query.Trim());             
                }
          
                model.Status = "No Of Row Affected: " + NoOfRowAffected + "";
            }
            catch (Exception Ex)
            {
                model.Status = Ex.Message + "-" + Ex.InnerException;
            }
                return model.Status;

        }
        #endregion

        #region Select Query Builder
        [Route("Masters/PostQueryBuilder")]
        public IHttpActionResult PostQueryBuilder([FromBody] QueryBuilderModel query)
        {
            try
            {
                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    using (var connection = _contextManager.Database.Connection)
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = query.Query;
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[reader.GetName(i)] = reader.GetValue(i);
                                    }
                                    result.Add(row);
                                }
                            }
                        }
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Get Owner Dashboard Count List
        [HttpPost]
        [Route("Masters/GetOwnerDashboardCountList")]
        public OwnerLoginDashCnt GetOwnerDashboardCountList(OwnerLoginDashCnt model)
        {
            OwnerLoginDashCnt ownerLoginCnt = new OwnerLoginDashCnt();
            try
            {
                ownerLoginCnt = _unitOfWork.MastersRepository.GetOwnerDashboardCountList(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerDashboardCountList", "Get Owner Dashboard Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ownerLoginCnt;
        }
        #endregion

        #region Get Stockist List By Branch Company wise for verify
        [HttpGet]
        [Route("Masters/GetStockistListByBranchCompany/{BranchId}/{CompanyId}/{Status}")]
        public List<StockistModel> GetStockistListByBranchCompany(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            List<StockistModel> invalidData = new List<StockistModel>();
            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistListByBranchCompany(BranchId, CompanyId, Status);
                invalidData = VerifyStokistForValid(StockLst);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistListByBranchCompany", "Get Stockist List By Branch Company", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return invalidData; // Return the list of objects with invalid data
        }
        #endregion

        #region Get Stockist List for Verify Data
        [HttpGet]
        [Route("Masters/GetStockistListforVerifyData/{BranchId}/{CompanyId}/{Status}")]
        [AllowAnonymous]
        public List<StockistModel> GetStockistListforVerifyDataList(int BranchId, int CompanyId, string Status)
        {
            List<StockistModel> StockLst = new List<StockistModel>();
            List<StockistModel> invalidData = new List<StockistModel>();

            try
            {
                StockLst = _unitOfWork.MastersRepository.GetStockistListforVerifyDataList(BranchId, CompanyId, Status);
                invalidData = VerifyStokistForValid(StockLst); //method call for verify stockist data
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistListforVerifyDataList", "Get Stockist List for Verify DataList", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return invalidData; // Return the list of objects with invalid data
        }
        #endregion

        #region Verify Data Method
        private List<StockistModel> VerifyStokistForValid(List<StockistModel> stockLst)
        {
            List<StockistModel> invalidStockistData = new List<StockistModel>();
            foreach (var stockist in stockLst)
            {
                if (!stockist.IsCustomerCodeValid() || !stockist.HasOnlyAlphanumericCharacters()
                    || !stockist.EmailAddressOnly() || !stockist.NumberOnlyAndComma() || !stockist.PanAlphaNumberOnly()
                    || !stockist.GstAlphaNumberOnly() || !stockist.DlNoCodeOnly()
                    || !stockist.AcntNoLalidation() || !!stockist.BankIdValidation() || !stockist.DLExpiryrDateValidation()
                    || !stockist.FoodExpryDateValidation() || !stockist.FoodLicNoValidate() || !stockist.CityValidate())
                {
                    invalidStockistData.Add(stockist);  //push Invalid data
                }

            }
            return invalidStockistData;
        }
        #endregion 

        #region To Get Import Type List 
        [HttpGet]
        [Route("Masters/GetImportTypeList")]
        public List<ImportTypeList> GetImportTypeList()
        {
            List<ImportTypeList> modelList = new List<ImportTypeList>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetImportTypeList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportTypeList", "Get Import Type List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Import File and Column Relation List
        [HttpGet]
        [Route("Masters/GetImportFileandColumnRelList/{BranchId}/{CompId}/{ImportId}")]
        public List<ImportFileColumnRelList> GetImportFileandColumnRelList(int BranchId,int CompId,int ImportId)
        {
            List<ImportFileColumnRelList> CompLst = new List<ImportFileColumnRelList>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetImportFileandColumnRelList(BranchId, CompId, ImportId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportFileandColumnRelList", "Get Import File and Column Rel List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Get Import File and Column Relation List
        [HttpGet]
        [Route("Masters/OnChangeColFieldList/{BranchId}/{CompId}/{ImpId}")]
        public List<ImportFileColumnRelList> OnChangeColFieldList(int BranchId, int CompId, int ImpId)
        {
            List<ImportFileColumnRelList> CompLst = new List<ImportFileColumnRelList>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.OnChangeColFieldList(BranchId, CompId, ImpId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OnChangeColFieldList", "On Change Col Field List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Add/Edit Import Dynamic Fields Masters
        [HttpPost]
        [Route("Masters/ImportDymAddEdit")]
        public int ImportDymAddEdit([FromBody] ImportDymAddEditModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.MastersRepository.ImportDymAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ImportDymAddEdit", "Import Dynamic Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Import Data Dynamically Master List
        [HttpGet]
        [Route("Masters/GetImportDyanamically/{BranchId}/{CompId}")]
        public List<ImportDynaListModel> GetImportDyanamically(int BranchId, int CompId)
        {
            List<ImportDynaListModel> ImportDynaList = new List<ImportDynaListModel>();
            try
            {
                ImportDynaList = _unitOfWork.MastersRepository.GetImportDyanamically(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportDyanamically", "Get Import Dyanamically" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImportDynaList;
        }
        #endregion

        #region Get Branch Comp List For Mob
        [HttpGet]
        [Route("Masters/GetBranchCompListForMob/{Status}")]
        public List<BranchDtlsList> GetBranchCompListForMob(string Status)
        {
            List<BranchDtlsList> branchCompLst = new List<BranchDtlsList>();
            try
            {
                branchCompLst = _unitOfWork.MastersRepository.GetBranchCompListForMob(Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetBranchCompListForMob", "Get Branch Comp List For Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return branchCompLst;
        }
        #endregion

        #region Get Expense Register Tds
        [HttpGet]
        [Route("Masters/GetExpenseRegisterTds/{Branch}/{VendorId}")]
        public List<VenderIsTds> GetExpenseRegisterTds(int Branch, int VendorId)
        {
            List<VenderIsTds> model = new List<VenderIsTds>();
            try
            {
                model = _unitOfWork.MastersRepository.GetExpenseRegisterTds(Branch, VendorId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseRegisterTds", "Get Vendor List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return model;
        }
        #endregion

        #region Company vendor Dropdown Detalis
        [HttpGet]
        [Route("Masters/GetCompanyLstForAccount/{Status}/{BranchId}")]
        public List<CompanyDtls> GetCompanyLstForAccount(string Status, int BranchId)
        {
            List<CompanyDtls> modelList = new List<CompanyDtls>();
            try
            {
                modelList = _unitOfWork.MastersRepository.GetCompanyLstForAccount(Status, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Get Company Lst For Account", "Get Company Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Expense Transporter Tds
        [HttpGet]
        [Route("Masters/GetExpenseTransporterTds/{BranchId}/{TransporterId}")]
        public List<TransporterIsTds> GetExpenseTransporterTds(int BranchId, int TransporterId)
        {
            List<TransporterIsTds> modellist = new List<TransporterIsTds>();
            try
            {
                modellist = _unitOfWork.MastersRepository.GetExpenseTransporterTds(BranchId, TransporterId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseTransporterTds", "Get Transporter List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modellist;
        }
        #endregion

        #region Get Expense Courier Tds
        [HttpGet]
        [Route("Masters/GetExpenseCourierTds/{BranchId}/{CourierId}")]
        public List<CourierIsTds> GetExpenseCourierTds(int BranchId, int CourierId)
        {
            List<CourierIsTds> modellist = new List<CourierIsTds>();
            try
            {
                modellist = _unitOfWork.MastersRepository.GetExpenseCourierTds(BranchId, CourierId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenseCourierTds", "Get Transporter List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modellist;
        }
        #endregion

        #region Brantch Details By BranchId By Hrishikesh
        [HttpGet]
        [Route("Masters/GetBranchDetailsById/{BranchId}")]
        public List<BranchDetails> GetBranchDetailsById(int BranchId)
        {
            List<BranchDetails> list = new List<BranchDetails>();
            try
            {
                list = _unitOfWork.MastersRepository.GetBranchDetailsById(BranchId);
            }
            catch (Exception e)
            {
                BusinessCont.SaveLog(0, 0, BranchId, "GetBranchDetailsById", "Get Branch Details By BranchId Hrishi ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(e.InnerException));
            }
            return list;
        }
        #endregion


        #region Get Company Details By Branch By Hrishi
        [HttpGet]
        [Route("Masters/GetBranchWiseCompany/{BranchId}")]
        [AllowAnonymous]
        public List<CompanyRelationHrishi>GetBranchWiseCompany(int BranchId)
        {
            List<CompanyRelationHrishi> CompLst = new List<CompanyRelationHrishi>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetBranchWiseCompany(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetComapnyBranchRelationList", "Get Comapny Branch Relation List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion

        #region Printer Data List Created by Pratyush 
        [HttpPost]
        [Route("Masters/GetPrinterDataList")]
        [AllowAnonymous]
        public List<PrinterDataModal> GetPrinterDataList([FromBody] PrinterValueModal model)
        {
            List<PrinterDataModal> list = new List<PrinterDataModal>();
            try
            {
                list = _unitOfWork.MastersRepository.GetPrinterDataList(model.BranchId, model.CompanyId, model.FromDate, model.ToDate, model.Flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterDataList", "Get Printer Data List " + "BranchId:  " + model.BranchId + "CompanyId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return list;
        }
        #endregion

        #region Update PrinterPDF Details By Hrishi
        [HttpPost]
        [Route("Masters/UpdatePrinterDetails")]
        public string UpdatePrinterDetails([FromBody] PrinterValueModal model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.MastersRepository.UpdatePrinterDetails(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.BranchId != 0 ? model.BranchId: 0), "UpdatePrinterDetails", " Printer PDF Details Updated", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion


        #region Get Company List By Branch For Print
        [HttpGet]
        [Route("Masters/GetCompanyListByBranchForPrint/{BranchId}/{Status}")]
        [AllowAnonymous]
        public List<CompanyDtls> GetCompanyListByBranchForPrint(int BranchId, string Status)
        {
            List<CompanyDtls> CompLst = new List<CompanyDtls>();
            try
            {
                CompLst = _unitOfWork.MastersRepository.GetCompanyListByBranch(BranchId, Status);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCompanyListByBranchForPrint", "Get Company List By Branch For Print", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CompLst;
        }
        #endregion Get Company List By Branch For Print

    }


}

