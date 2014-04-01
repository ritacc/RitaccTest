using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Web;

namespace CSC
{
	/// <summary>
	/// 权限点
	/// </summary>
	public enum EnumPermission
	{
		#region 系统首页
		[Enum("首页")]
		Home_Index,
		#endregion

		#region 登录查询
		[Enum("登录查询-查看")]
		AuditLog_View,
		#endregion'

		#region 店管理
		[Enum("店管理-查看")]
		Shop_View,
		[Enum("店管理-修改")]
		Shop_Edit,
		#endregion
		
		#region 参数
		[Enum("参数管理-查看")]
		Parameter_View,
		[Enum("参数管理-修改")]
		Parameter_Edit,
		#endregion
		
		#region 功能管理
		[Enum("功能管理-查看")]
		Function_View,
		[Enum("功能管理-修改")]
		Function_Edit,
		#endregion
		
		#region 密码变更
		[Enum("用户-修改密码")]
		User_ChangePassword,
		[Enum("修改自己的密码")]
		Own_ChangePassword,
		#endregion

		#region 角色
		[Enum("角色-查看")]
		Role_View,
		[Enum("角色-添加")]
		Role_Add,
		[Enum("角色-编辑")]
		Role_Edit,
		[Enum("角色-删除")]
		Role_Delete,
		#endregion

		#region 用户
		[Enum("用户-查看")]
		User_View,
		[Enum("用户-添加")]
		User_Add,
		[Enum("用户-编辑")]
		User_Edit,
		[Enum("用户-删除")]
		User_Delete,
		#endregion

		#region "Brand Code"
		[Enum("Brand Code-View")]
		CSCS02010_View,
		[Enum("Brand Code-Create")]
		CSCS02010_Add,
		[Enum("Brand Code-Edit")]
		CSCS02010_Edit,
		[Enum("Brand Code-Delete")]
		CSCS02010_Delete,
		#endregion

		#region "SERVICE_GROUP"
		[Enum("SERVICE_GROUP-View")]
		CSCS02020_View,
		[Enum("SERVICE_GROUP-Create")]
		CSCS02020_Add,
		[Enum("SERVICE_GROUP-Edit")]
		CSCS02020_Edit,
		[Enum("SERVICE_GROUP-Delete")]
		CSCS02020_Delete,
		#endregion

		#region "Reason Code"
		[Enum("Reason Code-View")]
		CSCP01010_View,
		[Enum("Reason Code-Add")]
		CSCP01010_Add,
		[Enum("Reason Code-Edit")]
		CSCP01010_Edit,
		[Enum("Reason Code-Delete")]
		CSCP01010_Delete,
		#endregion

		#region "Product Class"
		[Enum("Product Class-View")]
		CSCP01020_View,
		[Enum("Product Class-Add")]
		CSCP01020_Add,
		[Enum("Product Class-Edit")]
		CSCP01020_Edit,
		[Enum("Product Class-Delete")]
		CSCP01020_Delete,
		#endregion

		#region "Currency"
		[Enum("Currency-View")]
		CSCP01030_View,
		[Enum("Currency-Create")]
		CSCP01030_Add,
		[Enum("Currency-Edit")]
		CSCP01030_Edit,
		[Enum("Currency-Delete")]
		CSCP01030_Delete,
		#endregion

		#region "Exchange Rate"
		[Enum("Exchange Rate-View")]
		CSCP01040_View,
		[Enum("Exchange Rate-Create")]
		CSCP01040_Add,
		[Enum("Exchange Rate-Edit")]
		CSCP01040_Edit,
		[Enum("Exchange Rate-Delete")]
		CSCP01040_Delete,
		#endregion

		#region "Result Code"
		[Enum("Result Code-View")]
		CSCS02030_View,
		[Enum("Result Code-Create")]
		CSCS02030_Add,
		[Enum("Result Code-Edit")]
		CSCS02030_Edit,
		[Enum("Result Code-Delete")]
		CSCS02030_Delete,
		#endregion

		#region "Repair Code"
		[Enum("Repair Code-View")]
		CSCS02040_View,
		[Enum("Repair Code-Create")]
		CSCS02040_Add,
		[Enum("Repair Code-Edit")]
		CSCS02040_Edit,
		[Enum("Repair Code-Delete")]
		CSCS02040_Delete,
		#endregion

		#region "Charge Code"
		[Enum("Charge Code-View")]
		CSCS02050_View,
		[Enum("Charge Code-Create")]
		CSCS02050_Add,
		[Enum("Charge Code-Edit")]
		CSCS02050_Edit,
		[Enum("Charge Code-Delete")]
		CSCS02050_Delete,
		#endregion

		#region "Code Type"
		[Enum("Code Type-View")]
		CSCS02060_View,
		[Enum("Code Type-Create")]
		CSCS02060_Add,
		[Enum("Code Type-Edit")]
		CSCS02060_Edit,
		[Enum("Code Type-Delete")]
		CSCS02060_Delete,
		#endregion

		#region "Adress Code"
		[Enum("Adress Code-View")]
		CSCS02070_View,
		[Enum("Adress Code-Create")]
		CSCS02070_Add,
		[Enum("Adress Code-Edit")]
		CSCS02070_Edit,
		[Enum("Adress Code-Delete")]
		CSCS02070_Delete,
		#endregion

		#region "Supplier"
		[Enum("Supplier-View")]
		CSCP01050_View,
		[Enum("Supplier-Create")]
		CSCP01050_Add,
		[Enum("Supplier-Edit")]
		CSCP01050_Edit,
		[Enum("Supplier-Delete")]
		CSCP01050_Delete,
		#endregion

		#region "Client"
		[Enum("Client-View")]
		CSCP01060_View,
		[Enum("Client-Create")]
		CSCP01060_Add,
		[Enum("Client-Edit")]
		CSCP01060_Edit,
		[Enum("Client-Delete")]
		CSCP01060_Delete,
		#endregion

