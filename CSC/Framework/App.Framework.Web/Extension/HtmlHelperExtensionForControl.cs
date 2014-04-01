using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;
using System.Text;
using System.Reflection;

namespace App.Framework.Web
{
	/// <summary>
	///扩展 Html 控件
	/// </summary>
	public static class HtmlControlsX
	{

		/// <summary>
		/// 图片
		/// </summary>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="src">链接地址</param>
		/// <param name="alt">提示</param>
		/// <param name="htmlAttributes">IDictionary</param>
		/// <returns>string</returns>
		public static void Image(this HtmlHelper helper, string src, string alt = null, object htmlAttributes = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
			TagBuilder tbImage = new TagBuilder("img");

			tbImage.MergeAttribute("src", src);
			tbImage.MergeAttribute("alt", alt);
			if (htmlAttributes != null)
			{
				tbImage.MergeAttributes<string, object>(new RouteValueDictionary(htmlAttributes));
			}

			writer.InnerWriter.Write(tbImage.ToString(TagRenderMode.SelfClosing));
		}

		#region Calendar

		/// <summary>
		/// 日历
		/// </summary>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="name">name</param>
		/// <param name="value">value</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="calendarParams">日历附加属性</param>
		public static void Calendar(this HtmlHelper helper, string name, object value = null, object htmlAttributes = null, CalendarParams calendarParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);
			var dateFormat = App.Framework.Web.ConfigHelper.DateTimeFormat;
			calendarParams = calendarParams ?? new CalendarParams();
			dateFormat = dateFormat ?? calendarParams.Format;


			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}

