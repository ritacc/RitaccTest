using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkTestProject
{
	#region Log
	public abstract class Log
	{
		public abstract void Write();
		
	}

	public class FileLog : Log
	{
		public override void Write()
		{
			Console.Write("File log Write.");
		}
	}

	public class EventLog : Log
	{
		public override void Write()
		{
			Console.Write("Event log Write.");
		}
	}
	#endregion

	#region factory

	public abstract class LogFactory
	{
		public abstract Log Create();
	}

	public class EventFactory : LogFactory
	{
		public override Log Create()
		{
			return new EventLog();
		}
	}

	public class FileFactory : LogFactory
	{
		public override Log Create()
		{
			return new FileLog();
		}
	}
	#endregion
	/*
	class Program
	{
		static void Main(string[] args)
		{
			LogFactory factory = new EventFactory();
			Log log = factory.Create();
			log.Write();
			Console.ReadKey();
		}
	}
	 * /
	/***************
	 *  <appSettings>
     *		<add key="factoryName" value="EventFactory"></add>
	 *	</appSettings>
	 *  string strfactoryName = ConfigurationSettings.AppSettings["factoryName"];
	 *  LogFactory factory;
	 * factory = (LogFactory)Assembly.Load("FactoryMethod").CreateInstance("FactoryMethod." + strfactoryName);
	 * Log log = factory.Create();
	 * log.Write();
	 * ***********/


}