		#region "Model Class"
		[Enum("Model Class-View")]
		CSCS02080_View,
		[Enum("Model Class-Create")]
		CSCS02080_Add,
		[Enum("Model Class-Edit")]
		CSCS02080_Edit,
		[Enum("Model Class-Delete")]
		CSCS02080_Delete,
		#endregion

		#region "Material"
		[Enum("Material-View")]
		CSCS02090_View,
		[Enum("Material-Create")]
		CSCS02090_Add,
		[Enum("Material-Edit")]
		CSCS02090_Edit,
		[Enum("Material-Delete")]
		CSCS02090_Delete,
		#endregion

		#region "Model Type"
		[Enum("Model Type-View")]
		CSCS03010_View,
		[Enum("Model Type-Create")]
		CSCS03010_Add,
		[Enum("Model Type-Edit")]
		CSCS03010_Edit,
		[Enum("Model Type-Delete")]
		CSCS03010_Delete,
		#endregion

		#region "Model Code"
		[Enum("Model Code-View")]
		CSCS03020_View,
		[Enum("Model Code-Create")]
		CSCS03020_Add,
		[Enum("Model Code-Edit")]
		CSCS03020_Edit,
		[Enum("Model Code-Delete")]
		CSCS03020_Delete,
		#endregion

		#region "Symptom"
		[Enum("Symptom-View")]
		CSCS03030_View,
		[Enum("Symptom-Create")]
		CSCS03030_Add,
		[Enum("Symptom-Edit")]
		CSCS03030_Edit,
		[Enum("Symptom-Delete")]
		CSCS03030_Delete,
		#endregion

		#region "Parts Class"
		[Enum("Parts Class-View")]
		CSCP01070_View,
		[Enum("Parts Class-Create")]
		CSCP01070_Add,
		[Enum("Parts Class-Edit")]
		CSCP01070_Edit,
		[Enum("Parts Class-Delete")]
		CSCP01070_Delete,
		#endregion

		#region "Reorder Factor"
		[Enum("Reorder Factor-View")]
		CSCP01080_View,
		[Enum("Reorder Factor-Create")]
		CSCP01080_Add,
		[Enum("Reorder Factor-Edit")]
		CSCP01080_Edit,
		[Enum("Reorder Factor-Delete")]
		CSCP01080_Delete,
		#endregion

		#region "Reorder Factor"
		[Enum("Parts Maintenance-View")]
		CSCP02010_View,
		[Enum("Parts Maintenance-Create")]
		CSCP02010_Add,
		[Enum("Parts Maintenance-Edit")]
		CSCP02010_Edit,
		[Enum("Parts Maintenance-Delete")]
		CSCP02010_Delete,
		[Enum("Parts CreateReplacementItem")]
		CSCP02010_CreateReplacementItem,
		#endregion

		#region "Category Code Maintenance"
		[Enum("Category Code Maintenance-View")]
		CSCP01090_View,
		[Enum("Category Code Maintenance-Create")]
		CSCP01090_Add,
		[Enum("Category Code Maintenance-Edit")]
		CSCP01090_Edit,
		[Enum("Category Code Maintenance-Delete")]
		CSCP01090_Delete,
		#endregion

		#region "Bin Location"
		[Enum("Bin Location-View")]
		CSCP02020_View,
		[Enum("Bin Location-Create")]
		CSCP02020_Add,
		[Enum("Bin Location-Edit")]
		CSCP02020_Edit,
		[Enum("Bin Location-Delete")]
		CSCP02020_Delete,
		#endregion

		#region "Godown Code"
		[Enum("Godown Code-View")]
		CSCP02030_View,
		[Enum("Godown Code-Create")]
		CSCP02030_Add,
		[Enum("Godown Code-Edit")]
		CSCP02030_Edit,
		[Enum("Godown Code-Delete")]
		CSCP02030_Delete,
		#endregion

		#region "Maintenance Policy"
		[Enum("Maintenance Policy-View")]
		CSCS03040_View,
		[Enum("Maintenance Policy-Create")]
		CSCS03040_Add,
		[Enum("Maintenance Policy-Edit")]
		CSCS03040_Edit,
		[Enum("Maintenance Policy-Delete")]
		CSCS03040_Delete,
		#endregion

		#region "Product Information"
		[Enum("Product Information-View")]
		CSCS03050_View,
		[Enum("Product Information-Create")]
		CSCS03050_Add,
		[Enum("Product Information-Edit")]
		CSCS03050_Edit,
		[Enum("Product Information-Delete")]
		CSCS03050_Delete,
		#endregion

		#region "Technician Code"
		[Enum("Technician Code-View")]
		CSCS03060_View,
		[Enum("Technician Code-Create")]
		CSCS03060_Add,
		[Enum("Technician Code-Edit")]
		CSCS03060_Edit,
		[Enum("Technician Code-Delete")]
		CSCS03060_Delete,
		#endregion

		#region "Contractor"
		[Enum("Contractor-View")]
		CSCS03070_View,
		[Enum("Contractor-Create")]
		CSCS03070_Add,
		[Enum("Contractor-Edit")]
		CSCS03070_Edit,
		[Enum("Contractor-Delete")]
		CSCS03070_Delete,
		#endregion

		#region "Quotation"
		[Enum("Quotation-View")]
		CSCS03080_View,
		[Enum("Quotation-Create")]
		CSCS03080_Add,
		[Enum("Quotation-Edit")]
		CSCS03080_Edit,
		[Enum("Quotation-Delete")]
		CSCS03080_Delete,
		#endregion

		#region "Parts Factory Price Maintenance"
		[Enum("Parts Factory Price Maintenance-View")]
		CSCP02040_View,
		[Enum("Parts Factory Price Maintenance-Create")]
		CSCP02040_Add,
		[Enum("Parts Factory Price Maintenance-Edit")]
		CSCP02040_Edit,
		[Enum("Parts Factory Price Maintenance-Delete")]
		CSCP02040_Delete,
		#endregion