			if (calendarParams != null)
			{
				rvd.Add("options", calendarParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " calendar";
			}
			else
			{
				rvd.Add("class", "calendar");
			}
			rvd.Add("autocomplete", "off");

			DateTime dtValue;
			if (value != null)
			{
				if (DateTime.TryParse(value.ToString(), out dtValue))
				{
					value = dtValue.ToString(dateFormat);
				}
			}

			writer.InnerWriter.Write(helper.TextBox(name, value, rvd));
		}
		/// <summary>
		/// 日历
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="calendarParams">日历附加属性</param>
		public static MvcHtmlString CalendarFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, CalendarParams calendarParams = null)
		{
			string dateFormat = App.Framework.Web.ConfigHelper.DateTimeFormat;//= helper.ViewData["DateFormat"] as string;
			calendarParams = calendarParams ?? new CalendarParams();
			dateFormat = dateFormat ?? calendarParams.Format;
			calendarParams.Format = dateFormat;


			string name = ExpressionHelper.GetExpressionText(expression);
			string value = string.Empty;
			ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
			if (metaData.ModelType != typeof(DateTime) && metaData.ModelType != typeof(DateTime?))
			{
				throw new Exception("日历控件只支持 Datetime 或 Datetime? 类型");
			}

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (calendarParams != null)
			{
				rvd.Add("options", calendarParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " calendar";
			}
			else
			{
				rvd.Add("class", "calendar");
			}
			rvd.Add("autocomplete", "off");

			if (metaData.ModelType == typeof(DateTime))
			{
				value = metaData.Model == null ? string.Empty : ((DateTime)metaData.Model).ToString(dateFormat);
			}
			else if (metaData.ModelType == typeof(DateTime?))
			{
				DateTime? dt = (DateTime?)metaData.Model;
				value = dt.HasValue ? dt.Value.ToString(dateFormat) : string.Empty;
			}

			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttributes(rvd);
			tagBuilder.MergeAttribute("type", "text");
			tagBuilder.MergeAttribute("name", name, false);
			tagBuilder.MergeAttribute("value", value, true);
			tagBuilder.MergeAttribute("id", !string.IsNullOrEmpty(name)
				? name.Replace(".", HtmlHelper.IdAttributeDotReplacement)
				: string.Empty, false);

			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
		}


		/// <summary>
		/// 日历 默认为当天
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="calendarParams">日历附加属性</param>
		public static MvcHtmlString CalendarForDefault<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, CalendarParams calendarParams = null)
		{
			string dateFormat = App.Framework.Web.ConfigHelper.DateTimeFormat;// helper.ViewData["DateFormat"] as string;
			calendarParams = calendarParams ?? new CalendarParams();
			dateFormat = dateFormat ?? calendarParams.Format;


			string name = ExpressionHelper.GetExpressionText(expression);
			string value = string.Empty;
			ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
			if (metaData.ModelType != typeof(DateTime) && metaData.ModelType != typeof(DateTime?))
			{
				throw new Exception("日历控件只支持 Datetime 或 Datetime? 类型");
			}

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (calendarParams != null)
			{
				rvd.Add("options", calendarParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " calendar";
			}
			else
			{
				rvd.Add("class", "calendar");
			}
			rvd.Add("autocomplete", "off");

			if (metaData.ModelType == typeof(DateTime))
			{
				value = metaData.Model == null ? DateTime.Now.ToString(dateFormat) : ((DateTime)metaData.Model).ToString(dateFormat);
			}
			else if (metaData.ModelType == typeof(DateTime?))
			{
				DateTime? dt = (DateTime?)metaData.Model;
				value = dt.HasValue ? dt.Value.ToString(dateFormat) : DateTime.Now.ToString(dateFormat);
			}

			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttributes(rvd);
			tagBuilder.MergeAttribute("type", "text");
			tagBuilder.MergeAttribute("name", name, false);
			tagBuilder.MergeAttribute("value", value, true);
			tagBuilder.MergeAttribute("id", !string.IsNullOrEmpty(name)
				? name.Replace(".", HtmlHelper.IdAttributeDotReplacement)
				: string.Empty, false);

			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
		}
		#endregion

		#region NumericTextbox

		/// <summary>
		/// 数字输入框
		/// </summary>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="name">name</param>
		/// <param name="value">value</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="numericParams">附加属性</param>
		public static void NumericTextbox(this HtmlHelper helper, string name, object value = null, object htmlAttributes = null, NumericParams numericParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (numericParams != null)
			{
				rvd.Add("options", numericParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " numeric";
			}
			else
			{
				rvd.Add("class", "numeric");
			}

			writer.InnerWriter.Write(helper.TextBox(name, value, rvd));
		}

		/// <summary>
		/// 数字输入框
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression</param>
		/// <param name="htmlAttributes">html 属性</param>
		public static void NumericTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, NumericParams numericParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (numericParams != null)
			{
				rvd.Add("options", numericParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " numeric";
			}
			else
			{
				rvd.Add("class", "numeric");
			}

			writer.InnerWriter.Write(helper.TextBoxFor(expression, rvd));
		}


		/// <summary>
		/// 带小数点的数字输入框
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression</param>
		/// <param name="htmlAttributes">html 属性</param>
		public static void DecimalNumericTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, NumericParams numericParams = null)
		{
			/**
			 * Added by JLD on 2012-11-27
			 * 之前的NumericTextboxFor方法其实是可以重构
			 * 但为了不改变原有的写法 继而新增
			 * 主要是解决数字输入框输入带有小数点的数字 在失去交点时 数字框文本内容不会自动改变 保持原有的值
			 **/

			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (numericParams != null)
			{
				rvd.Add("options", numericParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " decimal_numeric";
			}
			else
			{
				rvd.Add("class", "decimal_numeric");
			}

			writer.InnerWriter.Write(helper.TextBoxFor(expression, rvd));
		}

		#endregion

		#region Combobox

		/// <summary>
		/// 下拉框
		/// </summary>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="name">name</param>
		/// <param name="selectList">SelectListItem</param>
		/// <param name="optionLabel">第一项, 值默认为空, 可为 null</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="comboboxParams">附加属性</param>
		public static void Combobox(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, string optionLabel = null, object htmlAttributes = null, ComboboxParams comboboxParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (comboboxParams != null)
			{
				rvd.Add("options", comboboxParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " combobox";
			}
			else
			{
				rvd.Add("class", "combobox");
			}

			writer.InnerWriter.Write(helper.DropDownList(name, selectList, optionLabel, rvd));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression</param>
		/// <param name="selectList">SelectListItem</param>
		/// <param name="optionLabel">第一项, 值默认为空, 可为 null</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="comboboxParams">附加属性</param>
		public static void ComboboxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel = null, object htmlAttributes = null, ComboboxParams comboboxParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (comboboxParams != null)
			{
				rvd.Add("options", comboboxParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " combobox";
			}
			else
			{
				rvd.Add("class", "combobox");
			}

			writer.InnerWriter.Write(helper.DropDownListFor(expression, selectList, optionLabel, rvd));
		}

		#endregion

		#region LOV

		/// <summary>
		/// LOV
		/// </summary>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="name">name</param>
		/// <param name="value">value</param>
		/// <param name="hiddenValue">隐藏域值, 对 lov 来说就是 id</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="lovParams">附加属性</param>
		public static void LOV(this HtmlHelper helper, string name, object value = null, object hiddenValue = null, object htmlAttributes = null, LOVParams lovParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (lovParams != null)
			{
				rvd.Add("options", lovParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " lov";
			}
			else
			{
				rvd.Add("class", "lov");
			}

			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "hid_lov");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, name + "_1");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, name + "_1");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			writer.InnerWriter.Write(helper.TextBox(name, value, rvd));
		}
		/// <summary>
		/// LOV
		/// </summary>
		/// <typeparam name="TModel">Model</typeparam>
		/// <typeparam name="TProperty">Property name</typeparam>
		/// <typeparam name="TProperty1">Property id</typeparam>
		/// <param name="helper">HtmlHelper</param>
		/// <param name="expression">Expression name</param>
		/// <param name="hiddenExpression">Expression id</param>
		/// <param name="htmlAttributes">html 属性</param>
		/// <param name="calendarParams">附加属性</param>
		public static void LOVFor<TModel, TProperty, TProperty1>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, Expression<Func<TModel, TProperty1>> hiddenExpression, object htmlAttributes = null, LOVParams lovParams = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(helper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
			}
			else
			{
				rvd = new RouteValueDictionary();
			}
			if (lovParams != null)
			{
				rvd.Add("options", lovParams.ToString());
			}
			if (rvd.ContainsKey("class"))
			{
				rvd["class"] += " lov";
			}
			else
			{
				rvd.Add("class", "lov");
			}

			writer.InnerWriter.Write(helper.HiddenFor(hiddenExpression, new { @class = "hid_lov" }));
			writer.InnerWriter.Write(helper.TextBoxFor(expression, rvd));
		}

		#endregion

		#region DropdownListEx
		public static void DropdownListEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			IEnumerable<ListItemWidthType> selectList, object htmlAttributes = null, string TargetDescID = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(htmlHelper.ViewContext.HttpContext.Response.Output);

			StringBuilder sbItems = new StringBuilder();
			StringBuilder sbShow = new StringBuilder();
			string guid = Guid.NewGuid().ToString();
			string isShowdesc = "";
			if (htmlAttributes != null)
			{
				RouteValueDictionary rvd = new RouteValueDictionary(htmlAttributes);
				foreach (KeyValuePair<string, object> d in rvd)
				{
					if (d.Key.ToLower() == "id")
					{
						guid = d.Value.ToString();
					}
					else if (d.Key.ToLower() == "showdesc")
					{
						if (d.Value.ToString().ToLower() == "true")
						{
							isShowdesc = " showdesc";
						}
					}
				}
			}

			InitDropdown(sbShow, sbItems, selectList, guid, htmlAttributes, TargetDescID);

			writer.Write(sbShow.ToString());
			writer.Write(htmlHelper.TextBoxFor(expression, new { @class = "textboxDepart " + isShowdesc, @Tag = "", @style = "display: none;", @id = guid }));//
			writer.Write(sbItems.ToString());
		}
		public static void InitDropdown(StringBuilder sbShow, StringBuilder sbItems, IEnumerable<ListItemWidthType> selectList
			,string guid
			, object htmlAttributes = null, string TargetDescID = null)
		{
			RouteValueDictionary rvd;
			
			string mwidth = "width:100px;";
			string txtWidth = "width:72px;";
			string mheight = "max-height:100px;";
			string mDwidth = "width:150px";
			string disabled = "";
			
			string strView = "";

			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
				foreach (KeyValuePair<string, object> d in rvd)
				{
					if (d.Key.ToLower() == "width")
					{
						int temp = Convert.ToInt32(d.Value);
						if (temp < 35)
							temp = 35;
						mwidth = string.Format("width:{0}px;", temp);
						txtWidth = string.Format("width:{0}px;", temp - 28);
					}
					else if (d.Key.ToLower() == "height")
					{
						mheight = string.Format("max-height:{0}px;", d.Value);
					}
					else if (d.Key.ToLower() == "dwidth")
					{
						mDwidth = string.Format("width:{0}px;", d.Value);
					}
					else if (d.Key.ToLower() == "disabled")
					{
						if (d.Value.ToString().ToLower() == "disabled")
						{
							disabled = " disabled='disabled'";
						}
					}
					else if (d.Key.ToLower() == "view")
					{
						strView = "view=view";
					}
				}
			}
			if (TargetDescID == null)
				TargetDescID = "";

			StringBuilder sbItemContent = new StringBuilder();

			if (selectList != null)
			{
				foreach (ListItemWidthType t in selectList)
				{
					string strValue = t.Value;
					string strText = t.Text;
					string strDesc = "";
					if (!string.IsNullOrEmpty(t.InterValue))
					{
						strDesc=t.InterValue.Replace("\"", "{:#}");
					}

					string mTemp = string.Format("-{0}", t.InterValue);
					if (mTemp.Trim() == "-")
						mTemp = "";

					sbItemContent.AppendLine(string.Format("        <li><a obj='#<#\"DisplayName\":\"{0}\",\"objValue\":\"{1}\",\"objDesc\":\"{2}\",\"objTarget\":\"{3}\"#>#'>{4}</a></li>",
					 strText, strValue, strDesc, TargetDescID, strText + mTemp)
						.Replace("#<#", "{").Replace("#>#", "}"));
				}
			}

			int len = 1;
			if (selectList != null && selectList.Count() > 0)
				len = selectList.Count();

			sbItems.AppendLine(string.Format(
				"<div style=\"overflow-y:scroll;   min-width:20px; visibility:hidden;{0} {1} {2} -moz-user-select: none;-moz-user-focus: ignore;\" ", mheight, mDwidth,
				string.Format("height: {0}px;", len * 19 + 2)));
			sbItems.AppendLine(string.Format(" onselectstart=\"return false;\" id=\"popup{0}\" class=\"auto-complete-popup\">", guid));
			sbItems.AppendLine(string.Format("    <ul id=\"ul{0}\" guid='{0}'>", guid));
			sbItems.AppendLine(sbItemContent.ToString());//内容项
			sbItems.AppendLine("    </ul>");
			sbItems.AppendLine("</div>");

			
			//sub show
			sbShow.AppendLine(string.Format("<div id=\"div{0}\" onselectstart=\"return false;\" style=\"{1}\"  class=\"auto-complete-button\" tabindex=\"0\" {2} {3}>"
				, guid, mwidth, disabled, strView));
			sbShow.AppendLine("    <table style=\" border:0px; width:100%;\">");
			sbShow.AppendLine("        <tr>");
			sbShow.AppendLine("            <td style=\" overflow:hidden;\">");
			sbShow.AppendLine(string.Format("                <div id=\"caption{0}\" style=\"{1}\" title=\"\" class=\"auto-complete-button_value\"><a></a></div>", guid, txtWidth));
			sbShow.AppendLine("            </td>");
			sbShow.AppendLine("            <td  style=\" width:18px;\">");
			sbShow.AppendLine("                <div class=\"auto-complete-buttom-click\"><a >▼</a></div>");
			sbShow.AppendLine("            </td>");
			sbShow.AppendLine("        </tr>");
			sbShow.AppendLine("    </table>");
			sbShow.AppendLine("</div>");
		}
		


		public static void DropdownListExWithName(this HtmlHelper htmlHelper, string name,
			IEnumerable<ListItemWidthType> selectList, object htmlAttributes = null, string TargetDescID = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(htmlHelper.ViewContext.HttpContext.Response.Output);

			StringBuilder sbItems = new StringBuilder();
			StringBuilder sbShow = new StringBuilder();
			string guid = Guid.NewGuid().ToString();
			string isShowdesc = "";
			if (htmlAttributes != null)
			{
				RouteValueDictionary rvd = new RouteValueDictionary(htmlAttributes);
				foreach (KeyValuePair<string, object> d in rvd)
				{
					if (d.Key.ToLower() == "id")
					{
						guid = d.Value.ToString();
					}
					else if (d.Key.ToLower() == "showdesc")
					{
						if (d.Value.ToString().ToLower() == "true")
						{
							isShowdesc = " showdesc";
						}
					}
				}
			}
			InitDropdown(sbShow, sbItems, selectList, guid, htmlAttributes, TargetDescID);

			writer.Write(sbShow.ToString());
			writer.Write(htmlHelper.TextBox(name, string.Empty, new { @class = "textboxDepart " + isShowdesc, @style = "display: none;", @id = guid }));//
			writer.Write(sbItems.ToString());
		}


		#endregion

		#region AutoComplete
		public static void DropdownAutoComplete<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			IEnumerable<ListItemWidthType> selectList, object htmlAttributes = null, string TargetDescID = null)
		{
			HtmlTextWriter writer = new HtmlTextWriter(htmlHelper.ViewContext.HttpContext.Response.Output);

			RouteValueDictionary rvd;
			string guid = Guid.NewGuid().ToString();
			string mwidth = "width:100px;";
			string txtWidth = "width:94px;";
			string mheight = "max-height:100px;";
			string mDwidth = "width:150px";
			string disabled = "";
			string isShowdesc = "";
			string strView = "";


			if (htmlAttributes != null)
			{
				rvd = new RouteValueDictionary(htmlAttributes);
				foreach (KeyValuePair<string, object> d in rvd)
				{
					if (d.Key.ToLower() == "width")
					{
						int temp = Convert.ToInt32(d.Value);
						if (temp < 35)
							temp = 35;
						mwidth = string.Format("width:{0}px;", temp);
						txtWidth = string.Format("width:{0}px;", temp - 6);
					}
					else if (d.Key.ToLower() == "height")
					{
						mheight = string.Format("max-height:{0}px;", d.Value);
					}
					else if (d.Key.ToLower() == "dwidth")
					{
						mDwidth = string.Format("width:{0}px;", d.Value);
					}
					else if (d.Key.ToLower() == "id")
					{
						guid = d.Value.ToString();
					}
					else if (d.Key.ToLower() == "disabled")
					{
						if (d.Value.ToString().ToLower() == "disabled")
						{
							disabled = " disabled='disabled'";
						}
					}
					else if (d.Key.ToLower() == "showdesc")
					{
						if (d.Value.ToString().ToLower() == "true")
						{
							isShowdesc = " showdesc";
						}
					}
					else if (d.Key.ToLower() == "view")
					{
						strView = "view=view";
					}
				}
			}

			if (TargetDescID == null)
				TargetDescID = "";


			StringBuilder sbItems = new StringBuilder();
			StringBuilder sbItemContent = new StringBuilder();

			if (selectList != null)
			{
				foreach (ListItemWidthType t in selectList)
				{
					string strValue = t.Value;
					string strText = t.Text;
					string strDesc = t.InterValue;

					string mTemp = string.Format("-{0}", strDesc);
					if (mTemp.Trim() == "-")
						mTemp = "";
					sbItemContent.AppendLine(string.Format("        <li><a obj='#<#\"DisplayName\":\"{0}\",\"objValue\":\"{1}\",\"objDesc\":\"{2}\",\"objTarget\":\"{3}\"#>#'>{4}</a></li>",
					 strText, strValue, strDesc, TargetDescID, strText + mTemp)
						.Replace("#<#", "{").Replace("#>#", "}"));
				}
			}
			int len = 1;
			if (selectList != null && selectList.Count() > 0)
				len = selectList.Count();

			sbItems.AppendLine(string.Format(
				"<div style=\"overflow-y:scroll;   min-width:20px; visibility:hidden;{0} {1} {2} -moz-user-select: none;-moz-user-focus: ignore;\" ", mheight, mDwidth,
				string.Format("height: {0}px;", len * 19 + 2)));
			sbItems.AppendLine(string.Format(" onselectstart=\"return false;\" id=\"popup{0}\" class=\"auto-complete-popup\">", guid));
			sbItems.AppendLine(string.Format("    <ul id=\"ul{0}\" guid='{0}'>", guid));
			sbItems.AppendLine(sbItemContent.ToString());//内容项
			sbItems.AppendLine("    </ul>");
			sbItems.AppendLine("</div>");

			StringBuilder sbShow = new StringBuilder();

			sbShow.AppendLine(string.Format("<div id=\"div{0}\" onselectstart=\"return false;\" style=\"{1}\"  class=\"auto-completeBox-button\" tabindex=\"0\" {2} {3} TargetDescID='{4}'>"
				, guid, mwidth, disabled, strView
				, string.IsNullOrEmpty(TargetDescID) ? "" : TargetDescID));			
			string strInput = string.Format("<input type='text' style='{0}'/>", txtWidth);
			sbShow.AppendLine(string.Format("                <div id=\"caption{0}\" style=\"{1}\" title=\"\" class=\"auto-complete-button_value AutoComplete_value\">{2}<a></a></div>", guid, txtWidth, strInput));
			
			sbShow.AppendLine("</div>");
			writer.Write(sbShow.ToString());
			writer.Write(htmlHelper.TextBoxFor(expression, new { @class = "textboxDepart " + isShowdesc, @Tag = "", @style = "display: none;", @id = guid }));//
			writer.Write(sbItems.ToString());
		}
		#endregion
	}
}
public enum CalendarLanguageEnum
{
	/// <summary>
	/// 简体中文
	/// </summary>
	CN,
	/// <summary>
	/// 英文
	/// </summary>
	EN,
	/// <summary>
	/// 繁体中文
	/// </summary>
	HK
}

