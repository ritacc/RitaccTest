using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolProject
{
    class BaseClass
    {
        public virtual void F()
        {
            Console.WriteLine("BaseClass.F");
        }
    }
    sealed class DeriveClass : BaseClass
    {
        //基类中的虚函数F被隐式的转化为非虚函数

        //密封类中不能再声明新的虚函数G
        //public virtual void G()
        //{
        //    Console.WriteLine("DeriveClass.G");
        //}
    }

    //需使用 new 修饰符显式声明，表示隐藏了基类中该函数的实现
    abstract class DeriveClass1 : BaseClass
    {
        public abstract new void F();
    }

    //抽象重写基类的虚方法
    abstract class DeriveClass2 : BaseClass
    {
        public abstract override void F();
       
    }
}
