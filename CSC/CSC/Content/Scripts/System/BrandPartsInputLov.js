; (function ($) {
	$.InputLov=function(opts)
	{
		return installInputLov(opts);
	}

	$.InputLov.defaults={
		ID_IDStr:			undefined,
		Code_IDStr:			undefined,
		Desc_IDStr:			undefined,
		Desc_TxtIDStr:		undefined,
		imgSelect_IDStr:	undefined,

		ID_Obj:				undefined,
		Code_Obj:			undefined,
		Desc_Obj:			undefined,
		Desc_TxtIDObj: 		undefined,
		imgSelect_Obj:		undefined,
		 
		LovUrl:		"",		//../Demo/DemoLov
		AItemUrl:	"",		//../Demo/GetItem
		codeOrdsc:	"codeOrdsc",
		RefID1:		"",		//
		RefModle:	"0",	//1等于时，RefID1无效，当等于0时，根据RefID1查询。
		isLoad:		false,
		oldValue:	"AA",		//保存上次的值
		//子项
		ChildItemID:		undefined,
		ChildItemCode:		undefined,
		ChildItemDesc:		undefined,

		ChildItemCodeOBJ:	undefined,

		LovTitle:			"",

		//禁用处理。
		//onNullDisable:		false,
		ChildDisable:		false
	}; 

	function installInputLov(options)
	{
		var opts = $.extend({}, $.InputLov.defaults, options || {});

        if(!opts.LovWid)
            opts.LovWid = 650;

        if(!opts.LovHig)
            opts.LovHig = 450;

		//button Click
		if(opts.Desc_IDStr)
		{
			opts.Desc_Obj=$("#"+opts.Desc_IDStr);
		}
		if(opts.Desc_TxtIDStr)
		{
			opts.Desc_TxtIDObj=$("#"+opts.Desc_TxtIDStr);
		}
		opts.ID_Obj=$("#"+opts.ID_IDStr);
		opts.Code_Obj=$("#"+opts.Code_IDStr);

		opts.imgSelect_Obj=$("#"+opts.imgSelect_IDStr);

		if(opts.ChildItemCode)
		{
			opts.ChildItemCodeOBJ=$("#"+opts.ChildItemCode);
		}

		opts.imgSelect_Obj.click(function(){
			var Code = opts.Code_Obj.val();
			if (Code == "")
			{
				ClearValues(opts);
				//return;
			}
			opts.isLoad = true;
			if (GetCodeDesc(opts))
			{
				return;
			}
		});

		//ID CHANGE 
		opts.ID_Obj.change(function(){
			if(opts.ID_Obj.val()=="" || opts.Code_Obj.val()=="")
			{
				if(opts.childItemID)
				{
					if(opts.Desc_Obj){
						opts.Desc_Obj.html("");
					}
					if(opts.Desc_TxtIDObj)//set desc
					{
						opts.Desc_TxtIDObj.val("");
					}
					$("#"+opts.childItemID).val("");
					//$.dropdown.ClearValue({id: opts.childItemID});
					
					if(opts.ChildItemCodeOBJ)
					{
						 opts.ChildItemCodeOBJ.val("");
					}
					if(opts.ChildItemDesc  && $("#"+opts.ChildItemDesc))
					{
						$("#"+opts.ChildItemDesc).html("");
					}
					$("#"+opts.childItemID).change();
				}
			}
		});

		if(opts.ChildDisable)
		{
			opts.ID_Obj.change(function(){
				if(opts.ID_Obj.val()=="")
				{
					opts.ChildItemCodeOBJ.attr("disabled", "disabled");
					opts.ChildItemCodeOBJ.addClass("label_readonly");
				}
				else
				{
					opts.ChildItemCodeOBJ.attr("disabled", "");
					opts.ChildItemCodeOBJ.removeClass("label_readonly");
				}
			});
			if(opts.ID_Obj.val()=="")
			{
				opts.ChildItemCodeOBJ.addClass("label_readonly");
				opts.ChildItemCodeOBJ.attr("disabled", "disabled");
			}
		}

		//CodeObj
		opts.Code_Obj.blur(function () {
			var Code = opts.Code_Obj.val();
			if (Code == "")
			{
				ClearValues(opts);
				return;
			}
			if (opts.isLoad)
				return;
			if (GetCodeDesc(opts))
				return;
		});

		opts.Code_Obj.keydown(function (e) {
			var code = (e.keyCode ? e.keyCode : e.which);
			opts.isLoad = false;
			IsClickChange=true;

			if (code == 13) {
				opts.isLoad = true;
				GetCodeDesc(opts);
				e.preventDefault();
                e.stopPropagation();
			}			
		}); //end key down

	}//end installInputLov

	//LovUrl: ""	//../Demo/DemoLov
	function ClearValues(opts)
	{
		opts.ID_Obj.val("");
		opts.ID_Obj.change();
		if(opts.Code_Obj)
		{
			opts.Code_Obj.val("");
		}
		if(opts.Desc_Obj)//set desc
		{
			opts.Desc_Obj.html("");
		}
		if(opts.Desc_TxtIDObj)//set desc
		{
			opts.Desc_TxtIDObj.val("");
		}
	}
	function LovSet(opts) {
		var Code = opts.Code_Obj.val();
 
		
		 if(opts.oldValue==Code)
		 {
			ClearValues(opts);
			return "";
		 }
		 opts.oldValue= Code;

		var RefID1="";
		if(opts.RefID1)
		{
			RefID1=$("#"+ opts.RefID1).val();
		}

		var murl = ''; //opts.LovUrl + '?notIsMultipleChoose=ture&Code=' + Code + "&RefID1=" + RefID1;
        if(opts.LovUrl.indexOf("?") >= 0)
        {
            murl = opts.LovUrl + '&notIsMultipleChoose=ture&Code=' + Code+ "&RefModle=" + opts.RefModle + "&RefID1=" + RefID1 ;
        }
        else
        {
            murl = opts.LovUrl + '?notIsMultipleChoose=ture&Code=' + Code + "&RefModle=" + opts.RefModle + "&RefID1=" + RefID1 ;
        }
		bindLovClick(null, murl, opts.LovWid, opts.LovHig, opts.ID_IDStr, opts.codeOrdsc , function (values, codesOrDscs) {
			var valArr = values.split(";");
			 
			if (valArr.length > 0) {
				values = valArr[valArr.length - 1];
				opts.ID_Obj.val(values);
				opts.oldValue = valArr[0];				
			}
			else {
				opts.ID_Obj.val(values);
			}
			
			codesOrDscs = codesOrDscs.replace(opts.oldValue + ";", "");
			if (codesOrDscs && codesOrDscs.length > 0) {
				var Arr = codesOrDscs.split("#");
				if (Arr.length = 2) {
					if(opts.Desc_Obj)//set desc
					{
						opts.Desc_Obj.html(Arr[1]);
					}
					opts.Code_Obj.val(Arr[0]);
					opts.oldValue=Arr[0];//change old value
					if(opts.Desc_TxtIDObj)
					{
						opts.Desc_TxtIDObj.val(Arr[1]);
					}
				}
			} else {
                ClearValues(opts);
            }
			//change
			opts.ID_Obj.change();
		}, opts.LovTitle);//end  bindLovClick
		ClearValues(opts);
	}

	function GetCodeDesc(opts) {
		var PrevCode =  opts.Code_Obj.val();
		var result = false;
		var strRefID1="";
		if(opts.RefID1)
		{
			strRefID1=$("#"+ opts.RefID1).val();
		}
		
		if(PrevCode !="")
		{
			if(opts.oldValue==PrevCode && opts.ID_Obj.val())
			{
				return;
			}//+ "&RefModle=" + opts.RefModle
			$.ajax({
				type: 'GET',
				url: opts.AItemUrl,		//'../Demo/GetAItem',
				dataType: "json",
				data: { Code: PrevCode ,RefID1: strRefID1,RefModle: opts.RefModle, _t: Math.random() },
				success: function (res) {
					if (res) {
						if(opts.Desc_Obj)//set desc
						{
							opts.Desc_Obj.html(res.Result.InterValue);
						}
						if(opts.Desc_TxtIDObj)
						{
							opts.Desc_TxtIDObj.val(res.Result.InterValue);
						}
						
						opts.Code_Obj.val(res.Result.Text);
						opts.oldValue=res.Result.Text;//change old
						opts.ID_Obj.val(res.Result.Value);
						opts.ID_Obj.change();
						result = true;
					}
					else { 
						LovSet(opts);
					}
				},
				error: function (xhr) {
                    ￿//alert(xhr.responseText);
				}
			}); //end ajax

		}
		else
		{
			LovSet(opts);
		}
		return result;
	}

})(jQuery);




