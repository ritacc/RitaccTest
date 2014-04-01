using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using App.Framework.Reflection;

namespace App.Framework.Data
{
	public static class SqlCriteriaSync
	{
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
		private static readonly Type syncType = typeof(Sync);
		public delegate void Sync(SqlCommand command, DataCriteria criteria);

		public static Sync CreateSync<T>() where T : DataCriteria
		{
			return CreateSync(typeof(T));
		}

		public static Sync CreateSync(Type criteriaType)
		{
			Sync sync = cache[criteriaType] as Sync;
			if(sync == null)
			{
				lock(cache.SyncRoot)
				{
					sync = cache[criteriaType] as Sync;
					if(sync != null)
					{
						return sync;
					}
					Type[] DynamicMethodArgs = new Type[] { typeof(SqlCommand), typeof(DataCriteria) };
					DynamicMethod dynMethod = new DynamicMethod(string.Format("EMIT${0}_CRITERIA_SYNC", criteriaType.Name), null, DynamicMethodArgs, criteriaType);
					ILGenerator generator = dynMethod.GetILGenerator();
					//SqlParameter param;
					LocalBuilder param = generator.DeclareLocal(typeof(SqlParameter));
					foreach(PropertyInfo property in criteriaType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
					{
						DbParameterAttribute paramInfo = ReflectionHelper.GetAttribute<DbParameterAttribute>(property);
						if(paramInfo == null)
						{
							continue;
						}
						if(paramInfo.Direction == ParameterDirection.Input)
						{
							continue;
						}
						//param = SqlCommand.Parameters["@" + paramInfo.ParameterName]
						generator.Emit(OpCodes.Ldarg_0);
						generator.Emit(OpCodes.Callvirt, typeof(SqlCommand).GetMethod("get_Parameters", Type.EmptyTypes));
						generator.Emit(OpCodes.Ldstr, "@" + paramInfo.ParameterName);
						generator.Emit(OpCodes.Callvirt, typeof(SqlParameterCollection).GetMethod("get_Item", new Type[] { typeof(String) }));
						generator.Emit(OpCodes.Stloc, param);
						// object value = param.Value;
						LocalBuilder value = generator.DeclareLocal(typeof(object));
						generator.Emit(OpCodes.Ldloc, param);
						generator.Emit(OpCodes.Call, typeof(SqlProvider).GetMethod("GetOutputParameterValue", new Type[] { typeof(SqlParameter) }));
						generator.Emit(OpCodes.Stloc, value);

						//Criteria.Property = value;
						if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
						{
							LocalBuilder nullableValue = generator.DeclareLocal(property.PropertyType);
							Label falseLabel = generator.DefineLabel();
							Label endLabel = generator.DefineLabel();
							//if(value == null)
							generator.Emit(OpCodes.Ldloc, value);
							generator.Emit(OpCodes.Ldnull);
							generator.Emit(OpCodes.Ceq);
							generator.Emit(OpCodes.Brfalse_S, falseLabel);
							{
								generator.Emit(OpCodes.Ldarg_1);
								generator.Emit(OpCodes.Ldloca_S, nullableValue);
								generator.Emit(OpCodes.Initobj, property.PropertyType);
								generator.Emit(OpCodes.Ldloc, nullableValue);
								generator.Emit(OpCodes.Callvirt, criteriaType.GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
								generator.Emit(OpCodes.Br_S, endLabel);
							}
							//else
							{
								generator.MarkLabel(falseLabel);
								LocalBuilder converter = generator.DeclareLocal(typeof(NullableConverter));
								LocalBuilder type = generator.DeclareLocal(typeof(Type));
								generator.Emit(OpCodes.Ldtoken, property.PropertyType);
								generator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
								generator.Emit(OpCodes.Stloc, type);
								generator.Emit(OpCodes.Ldloc, type);
								generator.Emit(OpCodes.Newobj, typeof(NullableConverter).GetConstructor(new Type[] { typeof(Type) }));
								generator.Emit(OpCodes.Stloc, converter);
								generator.Emit(OpCodes.Ldarg_1);
								generator.Emit(OpCodes.Ldloc, value);
								generator.Emit(OpCodes.Ldloc, converter);
								generator.Emit(OpCodes.Callvirt, typeof(NullableConverter).GetMethod("get_UnderlyingType", Type.EmptyTypes));
								generator.Emit(OpCodes.Call, typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) }));
								generator.Emit(OpCodes.Unbox_Any, property.PropertyType);
								generator.Emit(OpCodes.Callvirt, criteriaType.GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
							}
							generator.MarkLabel(endLabel);
						}
						else
						{
							generator.Emit(OpCodes.Ldarg_1);
							generator.Emit(OpCodes.Ldloc, value);
							if(property.PropertyType.IsValueType)
							{
								generator.Emit(OpCodes.Unbox_Any, property.PropertyType);
							}
							generator.Emit(OpCodes.Callvirt, criteriaType.GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
						}
						generator.Emit(OpCodes.Nop);
					}
					generator.Emit(OpCodes.Ret);

					sync = (Sync)dynMethod.CreateDelegate(syncType);
					cache.Add(criteriaType, sync);
				}
			}
			return sync;
		}
	}
}