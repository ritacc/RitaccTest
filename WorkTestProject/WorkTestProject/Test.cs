using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WorkTestProject
{
	public class program
	{

		/*
		 * lock
		 */
		public class mLock
		{
			public int index { get; set; }
			public string name { get; set; }
			public static object mobj = new object();
			public void test(tt mt)
			{
				//Console.WriteLine(string.Format("step 1:{0}", index));
				 
				lock (this)
				{
					while (mt.index > 10)
					{
						Console.WriteLine(string.Format("step 2:{0} Name={1}", mt.index, name));
						mt.index--;

						test(mt);
						 
					}
				}
			}
		}
		public class tt
		{
			public tt(int _index)
			{
				index = _index;
			}
			public int index { get; set; }
		}
		public static void Main()
		{
			new mLock().test(new tt(15));
			Console.ReadKey();
		}

		/**********
		 * 交换两个值
		 * ******/
		//public static void Main()
		//{
		//    int a = 2;
		//    int b = 22;

		//    Console.WriteLine(string.Format("1: a={0} b={1}", a, b));

		//    a = a ^ b;
		//    b = a ^ b;
		//    a = a ^ b;
		//    Console.WriteLine(string.Format("2: a={0} b={1}", a, b));

		//    a = b - a;
		//    b = b - a;
		//    a = b + a;
		//    Console.WriteLine(string.Format("3: a={0} b={1}", a, b));
		//    Console.ReadKey();
		//}
	}
}
