using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Security
{
	public interface ISecurityProvider
	{
        bool ValidateUser(string username, string password, bool autoSaveSate);
		bool ChangePassword(string username, string oldPassword, string newPassword);
	}
}