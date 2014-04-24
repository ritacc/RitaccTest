using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkTestProject
{
	public  class A
	{
		public A()
		{
			Console.WriteLine('A');
		}
		string str1 = "小猪";
		string str2 = "小猪";

		public virtual void Fun()
		{
			object obj1 = str1;
			object obj2 = str2;
		 Boolean bresult=	obj1.Equals(obj2);
		 Console.WriteLine(bresult.ToString());

			Console.WriteLine("A.Fun()");
		}
	}

	public class B : A
	{
		public B()
		{
			Console.WriteLine('B');
		}

		public new void Fun()
		{
			Console.WriteLine("B.Fun()");
		}
        /*
		public static void Main()
		{
			A a = new B();
			a.Fun();
			Console.ReadKey();
		}
         * */
	} 

}
