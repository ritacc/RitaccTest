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
			public void test()
			{
				//Console.WriteLine(string.Format("step 1:{0}", index));
				int i = index;
				lock (mobj)
				{
					while (i > 10)
					{
						Console.WriteLine(string.Format("step 2:{0} Name={1}", index, name));
						i--;
						index--;
						test();
						Thread.Sleep(new Random().Next(100,400));
					}
				}
			}
		}

		public static void Main()
		{
			mLock
				m = new mLock();
			m.name = "AA";
			m.test();
			m.index = 17;
			Thread th = new Thread(m.test);
			th.Start();

			mLock
				m1 = new mLock();
			m1.name = "cc";
			m1.test();
			m1.index = 19;
			Thread th1 = new Thread(m1.test);
			th1.Start();

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
