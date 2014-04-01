using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using System.Reflection.Emit;
using App.Framework.Reflection;

namespace App.Framework.Data
{
	public static class SqlCommandBuilder
	{
		internal static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
		private static readonly Type builderType = typeof(Builder);
		public delegate SqlCommand Builder(DataCriteria criteria);

		public static Builder CreateBuilder<T>() where T : DataCriteria
		{
			return CreateBuilder(typeof(T));
		}

		public static Builder CreateBuilder(Type criteriaType)
		{
			Builder builder = cache[criteriaType] as Builder;
			if(builder == null)
			{
				lock(cache.SyncRoot)
				{
					builder = cache[criteriaType] as Builder;
					if(builder != null)
					{
						return builder;
					}

					Type[] DynamicMethodArgs = new Type[] { typeof(DataCriteria) };
					DynamicMethod dynMethod = new DynamicMethod(string.Format("EMIT${0}_DBCOMMAND_BUILDER", criteriaType.Name), typeof(SqlCommand), DynamicMethodArgs, criteriaType);
					ILGenerator generator = dynMethod.GetILGenerator();

					//SqlCommand result = new SqlCommand()
					LocalBuilder result = generator.DeclareLocal(typeof(SqlCommand));
					generator.Emit(OpCodes.Newobj, typeof(SqlCommand).GetConstructor(Type.EmptyTypes));
					generator.Emit(OpCodes.Stloc, result);
					//result.CommandText = cmdInfo.CommandText
					DbCommandAttribute cmdInfo = ReflectionHelper.GetAttribute<DbCommandAttribute>(criteriaType);
					generator.Emit(OpCodes.Ldloc, result);
					generator.Emit(OpCodes.Ldstr, cmdInfo.CommandText);
					generator.Emit(OpCodes.Callvirt, typeof(SqlCommand).GetMethod("set_CommandText", new Type[] { typeof(String) }));
					//result.CommandType = cmdInfo.CommandType
					generator.Emit(OpCodes.Ldloc, result);
					generator.Emit(OpCodes.Ldc_I4, (Int32)cmdInfo.CommandType);
					generator.Emit(OpCodes.Callvirt, typeof(SqlCommand).GetMethod("set_CommandType", new Type[] { typeof(CommandType) }));
					//SqlParameter param;
					LocalBuilder param = generator.DeclareLocal(typeof(SqlParameter));
					foreach(PropertyInfo property in criteriaType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
					{
						DbParameterAttribute paramInfo = ReflectionHelper.GetAttribute<DbParameterAttribute>(property);
						if(paramInfo == null)
						{
							continue;
						}
						//param = new SqlParameter();
						generator.Emit(OpCodes.Newobj, typeof(SqlParameter).GetConstructor(Type.EmptyTypes));
						generator.Emit(OpCodes.Stloc, param);
						//param.ParameterName = "@" + attribute.ParameterName;
						generator.Emit(OpCodes.Ldloc, param);
						generator.Emit(OpCodes.Ldstr, "@" + paramInfo.ParameterName);
						generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_ParameterName", new Type[] { typeof(String) }));
						// property.PropertyType value = DataCriteria.get_Property();
						LocalBuilder value = generator.DeclareLocal(property.PropertyType);
						generator.Emit(OpCodes.Ldarg_0);
						generator.Emit(OpCodes.Callvirt, criteriaType.GetMethod("get_" + property.Name, Type.EmptyTypes));
						generator.Emit(OpCodes.Stloc, value);

						switch(property.PropertyType.Name)
						{
							case "Nullable`1":
								//param.SqlDbType = property.PropertyType.GetGenericArguments()[0];
								generator.Emit(OpCodes.Ldloc, param);
								generator.Emit(OpCodes.Ldc_I4, (Int32)SqlProvider.GetDbType(property.PropertyType.GetGenericArguments()[0]));
								generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_SqlDbType", new Type[] { typeof(SqlDbType) }));

								//if(value.HasValue())
								Label isNotNullValue = generator.DefineLabel();
								Label endifNullValue = generator.DefineLabel();
								generator.Emit(OpCodes.Ldloca_S, value);
								generator.Emit(OpCodes.Call, property.PropertyType.GetMethod("get_HasValue", Type.EmptyTypes));
								generator.Emit(OpCodes.Brfalse_S, endifNullValue);
								{
									//T notNullValue = value.Value;
									LocalBuilder notNullValue = generator.DeclareLocal(property.PropertyType.GetGenericArguments()[0]);
									generator.Emit(OpCodes.Ldloca_S, value);
									generator.Emit(OpCodes.Call, property.PropertyType.GetMethod("get_Value", Type.EmptyTypes));
									generator.Emit(OpCodes.Stloc, notNullValue);
									//param.Value = notNullValue;
									generator.Emit(OpCodes.Ldloc, param);
									generator.Emit(OpCodes.Ldloc, notNullValue);
									generator.Emit(OpCodes.Box, property.PropertyType.GetGenericArguments()[0]);
									generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
								}
								generator.MarkLabel(endifNullValue);
								break;
							case "String":
								//if(value==null) value = string.Empty
								Label isNotNullString = generator.DefineLabel();
								Label endifNullString = generator.DefineLabel();
								generator.Emit(OpCodes.Ldloc, value);
								generator.Emit(OpCodes.Unbox_Any, property.PropertyType);
								generator.Emit(OpCodes.Call, typeof(String).GetMethod("IsNullOrWhiteSpace", new Type[] { typeof(String) }));
								generator.Emit(OpCodes.Brfalse_S, isNotNullString);
								{
									generator.Emit(OpCodes.Ldloc, param);
									generator.Emit(OpCodes.Ldsfld, typeof(String).GetField("Empty"));
									generator.Emit(OpCodes.Box, property.PropertyType);
									generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
									generator.Emit(OpCodes.Br, endifNullString);
								}
								//else
								generator.MarkLabel(isNotNullString);
								{
									generator.Emit(OpCodes.Ldloc, value);
									generator.Emit(OpCodes.Callvirt, typeof(String).GetMethod("Trim", Type.EmptyTypes));
									generator.Emit(OpCodes.Stloc, value);
									generator.Emit(OpCodes.Ldloc, param);
									generator.Emit(OpCodes.Ldloc, value);
									generator.Emit(OpCodes.Box, property.PropertyType);
									generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
								}
								generator.MarkLabel(endifNullString);
								break;
							case "DateTime":
								Label isNotMinDate = generator.DefineLabel();
								Label endifMinDate = generator.DefineLabel();
								//if(DataCriteria.get_Property() == DateTime.MinValue)
								generator.Emit(OpCodes.Ldloc, value);
								generator.Emit(OpCodes.Ldsfld, typeof(DateTime).GetField("MinValue"));
								generator.Emit(OpCodes.Call, typeof(DateTime).GetMethod("op_Equality", new Type[] { typeof(DateTime), typeof(DateTime) }));
								generator.Emit(OpCodes.Ldc_I4_0);
								generator.Emit(OpCodes.Beq_S, isNotMinDate);
								{
									generator.Emit(OpCodes.Ldloc, param);
									generator.Emit(OpCodes.Ldsfld, typeof(SqlDateTime).GetField("MinValue"));
									generator.Emit(OpCodes.Call, typeof(SqlDateTime).GetMethod("op_Explicit", new Type[] { typeof(SqlDateTime) }));
									generator.Emit(OpCodes.Box, property.PropertyType);
									generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
									generator.Emit(OpCodes.Br, endifMinDate);
								}
								//else
								generator.MarkLabel(isNotMinDate);
								{
									generator.Emit(OpCodes.Ldloc, param);
									generator.Emit(OpCodes.Ldloc, value);
									generator.Emit(OpCodes.Box, property.PropertyType);
									generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
								}
								generator.MarkLabel(endifMinDate);
								break;
							default:
								//param.Value = DataCriteria.get_Property();
								generator.Emit(OpCodes.Ldloc, param);
								generator.Emit(OpCodes.Ldloc, value);
								generator.Emit(OpCodes.Box, property.PropertyType);
								generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Value", new Type[] { typeof(object) }));
								break;
						}
						//param.Direction = attribute.Direction;
						generator.Emit(OpCodes.Ldloc, param);
						generator.Emit(OpCodes.Ldc_I4, (Int32)paramInfo.Direction);
						generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Direction", new Type[] { typeof(ParameterDirection) }));
						//param.Size = attribute.Size;
						generator.Emit(OpCodes.Ldloc, param);
						generator.Emit(OpCodes.Ldc_I4, paramInfo.Size);
						generator.Emit(OpCodes.Callvirt, typeof(SqlParameter).GetMethod("set_Size", new Type[] { typeof(Int32) }));
						// command.Parameters.Add(param);
						generator.Emit(OpCodes.Ldloc, result);
						generator.Emit(OpCodes.Callvirt, typeof(SqlCommand).GetMethod("get_Parameters", Type.EmptyTypes));
						generator.Emit(OpCodes.Ldloc, param);
						generator.Emit(OpCodes.Callvirt, typeof(SqlParameterCollection).GetMethod("Add", new Type[] { typeof(SqlParameter) }));
						generator.Emit(OpCodes.Pop);
					}
					//return Result;
					generator.Emit(OpCodes.Ldloc, result);
					generator.Emit(OpCodes.Ret);

					builder = (Builder)dynMethod.CreateDelegate(builderType);
					cache.Add(criteriaType, builder);
				}
			}
			return builder;
		}
	}
}
