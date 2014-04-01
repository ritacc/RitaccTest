using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework;
using App.Framework.Data;
using System.ComponentModel.DataAnnotations;
using App.Framework.Export;

namespace CSC.Business {
    #region Province

    public class ProvinceListModel {
        public BusinessList<Province> ProvinceList { get; set; }

        public SearchProvinceCriteria ProvinceSearch { get; set; }
    }

    public class ProvinceListForDDLModel {
        public BusinessList<Province> ProvinceList { get; set; }

        public SearchProvinceCriteriaForDDL ProvinceDDLSearch { get; set; }
    }
    public class ProvinceListForLovModel {
        public BusinessList<Province> ProvinceList { get; set; }

        public SearchProvinceCriteriaForLov ProvinceLovSearch { get; set; }
    }


    [DbCommand("spSearchProvince")]
    public class SearchProvinceCriteria : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }
    }
    
    [DbCommand("spLoadProvince")]
    public class LoadProvinceCriteria : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }
    }

    [DbCommand("spSaveProvince")]
    public class SaveProvinceCriteria : DataCriteria {
        private Province m_Parent;

        public SaveProvinceCriteria(Province parent) {
            m_Parent = parent;
        }
        [DbParameter("CODE ")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }

        [DbParameter("FULL_NAME")]
        public string FullName {
            get { return m_Parent.FullName; }
            set { m_Parent.FullName = value; }
        }

        [DbParameter("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate {
            get { return m_Parent.LastUpdateDate; }
            set { m_Parent.LastUpdateDate = value; }
        }

        [DbParameter("LAST_UPDATED_BY")]
        public long LastUpdatedBy {
            get { return m_Parent.LastUpdatedBy; }
            set { m_Parent.LastUpdatedBy = value; }
        }

        [DbParameter("CURRENT_USER_ID")]
        public long CurrentUserID {
            get { return App.Framework.Security.User.Current.UserId; }
        }

        [DbParameter("RecordStatus")]
        public string RecordStatus {
            get { return m_Parent.RecordStatus; }
            set { m_Parent.RecordStatus = value; }
        }
    }

    [DbCommand("spDeleteProvince")]
    public class DeleteProvinceCriteria : DataCriteria {
        private Province m_Parent;

        [DbParameter("CODE")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }

        public DeleteProvinceCriteria(Province parent) {
            m_Parent = parent;
        }
    }
    
    public class Province : DataEntity {

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        [DbField("CODE")]
		[Export("Province", "Province_Code")]
        public string Code {get;set;}

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        [DbField("FULL_NAME")]
		[Export("Full Name", "Province_FullName")]
        public string FullName {get;set;}

        [DbField("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate {get;set;}

        [DbField("LAST_UPDATED_BY")]
        public long LastUpdatedBy {get;set;}

        [DbField("CREATION_DATE")]
        public DateTime CreationDate {get;set;}

        [DbField("CREATED_BY")]
        public long CreatedBy {get;set;}

        [DbField("LAST_UPDATE_LOGIN")]
        public long? LastUpdateLogin {get;set;}

        [DbField("RecordStatus")]
        public string RecordStatus {get;set;}

        public override DataCriteria CreateDeleteCriteria() {
            return new DeleteProvinceCriteria(this);
        }

        public override DataCriteria CreateSaveCriteria() {
            return new SaveProvinceCriteria(this);
        }
    }

    #region 提供给其他模块使用

    [DbCommand("spSearchProvinceForDDL")]
    public class SearchProvinceCriteriaForDDL : DataCriteria {
    }

    [DbCommand("spSearchProvinceForLov")]
    public class SearchProvinceCriteriaForLov : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }
    }
    #endregion

    #endregion

    #region City

    public class CityListModel {

        public BusinessList<City> CityList { get; set; }

        public SearchCityCriteria CitySearch { get; set; }
    }

    public class CityListForDDLModel {
        public BusinessList<City> CityList { get; set; }

        public SearchCityCriteriaForDDL CityDDLSearch { get; set; }
    }

    public class CityListForLovModel {
        public BusinessList<City> CityList { get; set; }

        public SearchCityCriteriaForLov CityLovSearch { get; set; }
    }


    [DbCommand("spSearchCity")]
    public class SearchCityCriteria : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }

        [DbParameter("PROV_CODE")]
        public string ProvCode { get; set; }
    }

    [DbCommand("spLoadCity")]
    public class LoadCityCriteria : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("PROV_CODE")]
        public string ProvCode { get; set; }
    }

    [DbCommand("spSaveCity")]
    public class SaveCityCriteria : DataCriteria {
        private City m_Parent;

        public SaveCityCriteria(City parent) {
            m_Parent = parent;
        }

        [DbParameter("PROV_CODE")]
        public string ProvCode {
            get { return m_Parent.ProvCode; }
            set { m_Parent.ProvCode = value; }
        }

        [DbParameter("CODE")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }

        [DbParameter("FULL_NAME")]
        public string FullName {
            get { return m_Parent.FullName; }
            set { m_Parent.FullName = value; }
        }

        [DbParameter("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate {
            get { return m_Parent.LastUpdateDate; }
            set { m_Parent.LastUpdateDate = value; }
        }

        [DbParameter("LAST_UPDATED_BY")]
        public long LastUpdatedBy {
            get { return m_Parent.LastUpdatedBy; }
            set { m_Parent.LastUpdatedBy = value; }
        }

        [DbParameter("CURRENT_USER_ID")]
        public long CurrentUserID {
            get { return App.Framework.Security.User.Current.UserId; }
        }

        [DbParameter("RecordStatus")]
        public string RecordStatus {
            get { return m_Parent.RecordStatus; }
            set { m_Parent.RecordStatus = value; }
        }
    }

    [DbCommand("spDeleteCity")]
    public class DeleteCityCriteria : DataCriteria {
        private City m_Parent;

        [DbParameter("PROV_CODE")]
        public string ProvCode {
            get { return m_Parent.ProvCode; }
            set { m_Parent.ProvCode = value; }
        }

        [DbParameter("CODE")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }

        public DeleteCityCriteria(City parent) {
            m_Parent = parent;
        }
    }

    [DbCommand("spSearchCityForDDL")]
    public class SearchCityCriteriaForDDL : DataCriteria {
        [DbParameter("PROV_CODE")]
        public string ProvCode { get; set; }
    }

    [DbCommand("spSearchCityForLov")]
    public class SearchCityCriteriaForLov : DataCriteria {
        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }

        [DbParameter("PROV_CODE")]
        public string ProvCode { get; set; }
    }

    public class City : DataEntity {
        [DbField("PROV_CODE")]
        [Export("Province", "Province_Code")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string ProvCode { get; set; }

        [DbField("CODE")]
        [Export("City", "City_Code")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string Code { get; set; }

        [DbField("FULL_NAME")]
        [Export("Full Name", "City_FullName")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string FullName { get; set; }

        [DbField("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; }

        [DbField("LAST_UPDATED_BY")]
        public long LastUpdatedBy { get; set; }

        [DbField("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [DbField("CREATED_BY")]
        public long CreatedBy { get; set; }

        [DbField("LAST_UPDATE_LOGIN")]
        public long? LastUpdateLogin { get; set; }

        [DbField("RecordStatus")]
        public string RecordStatus {
            get;
            set;
        }
        public override DataCriteria CreateDeleteCriteria() {
            return new DeleteCityCriteria(this);
        }

        public override DataCriteria CreateSaveCriteria() {
            return new SaveCityCriteria(this);
        }
    }

    #endregion

    #region Area

    public class AreaListModel {

        public BusinessList<Area> AreaList { get; set; }

        public SearchAreaCriteria AreaSearch { get; set; }

    }

    public class AreaListForDDLModel {
        public BusinessList<Area> AreaList { get; set; }

        public SearchAreaCriteriaForDDL AreaDDLSearch { get; set; }
    }

    public class AreaListForLovModel {
        public BusinessList<Area> AreaList { get; set; }

        public SearchAreaCriteriaForLov AreaLovSearch { get; set; }
    }

    

    [DbCommand("spSearchArea")]
    public class SearchAreaCriteria : DataCriteria {
        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode { get; set; }

        [DbParameter("CITY_CODE")]
        public string CityCode { get; set; }

        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }
    }

    [DbCommand("spLoadArea")]
    public class LoadAreaCriteria : DataCriteria {
        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode { get; set; }

        [DbParameter("CITY_CODE")]
        public string CityCode { get; set; }

        [DbParameter("CODE")]
        public string Code { get; set; }
    }

    [DbCommand("spSaveArea")]
    public class SaveAreaCriteria : DataCriteria {
        private Area m_Parent;

        public SaveAreaCriteria(Area parent) {
            m_Parent = parent;
        }

        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode {
            get { return m_Parent.ProvCode; }
            set { m_Parent.ProvCode = value; }
        }

        [DbParameter("CITY_CODE")]
        public string CityCod {
            get { return m_Parent.CityCode; }
            set { m_Parent.CityCode = value; }
        }

        [DbParameter("CODE")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }

        [DbParameter("FULL_NAME")]
        public string FullName {
            get { return m_Parent.FullName; }
            set { m_Parent.FullName = value; }
        }

        [DbParameter("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate {
            get { return m_Parent.LastUpdateDate; }
            set { m_Parent.LastUpdateDate = value; }
        }

        [DbParameter("LAST_UPDATED_BY")]
        public long LastUpdatedBy {
            get { return m_Parent.LastUpdatedBy; }
            set { m_Parent.LastUpdatedBy = value; }
        }


        [DbParameter("CURRENT_USER_ID")]
        public long CurrentUserID {
            get { return App.Framework.Security.User.Current.UserId; }
        }

        [DbParameter("RecordStatus")]
        public string RecordStatus {
            get { return m_Parent.RecordStatus; }
            set { m_Parent.RecordStatus = value; }
        }
    }

    [DbCommand("spDeleteArea")]
    public class DeleteAreaCriteria : DataCriteria {
        private Area m_Parent;

        public DeleteAreaCriteria(Area parent) {
            m_Parent = parent;
        }

        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode {
            get { return m_Parent.ProvCode; }
            set { m_Parent.ProvCode = value; }
        }

        [DbParameter("CITY_CODE")]
        public string CityCod {
            get { return m_Parent.CityCode; }
            set { m_Parent.CityCode = value; }
        }

        [DbParameter("CODE")]
        public string Code {
            get { return m_Parent.Code; }
            set { m_Parent.Code = value; }
        }
    }

    [DbCommand("spSearchAreaForDDL")]
    public class SearchAreaCriteriaForDDL : DataCriteria {
        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode { get; set; }

        [DbParameter("CITY_CODE")]
        public string CityCode { get; set; }
    }

    [DbCommand("spSearchAreaForLov")]
    public class SearchAreaCriteriaForLov : DataCriteria {
        [DbParameter("CITY_PROV_CODE")]
        public string ProvCode { get; set; }

        [DbParameter("CITY_CODE")]
        public string CityCode { get; set; }

        [DbParameter("CODE")]
        public string Code { get; set; }

        [DbParameter("FULL_NAME")]
        public string FullName { get; set; }
    }

    public class Area : DataEntity {
        [DbField("CITY_PROV_CODE")]
		[Export("Province", "Province_Code")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string ProvCode { get; set; }

        [DbField("CITY_CODE")]
		[Export("City", "City_Code")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string CityCode { get; set; }

        [DbField("CODE")]
		[Export("Area", "Area_Code")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "RequiredMessage")]
        [DbField("FULL_NAME")]
		[Export("Full Name", "Area_FullName")]
        public string FullName { get; set; }

        [DbField("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; }

        [DbField("LAST_UPDATED_BY")]
        public long LastUpdatedBy { get; set; }

        [DbField("CREATION_DATE")]
        public DateTime CreationDate { get; set; }

        [DbField("CREATED_BY")]
        public long CreatedBy { get; set; }

        [DbField("LAST_UPDATE_LOGIN")]
        public long? LastUpdateLogin { get; set; }

        [DbField("RecordStatus")]
        public string RecordStatus {
            get;
            set;
        }
        public override DataCriteria CreateDeleteCriteria() {
            return new DeleteAreaCriteria(this);
        }

        public override DataCriteria CreateSaveCriteria() {
            return new SaveAreaCriteria(this);
        }
    }

    #endregion
}
