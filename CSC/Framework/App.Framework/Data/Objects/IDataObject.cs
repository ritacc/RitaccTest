using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace App.Framework.Data
{
	public interface IDataObject
	{
		BusinessResult Save();
		BusinessResult Save(IDbTransaction trans);
		BusinessResult Delete();
		BusinessResult Delete(IDbTransaction trans);
	}
}