using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using App.Framework.Data;

namespace App.Framework.Reflection
{
	public static class DataEntityBuilder
	{
		internal static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
		private static readonly Type builderType = typeof(DataReaderDelegate);

		public static DataReaderDelegate CreateDataReaderBuilder(Type entityType)
		{
			DataReaderDelegate builder = cache[entityType] as DataReaderDelegate;
			if(builder == null)
			{
				lock(cache.SyncRoot)
				{
					builder = cache[entityType] as DataReaderDelegate;
					if(builder != null)
					{
						return builder;
					}

					Type[] DynamicMethodArgs = new Type[] { typeof(INullableDataReader) };
					DynamicMethod dynMethod = new DynamicMethod(string.Format("EMIT${0}_ENTITY_BUILDER", entityType.Name), entityType, DynamicMethodArgs, entityType);
					ILGenerator generator = dynMethod.GetILGenerator();
					//entityType result;
					LocalBuilder result = generator.DeclareLocal(entityType);

					//if(IDataReader.Read())
					Label noRecord = generator.DefineLabel();
					generator.Emit(OpCodes.Ldarg_0);
					generator.Emit(OpCodes.Call, typeof(IDataReader).GetMethod("Read", Type.EmptyTypes));
					generator.Emit(OpCodes.Brfalse, noRecord);
					{
						//result = new entityType()
						generator.Emit(OpCodes.Newobj, entityType.GetConstructor(Type.EmptyTypes));
						generator.Emit(OpCodes.Stloc, result);
						FillEntity(generator, entityType, result);
					}
					generator.MarkLabel(noRecord);
					generator.Emit(OpCodes.Ldloc, result);
					generator.Emit(OpCodes.Ret);

					builder = (DataReaderDelegate)dynMethod.CreateDelegate(builderType);
					cache.Add(entityType, builder);
				}
			}
			return builder;
		}

		public static void FillEntity(ILGenerator generator, Type entityType, LocalBuilder entity)
		{
			//Dictionary<string, int> fields = DataPortal.FindFields(data);
			LocalBuilder fields = generator.DeclareLocal(typeof(Dictionary<string, int>));
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Call, typeof(DataPortal).GetMethod("FindFields", new Type[]{typeof(IDataRecord)}));
			generator.Emit(OpCodes.Stloc, fields);

			foreach(PropertyInfo property in entityType.GetProperties())
			{
				DbFieldAttribute fieldInfo = ReflectionHelper.GetAttribute<DbFieldAttribute>(property);
				if(fieldInfo == null)
				{
					continue;
				}

				//int fieldIndex = DataPortal.FindField(fields, propInfo.Name);
				LocalBuilder fieldIndex = generator.DeclareLocal(typeof(Int32));
				generator.Emit(OpCodes.Ldloc, fields);
				generator.Emit(OpCodes.Ldstr, fieldInfo.FieldName);
				generator.Emit(OpCodes.Call, typeof(DataPortal).GetMethod("FindField", new Type[] { typeof(Dictionary<string, int>), typeof(string) }));
				generator.Emit(OpCodes.Stloc, fieldIndex);
				//if(fieldIndex >= 0)
				Label notFieldExists = generator.DefineLabel();
				generator.Emit(OpCodes.Ldloc, fieldIndex);
				generator.Emit(OpCodes.Ldc_I4_0);
				generator.Emit(OpCodes.Clt);
				generator.Emit(OpCodes.Brtrue_S, notFieldExists);
				{
					FillProperty(generator, entityType, entity, property, fieldIndex);
					generator.Emit(OpCodes.Nop);
				}
				generator.MarkLabel(notFieldExists);
			}
		}

		public static void FillProperty(ILGenerator generator, Type entityType, LocalBuilder entity, PropertyInfo property, LocalBuilder fieldIndex)
		{
			LocalBuilder value = generator.DeclareLocal(property.PropertyType);
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Ldloc, fieldIndex);

			switch(property.PropertyType.Name)
			{
				case "Nullable`1":
					Type underlyingType = property.PropertyType.GetGenericArguments()[0];
					switch(underlyingType.Name)
					{
						case "Byte":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableByte", new Type[] { typeof(Int32) }));
							break;
						case "Int16":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableInt16", new Type[] { typeof(Int32) }));
							break;
						case "Int32":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableInt32", new Type[] { typeof(Int32) }));
							break;
						case "Int64":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableInt64", new Type[] { typeof(Int32) }));
							break;
						case "Decimal":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableDecimal", new Type[] { typeof(Int32) }));
							break;
						case "Float":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableFloat", new Type[] { typeof(Int32) }));
							break;
						case "Double":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableDouble", new Type[] { typeof(Int32) }));
							break;
						case "Char":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableChar", new Type[] { typeof(Int32) }));
							break;
						case "DateTime":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableDateTime", new Type[] { typeof(Int32) }));
							break;
						case "Boolean":
							generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableBoolean", new Type[] { typeof(Int32) }));
							break;
                        case "Guid":
                            generator.Emit(OpCodes.Callvirt, typeof(INullableDataReader).GetMethod("GetNullableGuid", new Type[] { typeof(Int32) }));
                            break;
					}
					break;
				case "Byte":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetByte", new Type[] { typeof(Int32) }));
					break;
				case "Int16":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt16", new Type[] { typeof(Int32) }));
					break;
				case "Int32":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt32", new Type[] { typeof(Int32) }));
					break;
				case "Int64":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt64", new Type[] { typeof(Int32) }));
					break;
				case "Decimal":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDecimal", new Type[] { typeof(Int32) }));
					break;
				case "Float":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetFloat", new Type[] { typeof(Int32) }));
					break;
				case "Double":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDouble", new Type[] { typeof(Int32) }));
					break;
				case "Char":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetChar", new Type[] { typeof(Int32) }));
					break;
				case "String":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetString", new Type[] { typeof(Int32) }));
					break;
				case "DateTime":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDateTime", new Type[] { typeof(Int32) }));
					break;
				case "Boolean":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetBoolean", new Type[] { typeof(Int32) }));
					break;
				case "Guid":
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetGuid", new Type[] { typeof(Int32) }));
					break;
				default:
					generator.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetValue", new Type[] { typeof(Int32) }));
					break;
			}
			generator.Emit(OpCodes.Stloc, value);
			generator.Emit(OpCodes.Ldloc, entity);
			generator.Emit(OpCodes.Ldloc, value);
			generator.Emit(OpCodes.Callvirt, entityType.GetMethod("set_" + property.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { property.PropertyType }, null));
		}
	}
}