		#region "Parts Selling Price Maintenance"
		[Enum("Parts Selling Price Maintenance-View")]
		CSCP02050_View,
		[Enum("Parts Selling Price Maintenance-Create")]
		CSCP02050_Add,
		[Enum("Parts Selling Price Maintenance-Edit")]
		CSCP02050_Edit,
		[Enum("Parts Selling Price Maintenance-Delete")]
		CSCP02050_Delete,
		#endregion

		#region "Purchase Order Maintenance"
		[Enum("Purchase Order Maintenance-View")]
		CSCP02060_View,
		[Enum("Purchase Order Maintenance-Create")]
		CSCP02060_Add,
		[Enum("Purchase Order Maintenance-Edit")]
		CSCP02060_Edit,
		[Enum("Purchase Order Maintenance-Delete")]
		CSCP02060_Delete,
		#endregion

		#region "Job Labour Fee"
		[Enum("Job Labour Fee-View")]
		CSCS03090_View,
		[Enum("Job Labour Fee-Create")]
		CSCS03090_Add,
		[Enum("Job Labour Fee-Edit")]
		CSCS03090_Edit,
		[Enum("Job Labour Fee-Delete")]
		CSCS03090_Delete,
		#endregion

		#region "Product Renewal Letter Printing"
		[Enum("Product Renewal Letter Printing-View")]
		CSCS04010_View,
		[Enum("Product Renewal Letter Printing-Create")]
		CSCS04010_Add,
		[Enum("Product Renewal Letter Printing-Edit")]
		CSCS04010_Edit,
		[Enum("Product Renewal Letter Printing-Delete")]
		CSCS04010_Delete,
		#endregion

		#region "Rejoin Renewal Product Printing"
		[Enum("Rejoin Renewal Product Printing-View")]
		CSCS04020_View,
		[Enum("Rejoin Renewal Product Printing-Create")]
		CSCS04020_Add,
		[Enum("Rejoin Renewal Product Printing-Edit")]
		CSCS04020_Edit,
		[Enum("Rejoin Renewal Product Printing-Delete")]
		CSCS04020_Delete,
		#endregion

		#region "Take Order"
		[Enum("Take Order-View")]
		CSCS04030_View,
		[Enum("Take Order-Create")]
		CSCS04030_Add,
		[Enum("Take Order-Edit")]
		CSCS04030_Edit,
		[Enum("Take Order-Delete")]
		CSCS04030_Delete,
		#endregion

		#region "PO Approval Setup"
		[Enum("PO Approval Setup-View")]
		CSCP02070_View,
		[Enum("PO Approval Setup-Create")]
		CSCP02070_Add,
		[Enum("PO Approval Setup-Edit")]
		CSCP02070_Edit,
		[Enum("PO Approval Setup-Delete")]
		CSCP02070_Delete,
		#endregion

		#region "Assign Purchase Order Approval Rule"
		[Enum("Assign Purchase Order Approval Rule-View")]
		CSCP02080_View,
		[Enum("Assign Purchase Order Approval Rule-Create")]
		CSCP02080_Add,
		[Enum("Assign Purchase Order Approval Rule-Edit")]
		CSCP02080_Edit,
		[Enum("Assign Purchase Order Approval Rule-Delete")]
		CSCP02080_Delete,
		#endregion

		#region "Stock Arrival Maintenance"
		[Enum("Stock Arrival Maintenance-View")]
		CSCP02090_View,
		[Enum("Stock Arrival Maintenance-Create")]
		CSCP02090_Add,
		[Enum("Stock Arrival Maintenance-Edit")]
		CSCP02090_Edit,
		[Enum("Stock Arrival Maintenance-Delete")]
		CSCP02090_Delete,
		#endregion

		#region "Stock Transfer"
		[Enum("Stock Transfer-View")]
		CSCP03010_View,
		[Enum("Stock Transfer-Create")]
		CSCP03010_Add,
		[Enum("Stock Transfer-Edit")]
		CSCP03010_Edit,
		[Enum("Stock Transfer-Delete")]
		CSCP03010_Delete,
		#endregion

		#region "Requisition Note"
		[Enum("Requisition Note-View")]
		CSCP03020_View,
		[Enum("Requisition Note-Create")]
		CSCP03020_Add,
		[Enum("Requisition Note-Edit")]
		CSCP03020_Edit,
		[Enum("Requisition Note-Delete")]
		CSCP03020_Delete,
		#endregion

		#region "Assign Job"
		[Enum("Assign Job-View")]
		CSCS04040_View,
		[Enum("Assign Job-Create")]
		CSCS04040_Add,
		[Enum("Assign Job-Edit")]
		CSCS04040_Edit,
		[Enum("Assign Job-Delete")]
		CSCS04040_Delete,
		#endregion

		#region "Quotation"
		[Enum("Quotation-View")]
		CSCS04050_View,
		[Enum("Quotation-Create")]
		CSCS04050_Add,
		[Enum("Quotation-Edit")]
		CSCS04050_Edit,
		[Enum("Quotation-Delete")]
		CSCS04050_Delete,
		#endregion

		#region "Enter Job Result and Complete-CSCS04060"
		[Enum("Enter Job Result and Complete-View")]
		CSCS04060_View,
		[Enum("Enter Job Result and Complete-Create")]
		CSCS04060_Add,
		[Enum("Enter Job Result and Complete-Edit")]
		CSCS04060_Edit,
		[Enum("Enter Job Result and Complete-Delete")]
		CSCS04060_Delete,
		#endregion


		#region "Stock Adjustment"
		[Enum("Stock Adjustment-View")]
		CSCP03030_View,
		[Enum("Stock Adjustment-Create")]
		CSCP03030_Add,
		[Enum("Stock Adjustment-Edit")]
		CSCP03030_Edit,
		[Enum("Stock Adjustment-Delete")]
		CSCP03030_Delete,
		#endregion