public enum CalendarTypeEnum
{
	/// <summary>
	/// 日历
	/// </summary>
	All,
	/// <summary>
	/// 年
	/// </summary>
	Year,
	/// <summary>
	/// 月
	/// </summary>
	Month,
	/// <summary>
	/// 日
	/// </summary>
	Day,
	/// <summary>
	/// 时
	/// </summary>
	Hour,
	/// <summary>
	/// 分
	/// </summary>
	Minute
}


/// <summary>
/// LOV Textbox 参数
/// </summary>
public class LOVParams
{
	public LOVParams()
	{
		this.width = 400;
		this.height = 350;
		this.isInput = false;
		this.isNotFindWindow = true;
		this.viewName = string.Empty;
		this.lovData = string.Empty;
	}
	/// <summary>
	/// 宽度
	/// </summary>
	public Int32 width { get; set; }
	/// <summary>
	/// 高度
	/// </summary>
	public Int32 height { get; set; }
	/// <summary>
	/// 视图名称
	/// </summary>
	public String viewName { get; set; }
	/// <summary>
	/// 是否能输入
	/// </summary>
	public Boolean isInput { get; set; }

	/// <summary>
	/// 是否能输入
	/// </summary>
	public Boolean isNotFindWindow { get; set; }

	public String lovData { get; set; }
	/// <summary>
	/// 返回 json
	/// </summary>
	/// <returns>json 字符串</returns>
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		LOVParams oldParams = new LOVParams();
		PropertyInfo[] pisOld = oldParams.GetType().GetProperties();
		PropertyInfo[] pis = this.GetType().GetProperties();
		for (int i = 0; i < pis.Length; i++)
		{
			var val = pis[i].GetValue(this, null);
			if (val != null)
			{
				if (val.Equals(pisOld[i].GetValue(oldParams, null)))
				{
					continue;
				}
				sb.AppendFormat("{0}: '{1}', ", pis[i].Name, val.ToString());
			}
		}
		return string.Format("{{{0}}}", sb.ToString().Trim().TrimEnd(','));
	}
}

