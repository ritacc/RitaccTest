using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalysisJson
{
	//'Name':'list','Rows':
	//[{'ID':971,'subjectType':2,'IssueSubject':'动画1中有几种违法行为？',
	//'EIssueSubject':null,'IssueType_ID':1,'Answer':'B',
	//'IssueResult':'<p>A. 一种违法行为<br />B. 二种违法行为<br />C. 三种违法行为<br /> D. 四种违法行为<br /><br /></p>',
	//'EIssueResult':null,'ImagePath':'/UploadFiles/subject3/动画1.wmv'},
	public class Q
	{
		public int ID { get; set; }
		public int subjectType { get; set; }
		public string IssueSubject { get; set; }
		public string EIssueSubject { get; set; }
		public string IssueType_ID { get; set; }
		public string Answer { get; set; }
		public string IssueResult { get; set; }
		public string EIssueResult { get; set; }
		public string ImagePath { get; set; }
	}
}