		#region "Sales Memo"
		[Enum("Sales Memo-View")]
		CSCP03040_View,
		[Enum("Sales Memo-Create")]
		CSCP03040_Add,
		[Enum("Sales Memo-Edit")]
		CSCP03040_Edit,
		[Enum("Sales Memo-Delete")]
		CSCP03040_Delete,
		#endregion

		#region "Return To Vendor"
		[Enum("Return To Vendor-View")]
		CSCP03050_View,
		[Enum("Return To Vendor-Create")]
		CSCP03050_Add,
		[Enum("Return To Vendor-Edit")]
		CSCP03050_Edit,
		[Enum("Return To Vendor-Delete")]
		CSCP03050_Delete,
		#endregion

		#region "Stock Arrival Correction"
		[Enum("Stock Arrival Correction-View")]
		CSCP03060_View,
		[Enum("Stock Arrival Correction-Create")]
		CSCP03060_Add,
		[Enum("Stock Arrival Correction-Edit")]
		CSCP03060_Edit,
		[Enum("Stock Arrival Correction-Delete")]
		CSCP03060_Delete,
		#endregion

		#region "Print Job Sheet"
		[Enum("Print Job Sheet-View")]
		CSCS04070_View,
		[Enum("Print Job Sheet-Create")]
		CSCS04070_Add,
		[Enum("Print Job Sheet-Edit")]
		CSCS04070_Edit,
		[Enum("Print Job Sheet-Delete")]
		CSCS04070_Delete,
		#endregion

		#region "Supplier Invoice"
		[Enum("Supplier Invoice-View")]
		CSCP03070_View,
		[Enum("Supplier Invoice-Create")]
		CSCP03070_Add,
		[Enum("Supplier Invoice-Edit")]
		CSCP03070_Edit,
		[Enum("Supplier Invoice-Delete")]
		CSCP03070_Delete,
		#endregion

		#region "GL Account"
		[Enum("GL Account-View")]
		CSCP03080_View,
		[Enum("GL Account-Create")]
		CSCP03080_Add,
		[Enum("GL Account-Edit")]
		CSCP03080_Edit,
		[Enum("GL Account-Delete")]
		CSCP03080_Delete,
		#endregion

		#region "Print Technician Daily Job List"
		[Enum("Print Technician Daily Job List-View")]
		CSCS04080_View,
		[Enum("Print Technician Daily Job List-Create")]
		CSCS04080_Add,
		[Enum("Print Technician Daily Job List-Edit")]
		CSCS04080_Edit,
		[Enum("Print Technician Daily Job List-Delete")]
		CSCS04080_Delete,
		#endregion

		#region "Parts Enquiry"
		[Enum("Parts Enquiry-View")]
		CSCP03090_View,
		[Enum("Parts Enquiry-Create")]
		CSCP03090_Add,
		[Enum("Parts Enquiry-Edit")]
		CSCP03090_Edit,
		[Enum("Parts Enquiry-Delete")]
		CSCP03090_Delete,
		#endregion

		#region "Technician Outstanding Parts Enquiry"
		[Enum("Technician Outstanding Parts Enquiry-View")]
		CSCP04010_View,
		[Enum("Technician Outstanding Parts Enquiry-Create")]
		CSCP04010_Add,
		[Enum("Technician Outstanding Parts Enquiry-Edit")]
		CSCP04010_Edit,
		[Enum("Technician Outstanding Parts Enquiry-Delete")]
		CSCP04010_Delete,
		#endregion

		#region "Requisition Return"
		[Enum("Requisition Return-View")]
		CSCP04020_View,
		[Enum("Requisition Return-Create")]
		CSCP04020_Add,
		[Enum("Requisition Return-Edit")]
		CSCP04020_Edit,
		[Enum("Requisition Return-Delete")]
		CSCP04020_Delete,
		#endregion

		#region "Payment Method"
		[Enum("Payment Method-View")]
		CSCS04090_View,
		[Enum("Payment Method-Create")]
		CSCS04090_Add,
		[Enum("Payment Method-Edit")]
		CSCS04090_Edit,
		[Enum("Payment Method-Delete")]
		CSCS04090_Delete,
		#endregion

		#region "Customer Invoice"
		[Enum("Customer Invoice-View")]
		CSCS05010_View,
		[Enum("Customer Invoice-Create")]
		CSCS05010_Add,
		[Enum("Customer Invoice-Edit")]
		CSCS05010_Edit,
		[Enum("Customer Invoice-Delete")]
		CSCS05010_Delete,
		#endregion

		#region "Sales Return"
		[Enum("Sales Return-View")]
		CSCP04030_View,
		[Enum("Sales Return-Create")]
		CSCP04030_Add,
		[Enum("Sales Return-Edit")]
		CSCP04030_Edit,
		[Enum("Sales Return-Delete")]
		CSCP04030_Delete,
		#endregion

		#region "Stock Movement Enquiry"
		[Enum("Stock Movement Enquiry-View")]
		CSCP04040_View,
		[Enum("Stock Movement Enquiry-Create")]
		CSCP04040_Add,
		[Enum("Stock Movement Enquiry-Edit")]
		CSCP04040_Edit,
		[Enum("Stock Movement Enquiry-Delete")]
		CSCP04040_Delete,
		#endregion

		#region "Issued Requisition Enquiry"
		[Enum("Issued Requisition Enquiry-View")]
		CSCP04050_View,
		[Enum("Issued Requisition Enquiry-Create")]
		CSCP04050_Add,
		[Enum("Issued Requisition Enquiry-Edit")]
		CSCP04050_Edit,
		[Enum("Issued Requisition Enquiry-Delete")]
		CSCP04050_Delete,
		#endregion