public class ComboboxParams
{
	public ComboboxParams()
	{
		this.size = 25;
		this.isInput = false;
	}
	/// <summary>
	/// 显示的项数量
	/// </summary>
	public Int32 size { get; set; }
	/// <summary>
	/// 是否允许输入, 为 true 刚获取 text, false 获取 value
	/// </summary>
	public Boolean isInput { get; set; }
	/// <summary>
	/// 返回 json
	/// </summary>
	/// <returns>json 字符串</returns>
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		ComboboxParams oldParams = new ComboboxParams();
		PropertyInfo[] pisOld = oldParams.GetType().GetProperties();
		PropertyInfo[] pis = this.GetType().GetProperties();
		for (int i = 0; i < pis.Length; i++)
		{
			var val = pis[i].GetValue(this, null);
			if (val != null)
			{
				if (val.Equals(pisOld[i].GetValue(oldParams, null)))
				{
					continue;
				}
				sb.AppendFormat("{0}: '{1}', ", pis[i].Name, val.ToString());
			}
		}
		return string.Format("{{{0}}}", sb.ToString().Trim().TrimEnd(','));
	}
}
public class NumericParams
{
	public NumericParams()
	{
		this.maxValue = 100000000000;
		this.minValue = 0;
		this.digits = 0;
		this.step = 1;

	}

