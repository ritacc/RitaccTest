using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkTestProject
{

    public class CatShoutEventArgs : EventArgs
    {
        public string Name { get; set; }
    }

   public class Cat
    {
       public Cat(string _name)
       {
           Name = _name;
       }

       public string Name { get; set; }

       public delegate void CatShoutHeadler(object obj,CatShoutEventArgs e);
       public event CatShoutHeadler catShout;

       //public delegate 
       public void Shout()
       {
           Console.WriteLine(string.Format("大家好，我是小猫:{0}",Name));
           if (catShout != null)
           {
               CatShoutEventArgs e = new CatShoutEventArgs();
               e.Name = this.Name;
               catShout(this,e);
           }
       }
    }

   public class Mouse
   {
       public Mouse(string _name)
       {
           Name = _name;
       }

       public string Name { get; set; }
       public void run(object obj,CatShoutEventArgs e)
       {
           Console.WriteLine(string.Format("猫:{0}来了：{1}快跑。 ",e.Name, Name));
       }
   }

	/*
    public class program
    {
        public static void Main()
        {
            Cat _cat = new Cat("小猫猫");
            Mouse _kiity = new Mouse("kitty");
            Mouse _hK = new Mouse("K");

            _cat.catShout += new Cat.CatShoutHeadler(_kiity.run);
            _cat.catShout += new Cat.CatShoutHeadler(_hK.run);
            _cat.Shout();

            Console.ReadKey();
        }
    }
	 
	*/
}