		#region "Discontinue Parts Enquiry"
		[Enum("Discontinue Parts Enquiry-View")]
		CSCP04060_View,
		[Enum("Discontinue Parts Enquiry-Create")]
		CSCP04060_Add,
		[Enum("Discontinue Parts Enquiry-Edit")]
		CSCP04060_Edit,
		[Enum("Discontinue Parts Enquiry-Delete")]
		CSCP04060_Delete,
		#endregion

		#region "Parts Details Enquiry"
		[Enum("Parts Details Enquiry-View")]
		CSCP04070_View,
		[Enum("Parts Details Enquiry-Create")]
		CSCP04070_Add,
		[Enum("Parts Details Enquiry-Edit")]
		CSCP04070_Edit,
		[Enum("Parts Details Enquiry-Delete")]
		CSCP04070_Delete,
		#endregion

		#region "Payment Terms"
		[Enum("Payment Terms-View")]
		CSCS05020_View,
		[Enum("Payment Terms-Create")]
		CSCS05020_Add,
		[Enum("Payment Terms-Edit")]
		CSCS05020_Edit,
		[Enum("Payment Terms-Delete")]
		CSCS05020_Delete,
		#endregion

		#region "Adjust Job"
		[Enum("Adjust Job-View")]
		CSCS05030_View,
		[Enum("Adjust Job-Create")]
		CSCS05030_Add,
		[Enum("Adjust Job-Edit")]
		CSCS05030_Edit,
		[Enum("Adjust Job-Delete")]
		CSCS05030_Delete,
		#endregion

		#region "Technician Bonus"
		[Enum("Technician Bonus-View")]
		CSCS05040_View,
		[Enum("Technician Bonus-Create")]
		CSCS05040_Add,
		[Enum("Technician Bonus-Edit")]
		CSCS05040_Edit,
		[Enum("Technician Bonus-Delete")]
		CSCS05040_Delete,
		#endregion

		#region "Job Payment"
		[Enum("Job Payment-View")]
		CSCS05050_View,
		[Enum("Job Payment-Create")]
		CSCS05050_Add,
		[Enum("Job Payment-Edit")]
		CSCS05050_Edit,
		[Enum("Job Payment-Delete")]
		CSCS05050_Delete,
		#endregion

		#region "Invoice Generation"
		[Enum("Invoice Generation-View")]
		CSCS05060_View,
		[Enum("Invoice Generation-Create")]
		CSCS05060_Add,
		[Enum("Invoice Generation-Edit")]
		CSCS05060_Edit,
		[Enum("Invoice Generation-Delete")]
		CSCS05060_Delete,
		#endregion

		#region "Product Upload"
		[Enum("Product Upload-View")]
		CSCS05070_View,
		[Enum("Product Upload-Create")]
		CSCS05070_Add,
		[Enum("Product Upload-Edit")]
		CSCS05070_Edit,
		[Enum("Product Upload-Delete")]
		CSCS05070_Delete,
		#endregion

		#region "Stock Take"
		[Enum("Stock Take-View")]
		CSCP04080_View,
		[Enum("Stock Take-Create")]
		CSCP04080_Add,
		[Enum("Stock Take-Edit")]
		CSCP04080_Edit,
		[Enum("Stock Take-Delete")]
		CSCP04080_Delete,
		#endregion

		#region "Discontinue Letter Printing"
		[Enum("Discontinue Letter Printing-View")]
		CSCS05080_View,
		[Enum("Discontinue Letter Printing-Create")]
		CSCS05080_Add,
		[Enum("Discontinue Letter Printing-Edit")]
		CSCS05080_Edit,
		[Enum("Discontinue Letter Printing-Delete")]
		CSCS05080_Delete,
		#endregion

		#region "DR/CR Note"
		[Enum("DR/CR Note-View")]
		CSCS05090_View,
		[Enum("DR/CR Note-Create")]
		CSCS05090_Add,
		[Enum("DR/CR Note-Edit")]
		CSCS05090_Edit,
		[Enum("DR/CR Note-Delete")]
		CSCS05090_Delete,
		#endregion

		#region "Payment Received"
		[Enum("Payment Received-View")]
		CSCS06010_View,
		[Enum("Payment Received-Create")]
		CSCS06010_Add,
		[Enum("Payment Received-Edit")]
		CSCS06010_Edit,
		[Enum("Payment Received-Delete")]
		CSCS06010_Delete,
		#endregion

		#region "Bad Debt"
		[Enum("Bad Debt-View")]
		CSCS06020_View,
		[Enum("Bad Debt-Create")]
		CSCS06020_Add,
		[Enum("Bad Debt-Edit")]
		CSCS06020_Edit,
		[Enum("Bad Debt-Delete")]
		CSCS06020_Delete,
		#endregion

		#region "Maintenance Policy Printout"
		[Enum("Maintenance Policy Printout-View")]
		CSCS06030_View,
		[Enum("Maintenance Policy Printout-Create")]
		CSCS06030_Add,
		[Enum("Maintenance Policy Printout-Edit")]
		CSCS06030_Edit,
		[Enum("Maintenance Policy Printout-Delete")]
		CSCS06030_Delete,
		#endregion

		#region "Refund Maintenance Agreement"
		[Enum("Refund Maintenance Agreement-View")]
		CSCS06040_View,
		[Enum("Refund Maintenance Agreement-Create")]
		CSCS06040_Add,
		[Enum("Refund Maintenance Agreement-Edit")]
		CSCS06040_Edit,
		[Enum("Refund Maintenance Agreement-Delete")]
		CSCS06040_Delete,
		#endregion

		#region "ID/OD Order"
		[Enum("ID/OD Order-View")]
		CSCS06050_View,
		[Enum("ID/OD Order-Create")]
		CSCS06050_Add,
		[Enum("ID/OD Order-Edit")]
		CSCS06050_Edit,
		[Enum("ID/OD Order-Delete")]
		CSCS06050_Delete,
		#endregion