	public bool hideUpDownBtn { get; set; }
	/// <summary>
	/// 最大值
	/// </summary>
	public Decimal maxValue { get; set; }
	/// <summary>
	/// 最小值
	/// </summary>
	public Decimal minValue { get; set; }
	/// <summary>
	/// 小数位数
	/// </summary>
	public Int32 digits { get; set; }
	/// <summary>
	/// 步长
	/// </summary>
	public Decimal step { get; set; }
	/// <summary>
	/// 返回 json
	/// </summary>
	/// <returns>json 字符串</returns>
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		NumericParams oldParams = new NumericParams();
		PropertyInfo[] pisOld = oldParams.GetType().GetProperties();
		PropertyInfo[] pis = this.GetType().GetProperties();
		for (int i = 0; i < pis.Length; i++)
		{
			var val = pis[i].GetValue(this, null);
			if (val != null)
			{
				//if (val.Equals(pisOld[i].GetValue(oldParams, null))) {
				//    continue;
				//}
				sb.AppendFormat("{0}: '{1}', ", pis[i].Name, val.ToString());
			}
		}
		return string.Format("{{{0}}}", sb.ToString().Trim().TrimEnd(','));
	}
}

[Serializable]
public class CalendarParams
{
	public CalendarParams()
	{
		this.Css = "5";
		this.Lang = CalendarLanguageEnum.CN;
		this.Type = CalendarTypeEnum.All;
		this.Format = "yyyy-MM-dd";
		this.Date = null;
		this.StartDate = new DateTime(1900, 1, 1);
		this.EndDate = new DateTime(2050, 12, 31);
		this.IsAttach = false;
		this.IsClick = true;
		this.IsOver = true;
		this.IsClear = true;
		this.IsPosition = false;
		this.IsOutCurrentMonth = true;
		this.IsShowOften = true;
		this.FireEventObjectId = string.Empty;
		this.FireEventName = string.Empty;
		this.FunCall = string.Empty;
	}
	public string FunCall { get; set; }
	/// <summary>
	/// 皮肤编号
	/// </summary>
	public String Css { get; set; }
	/// <summary>
	/// 语言（cn - 简体中文，hk - 繁体中文，en - 英文）
	/// </summary>
	public CalendarLanguageEnum Lang { get; set; }
	/// <summary>
	/// 选择年/月/日/时/分/日期("Year","Month","Day","Hour","Minute","All")
	/// </summary>
	public CalendarTypeEnum Type { get; set; }
	/// <summary>
	/// 格式化日历输出，支持 3 种分隔符 [.,-,/]
	/// </summary>
	public String Format { get; set; }
	/// <summary>
	/// 日历起始时间，如果是当前时间，请留空（固定格式[yyyy-MM-dd]，年份在 1900 - 2099 年之间）
	/// </summary>
	public DateTime? Date { get; set; }
	/// <summary>
	/// 起始日期（固定格式[yyyy-MM-dd]，年份在 1900 - 2099 年之间）
	/// </summary>
	public DateTime StartDate { get; set; }
	/// <summary>
	/// 终止日期（固定格式[yyyy-MM-dd]，年份在 1900 - 2099 年之间）
	/// </summary>
	public DateTime EndDate { get; set; }
	/// <summary>
	/// 是否显示底端的时/分/秒
	/// </summary>
	public Boolean IsAttach { get; set; }
	/// <summary>
	/// 是否在文档其它地方单击隐藏
	/// </summary>
	public Boolean IsClick { get; set; }
	/// <summary>
	/// 是否鼠标移出隐藏
	/// </summary>
	public Boolean IsOver { get; set; }
	/// <summary>
	/// 清空操作后是否隐藏
	/// </summary>
	public Boolean IsClear { get; set; }
	/// <summary>
	/// 日历显示位置(true 表示显示在触发事件的对象下面)
	/// </summary>
	public Boolean IsPosition { get; set; }
	/// <summary>
	/// 是否输出不是本月的日期数据和每年的周数
	/// </summary>
	public Boolean IsOutCurrentMonth { get; set; }
	/// <summary>
	/// 当为只读或禁用时是否仍显示日历
	/// </summary>
	public Boolean IsShowOften { get; set; }
	/// <summary>
	/// 触发指定对象
	/// </summary>
	public String FireEventObjectId { get; set; }
	/// <summary>
	/// 触发指定事件
	/// </summary>
	public String FireEventName { get; set; }
	/// <summary>
	/// 返回 json
	/// </summary>
	/// <returns>json 字符串</returns>
	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		CalendarParams oldParams = new CalendarParams();
		PropertyInfo[] pisOld = oldParams.GetType().GetProperties();
		PropertyInfo[] pis = this.GetType().GetProperties();
		for (int i = 0; i < pis.Length; i++)
		{
			var val = pis[i].GetValue(this, null);
			if (val != null)
			{
				if (val.Equals(pisOld[i].GetValue(oldParams, null)))
				{
					continue;
				}
				if (pis[i].PropertyType == typeof(DateTime))
				{
					val = ((DateTime)val).ToString("yyyy-MM-dd");
				}
				sb.AppendFormat("{0}: '{1}', ", pis[i].Name, val.ToString());
			}
		}
		return string.Format("{{{0}}}", sb.ToString().Trim().TrimEnd(','));
	}



}



