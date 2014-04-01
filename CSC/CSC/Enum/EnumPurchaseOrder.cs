using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Web;

namespace CSC  
{
	/// <summary>
	/// P/O Type Enum
	/// </summary>
	public enum POTypeEnum
	{
		[Enum("Air")]
		A,
		[Enum("Local")]
		L,
		[Enum("Sea")]
		S,
		[Enum("Parcel")]
		P,
		[Enum("Emergency")]
		E
	}

	/// <summary>
	/// P/O Status Enum
	/// </summary>
	public enum POStatusEnum
	{
		[Enum("Incomplete")]
		I,
		[Enum("Wait for Approval")]
		W,
		[Enum("Approved")]
		A,
		[Enum("Require for Reapproval")]
		R,
		//[Enum("Deny")]
		//D,
		[Enum("Closed")]
		C,
		//[Enum("Reject")]
		//J,
		[Enum("Cancelled")]
		X
	}

	/// <summary>
	/// PO Line Status
	/// </summary>
	public enum POLineStatusEnum
	{
		[Enum("Open")]
		O,
		[Enum("Close")]
		C,
		[Enum("Cancel")]
		X
	}

	public enum PoReceiptNewStatus
	{
		[Enum("Open")]
		OPEN,
		[Enum("Confirmed")]
		CONFIRMED,
		[Enum("Closed")]
		CLOSED
	}

	public enum PoReceiptReturnStatus
	{
		[Enum("Open")]
		OPEN,
		[Enum("Confirmed")]
		CONFIRMED
	}

	/// <summary>
	/// 有可能过期了
	/// </summary>
	public enum POReceiptStatusEnum
	{
		[Enum("Open")]
		O,
		[Enum("Close")]
		C,
		[Enum("Confirmed")]
		A,
		[Enum("Cancelled")]
		X
	}

	/// <summary>
	/// P/O Inter Grp Dept
	/// </summary>
	public enum POInterGrpDeptEnum
	{
		[Enum("Group")]
		G,
		[Enum("Department")]
		D
	}



}