		#region "Renew Maint"
		[Enum("Renew Maint-View")]
		CSCS06060_View,
		[Enum("Renew Maint-Create")]
		CSCS06060_Add,
		[Enum("Renew Maint-Edit")]
		CSCS06060_Edit,
		[Enum("Renew Maint-Delete")]
		CSCS06060_Delete,
		#endregion

		#region "Print Invoice"
		[Enum("Print Invoice-View")]
		CSCS06070_View,
		[Enum("Print Invoice-Create")]
		CSCS06070_Add,
		[Enum("Print Invoice-Edit")]
		CSCS06070_Edit,
		[Enum("Print Invoice-Delete")]
		CSCS06070_Delete,
		#endregion

		#region "Day End"
		[Enum("Day End-View")]
		CSCS06080_View,
		[Enum("Day End-Create")]
		CSCS06080_Add,
		[Enum("Day End-Edit")]
		CSCS06080_Edit,
		[Enum("Day End-Delete")]
		CSCS06080_Delete,
		#endregion

		#region "Outstanding Job Enquiry"
		[Enum("Outstanding Job Enquiry-View")]
		CSCS06090_View,
		[Enum("Outstanding Job Enquiry-Create")]
		CSCS06090_Add,
		[Enum("Outstanding Job Enquiry-Edit")]
		CSCS06090_Edit,
		[Enum("Outstanding Job Enquiry-Delete")]
		CSCS06090_Delete,
		#endregion

		#region "Job Waiting Parts Enquiry"
		[Enum("Job Waiting Parts Enquiry-View")]
		CSCS07010_View,
		[Enum("Job Waiting Parts Enquiry-Create")]
		CSCS07010_Add,
		[Enum("Job Waiting Parts Enquiry-Edit")]
		CSCS07010_Edit,
		[Enum("Job Waiting Parts Enquiry-Delete")]
		CSCS07010_Delete,
		#endregion

		#region "Job Important Field Approval"
		[Enum("Job Important Field Approval-View")]
		CSCS07030_View,
		[Enum("Job Important Field Approval-Create")]
		CSCS07030_Add,
		[Enum("Job Important Field Approval-Edit")]
		CSCS07030_Edit,
		[Enum("Job Important Field Approval-Delete")]
		CSCS07030_Delete,
		#endregion

		#region "Job Payment Enquiry"
		[Enum("Job Payment Enquiry-View")]
		CSCS07020_View,
		[Enum("Job Payment Enquiry-Create")]
		CSCS07020_Add,
		[Enum("Job Payment Enquiry-Edit")]
		CSCS07020_Edit,
		[Enum("Job Payment Enquiry-Delete")]
		CSCS07020_Delete,
		#endregion

		#region "Stock Take Print Variant"
		[Enum("Stock Take Print Variant-View")]
		CSCP04081_View,
		#endregion

		#region "Stock Take Print All"
		[Enum("Stock Take Print All-View")]
		CSCP04082_View,
		#endregion

		#region "Stock Take Consolidate Report"
		[Enum("Stock Take Consolidate Report-View")]
		CSCP04083_View,
		#endregion

		#region "Stock Take Discrepancy Report"
		[Enum("Stock Take Discrepancy Report-View")]
		CSCP04084_View,
		#endregion

		#region "Generate Invoice For Sales Memo"
		[Enum("Generate Invoice For Sales Memo-View")]
		CSCS07040_View,
		[Enum("Generate Invoice For Sales Memo-Create")]
		CSCS07040_Add,
		[Enum("Generate Invoice For Sales Memo-Edit")]
		CSCS07040_Edit,
		[Enum("Generate Invoice For Sales Memo-Delete")]
		CSCS07040_Delete,
		#endregion

		#region "Job Payment Consolidate Report"
		[Enum("Job Payment Consolidate Report-View")]
		CSCS07050_View,
		#endregion

		#region "Adjust Job Create Refund"
		[Enum("Adjust Job Create Refund-View")]
		CSCS05031_View,
		[Enum("Adjust Job Create Refund-Create")]
		CSCS05031_Add,
		[Enum("Adjust Job Create Refund-Edit")]
		CSCS05031_Edit,
		[Enum("Adjust Job Create Refund-Delete")]
		CSCS05031_Delete,
		#endregion

		#region "AP Charges"
		[Enum("AP Charges-View")]
		CSCP04090_View,
		[Enum("AP Charges-Create")]
		CSCP04090_Add,
		[Enum("AP Charges-Edit")]
		CSCP04090_Edit,
		[Enum("AP Charges-Delete")]
		CSCP04090_Delete,
		#endregion

		#region "Technician Group"
		[Enum("Technician Group-View")]
		CSCS07060_View,
		[Enum("Technician Group-Create")]
		CSCS07060_Add,
		[Enum("Technician Group-Edit")]
		CSCS07060_Edit,
		[Enum("Technician Group-Delete")]
		CSCS07060_Delete,
		#endregion

		#region "Upload EAD Product"
		[Enum("Upload EAD Product-View")]
		CSCS07070_View,
		[Enum("Upload EAD Product-Create")]
		CSCS07070_Add,
		[Enum("Upload EAD Product-Edit")]
		CSCS07070_Edit,
		[Enum("Upload EAD Product-Delete")]
		CSCS07070_Delete,
		#endregion

		#region "Override Job Result-CSCS07080"
		[Enum("Override Job Result-View")]
		CSCS07080_View,
		[Enum("Override Job Result-Create")]
		CSCS07080_Add,
		[Enum("Override Job Result-Edit")]
		CSCS07080_Edit,
		[Enum("Override Job Result-Delete")]
		CSCS07080_Delete,
		#endregion

