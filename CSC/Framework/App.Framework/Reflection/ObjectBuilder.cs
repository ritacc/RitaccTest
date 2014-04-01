using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using App.Framework.Data;

namespace App.Framework.Reflection
{
	public static class ObjectBuilder
	{
		internal static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());

		private readonly static Type builderType = typeof(Builder);
		public delegate object Builder();

		public static Builder CreateBuilder<T>()
		{
			Type type = typeof(T);
			return CreateBuilder(type);
		}

		public static Builder CreateBuilder(Type type)
		{
			Builder builder = cache[type] as Builder;
			if(builder == null)
			{
				lock(cache.SyncRoot)
				{
					builder = cache[type] as Builder;
					if(builder != null)
					{
						return builder;
					}

					DynamicMethod dynMethod = new DynamicMethod(string.Format("EMIT${0}_OBJECT_BUILDER", type.Name), type, Type.EmptyTypes, type);
					ILGenerator generator = dynMethod.GetILGenerator();
					//Type result = new Type()
					LocalBuilder result = generator.DeclareLocal(type);
					generator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
					generator.Emit(OpCodes.Stloc, result);
					//return result;
					generator.Emit(OpCodes.Ldloc, result);
					generator.Emit(OpCodes.Ret);

					builder = (Builder)dynMethod.CreateDelegate(builderType);
					cache.Add(type, builder);
				}
			}
			return builder;
		}
	}
}