		#region "Reports"
		[Enum("Purchase Order-View")]
		CSCPR00010_View,
		[Enum("Purchase Order-Edit")]
		CSCPR00010_Edit,
		[Enum("Purchase Order Report-View")]
		CSCPR01010_View,
		[Enum("Purchase Order Report-Edit")]
		CSCPR01010_Edit,
		[Enum("Purchase Receiving Correction Report-View")]
		CSCPR01020_View,
		[Enum("Purchase Receiving Correction Report-Edit")]
		CSCPR01020_Edit,
		[Enum("Purchase Order Suggest List Report-View")]
		CSCPR01030_View,
		[Enum("Purchase Order Suggest List Report-Edit")]
		CSCPR01030_Edit,
		[Enum("Purchase Analysis Report-View")]
		CSCPR01040_View,
		[Enum("Purchase Analysis Report-Edit")]
		CSCPR01040_Edit,
		[Enum("Counter Sales Report-View")]
		CSCPR01050_View,
		[Enum("Counter Sales Report-Edit")]
		CSCPR01050_Edit,
		[Enum("Counter Sales Return Report-View")]
		CSCPR01060_View,
		[Enum("Counter Sales Return Report-Edit")]
		CSCPR01060_Edit,
		[Enum("Outstanding Parts By Technician Report-View")]
		CSCPR01070_View,
		[Enum("Outstanding Parts By Technician Report-Edit")]
		CSCPR01070_Edit,
		[Enum("Outstanding Parts History By Technician Report-View")]
		CSCPR01080_View,
		[Enum("Outstanding Parts History By Technician Report-Edit")]
		CSCPR01080_Edit,
		[Enum("Technician Parts Movement Report-View")]
		CSCPR01090_View,
		[Enum("Technician Parts Movement Report-Edit")]
		CSCPR01090_Edit,
		[Enum("Stock Movement Report-View")]
		CSCPR02010_View,
		[Enum("Stock Movement Report-Edit")]
		CSCPR02010_Edit,
		[Enum("TStock Adjustment Transaction Report-View")]
		CSCPR02020_View,
		[Enum("TStock Adjustment Transaction Report-Edit")]
		CSCPR02020_Edit,
		[Enum("Stock Value Report-View")]
		CSCPR02030_View,
		[Enum("Stock Value Report-Edit")]
		CSCPR02030_Edit,
		[Enum("Stock Aging By Brand By Type Report-View")]
		CSCPR02040_View,
		[Enum("Stock Aging By Brand By Type Report-Edit")]
		CSCPR02040_Edit,
		[Enum("Stock Aging By Brand Report-View")]
		CSCPR02050_View,
		[Enum("Stock Aging By Brand Report-Edit")]
		CSCPR02050_Edit,
		[Enum("Stock Aging By Brand By Parts Report-View")]
		CSCPR02060_View,
		[Enum("Stock Aging By Brand By Parts Report-Edit")]
		CSCPR02060_Edit,
		[Enum("Stock Take Report-View")]
		CSCPR02070_View,
		[Enum("Stock Take Report-Edit")]
		CSCPR02070_Edit,
		[Enum("Stock Take Discrepancy Report-View")]
		CSCPR02080_View,
		[Enum("Stock Take Discrepancy Report-Edit")]
		CSCPR02080_Edit,
		[Enum("Wait Order Stock Arrival Alert List-View")]
		CSCPR02090_View,
		[Enum("Wait Order Stock Arrival Alert List-Edit")]
		CSCPR02090_Edit,
		[Enum("Parts Sales Analysis Report-View")]
		CSCPR03010_View,
		[Enum("Parts Sales Analysis Report-Edit")]
		CSCPR03010_Edit,
		[Enum("Stock Transfer Note-View")]
		CSCPR03020_View,
		[Enum("Stock Transfer Note-Edit")]
		CSCPR03020_Edit,
		[Enum("AP Invoice Report-View")]
		CSCPR03030_View,
		[Enum("AP Invoice Report-Edit")]
		CSCPR03030_Edit,
		[Enum("Material Account Distribution Detail-View")]
		CSCPR03040_View,
		[Enum("Material Account Distribution Detail-Edit")]
		CSCPR03040_Edit,
		[Enum("Material Account Distribution Summary-View")]
		CSCPR03050_View,
		[Enum("Material Account Distribution Summary-Edit")]
		CSCPR03050_Edit,
		[Enum("Daily Cost Consumption Summary Report-View")]
		CSCPR03060_View,
		[Enum("Daily Cost Consumption Summary Report-Edit")]
		CSCPR03060_Edit,
		[Enum("Daily Job Pick Slip Report-View")]
		CSCPR03070_View,
		[Enum("Daily Job Pick Slip Report-Edit")]
		CSCPR03070_Edit,
		[Enum("Daily Job Report-View")]
		CSCPR03080_View,
		[Enum("Daily Job Report-Edit")]
		CSCPR03080_Edit,
		[Enum("Technician Daily Job List-View")]
		CSCPR03090_View,
		[Enum("Technician Daily Job List-Edit")]
		CSCPR03090_Edit,
		[Enum("Outstanding Job Alert Report By Technician-View")]
		CSCPR04010_View,
		[Enum("Outstanding Job Alert Report By Technician-Edit")]
		CSCPR04010_Edit,
		[Enum("ID OD Check List-View")]
		CSCPR04020_View,
		[Enum("ID OD Check List-Edit")]
		CSCPR04020_Edit,
		[Enum("Daily Received Transaction Report-View")]
		CSCPR04030_View,
		[Enum("Daily Received Transaction Report-Edit")]
		CSCPR04030_Edit,
		[Enum("Debtor Invoice Report-View")]
		CSCPR04040_View,
		[Enum("Debtor Invoice Report-Edit")]
		CSCPR04040_Edit,
		[Enum("AR Settlement History Report-View")]
		CSCPR04050_View,
		[Enum("AR Settlement History Report-Edit")]
		CSCPR04050_Edit,
		[Enum("Debtor Aging Report-View")]
		CSCPR04060_View,
		[Enum("Debtor Aging Report-Edit")]
		CSCPR04060_Edit,
		[Enum("Sales Analysis By Maint Type-View")]
		CSCPR04070_View,
		[Enum("Sales Analysis By Maint Type-Edit")]
		CSCPR04070_Edit,
		[Enum("Sales Analysis By Product Aging-View")]
		CSCPR04080_View,
		[Enum("Sales Analysis By Product Aging-Edit")]
		CSCPR04080_Edit,
		[Enum("Renew Analysis By Aging Report-View")]
		CSCPR04090_View,
		[Enum("Renew Analysis By Aging Report-Edit")]
		CSCPR04090_Edit,
		[Enum("Product Information Report-View")]
		CSCPR05010_View,
		[Enum("Product Information Report-Edit")]
		CSCPR05010_Edit,
		[Enum("Job Details Information Report-View")]
		CSCPR05020_View,
		[Enum("Job Details Information Report-Edit")]
		CSCPR05020_Edit,
		[Enum("Job Analysis For Non DCH Brand-View")]
		CSCPR05030_View,
		[Enum("Job Analysis For Non DCH Brand-Edit")]
		CSCPR05030_Edit,
		[Enum("Repair Symptom Analysis Report-View")]
		CSCPR05040_View,
		[Enum("Repair Symptom Analysis Report-Edit")]
		CSCPR05040_Edit,
		[Enum("Technician Performance Report-View")]
		CSCPR05050_View,
		[Enum("Technician Performance Report-Edit")]
		CSCPR05050_Edit,
		[Enum("Technician Performance By Product Report-View")]
		CSCPR05060_View,
		[Enum("Technician Performance By Product Report-Edit")]
		CSCPR05060_Edit,
		[Enum("District Distribution Report-View")]
		CSCPR05070_View,
		[Enum("District Distribution Report-Edit")]
		CSCPR05070_Edit,
		[Enum("Model Information Report-View")]
		CSCPR05080_View,
		[Enum("Model Information Report-Edit")]
		CSCPR05080_Edit,
		[Enum("Accounting Movement Transaction Report-View")]
		CSCPR05090_View,
		[Enum("Accounting Movement Transaction Report-Edit")]
		CSCPR05090_Edit,
		[Enum("Daily Sales Summary Report-View")]
		CSCPR06010_View,
		[Enum("Daily Sales Summary Report-Edit")]
		CSCPR06010_Edit,
		[Enum("Inventory Distribution Report-View")]
		CSCPR06020_View,
		[Enum("Inventory Distribution Report-Edit")]
		CSCPR06020_Edit,
		[Enum("Posted Journals-View")]
		CSCPR06030_View,
		[Enum("Posted Journals-Edit")]
		CSCPR06030_Edit,
		[Enum("Parts Selling Price Audit Report-View")]
		CSCPR06040_View,
		[Enum("Parts Selling Price Audit Report-Edit")]
		CSCPR06040_Edit,
		[Enum("Parts Selling Price Report-View")]
		CSCPR06050_View,
		[Enum("Parts Selling Price Report-Edit")]
		CSCPR06050_Edit,
		[Enum("Maintenance Price Report-View")]
		CSCPR06060_View,
		[Enum("Maintenance Price Report-Edit")]
		CSCPR06060_Edit,
		[Enum("Maintenance Price Audit Report-View")]
		CSCPR06070_View,
		[Enum("Maintenance Price Audit Report-Edit")]
		CSCPR06070_Edit,
		[Enum("Product Information Audit Report-View")]
		CSCPR06080_View,
		[Enum("Product Information Audit Report-Edit")]
		CSCPR06080_Edit,
		[Enum("AR Voucher Report-View")]
		CSCPR06090_View,
		[Enum("AR Voucher Report-Edit")]
		CSCPR06090_Edit,
		[Enum("Technician Incentive Bonus Report-View")]
		CSCPR07010_View,
		[Enum("Technician Incentive Bonus Report-Edit")]
		CSCPR07010_Edit,
		[Enum("Customer Information Audit Report-View")]
		CSCPR07020_View,
		[Enum("Customer Information Audit Report-Edit")]
		CSCPR07020_Edit,
		[Enum("Parts Selling Price VS Cost Audit Report-View")]
		CSCPR07030_View,
		[Enum("Parts Selling Price VS Cost Audit Report-Edit")]
		CSCPR07030_Edit,
		[Enum("Stock Take Skip Parts Report-View")]
		CSCPR07040_View,
		[Enum("Stock Take Skip Parts Report-Edit")]
		CSCPR07040_Edit,
		[Enum("Stock Arrival Report-View")]
		CSCPR07050_View,
		[Enum("Stock Arrival Report-Edit")]
		CSCPR07050_Edit,
		[Enum("Daily Physical Stock OnHand Report-View")]
		CSCPR07080_View,
		[Enum("Daily Physical Stock OnHand Report-Edit")]
		CSCPR07080_Edit,
		[Enum("Daily Job Allocation List-View")]
		CSCPR07090_View,
		[Enum("Daily Job Allocation List-Edit")]
		CSCPR07090_Edit,
		[Enum("User List Report-View")]
		CSCPR08010_View,
		[Enum("User List Report-Edit")]
		CSCPR08010_Edit,
		[Enum("Function List Report-View")]
		CSCPR08020_View,
		[Enum("Function List Report-Edit")]
		CSCPR08020_Edit,


		#region "Job Queue"
		[Enum("Job Queue-View")]
		CSCPR07070_View,
		[Enum("Job Queue-Create")]
		CSCPR07070_Add,
		[Enum("Job Queue-Edit")]
		CSCPR07070_Edit,
		[Enum("Job Queue-Delete")]
		CSCPR07070_Delete
		#endregion
		#endregion

		
    }
}
