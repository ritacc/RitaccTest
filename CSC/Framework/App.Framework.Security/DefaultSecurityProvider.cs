using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Security
{
	/// <summary>
	/// 用户相关
	/// </summary>
	public class DefaultSecurityProvider : ISecurityProvider
	{
		#region Fields
		private IEncrypt _IEncrypt = DefaultEncrypt.Instance;
		private IUserProfile _IUserProfile;
		#endregion

		#region Method
		public DefaultSecurityProvider(IEncrypt encrypt, IUserProfile userProfile)
		{
			_IEncrypt = encrypt;
			_IUserProfile = userProfile;
		}

		public bool ValidateUser(string username,string userdescription, string password, string shopCode, bool autoSaveSate = true)
		{
			if (string.IsNullOrEmpty(username)) throw new SecurityExceptionToUser(Resources.Exception.UserNameIsRequired);
			if (string.IsNullOrEmpty(password)) throw new SecurityExceptionToUser(Resources.Exception.PasswordIsRequired);
			if (!_IUserProfile.UserIsExists(username, shopCode))
				throw new SecurityExceptionToUser(Resources.Exception.UserNotExists);
			var ub = _IUserProfile.GetUserInfo(username, shopCode);
			long userid = Convert.ToInt64(ub.UserIdentity);
			if (_IUserProfile.GetErrorPasswordTimes(userid) > SecurityPortal.MaxInvalidPasswordAttempts)
			{
				_IUserProfile.Freeze(userid);
				_IUserProfile.ClearUserErrorPasswordTimes(userid);
				throw new SecurityExceptionToUser(Resources.Exception.HasExceededTheMaxInvalidPasswordAttempts);
			}
			string pass = _IUserProfile.GetUserPassword(username, shopCode);
			if (_IEncrypt.IsEqual(pass, password))
			{
				DateTime? errectTime = _IUserProfile.GetPasswordEffectiveDays(username, shopCode);
				if (errectTime < DateTime.Now)
					throw new SecurityExceptionToUser(Resources.Exception.PasswordIsEffected);
				if (autoSaveSate)
				{
					ub.ShopCode = shopCode;
				    ub.UserDescription = userdescription;
					_IUserProfile.SaveUserState(ub);
				}
				return true;
			}
			_IUserProfile.SaveErrorPasswordTimes(userid);
			throw new SecurityExceptionToUser(Resources.Exception.PasswordIsError);
		}

		public string GetUserPassword(string username, string shopCode)
		{
			string pwd = _IUserProfile.GetUserPassword(username, shopCode);
			return _IEncrypt.Decrypt(pwd);
		}

		public bool ChangePassword(string username, string oldPassword, string systemCode, string newPassword)
		{
			if (!ValidateUser(username,null, oldPassword, systemCode))
				throw new SecurityExceptionToUser(Resources.Exception.PasswordIsError);
			if (newPassword.Length < SecurityPortal.MinRequiredPasswordLength)
				throw new SecurityExceptionToUser(Resources.Exception.PasswordRequiredMinLengthFormat);
			_IUserProfile.ModifyUserPassword(username, newPassword, SecurityPortal.PasswordEffectiveDays);
			return true;
		}

		public void ModifyUserLoginPwdAndAuthPwd(long userID, string newLoginPwd, string newAuthPwd)
		{
			//if (newLoginPwd.Length < SecurityPortal.MinRequiredPasswordLength || newAuthPwd.Length < SecurityPortal.MinRequiredPasswordLength)
			//{
			//    throw new SecurityExceptionToUser(Resources.Exception.PasswordRequiredMinLengthFormat);
			//}

			_IUserProfile.ModifyUserLoginPwdAndAuthPwd(userID, _IEncrypt.Encrypt(newLoginPwd), _IEncrypt.Encrypt(newAuthPwd));
		}

		public void ModifyUserAuthPwd(long userID, string newAuthPwd)
		{
			//if (newLoginPwd.Length < SecurityPortal.MinRequiredPasswordLength || newAuthPwd.Length < SecurityPortal.MinRequiredPasswordLength)
			//{
			//    throw new SecurityExceptionToUser(Resources.Exception.PasswordRequiredMinLengthFormat);
			//}

			_IUserProfile.ModifyUserAuthPwd(userID, _IEncrypt.Encrypt(newAuthPwd));
		}

		public void ModifyUserLoginPwd(long userID, string newLoginPwd)
		{
			#region "check Minimum password length"
			if (newLoginPwd.Length < SecurityPortal.MinRequiredPasswordLength)
			{
				throw new SecurityExceptionToUser(string.Format(Resources.Exception.PasswordRequiredMinLengthFormat, SecurityPortal.MinRequiredPasswordLength.ToString()));
			}
			#endregion

			#region "check Password composition"
			int minNonAlphaChar = 0;
			int minAlphaChar = 0;
			int minNumChar = 0;
			int ascValue;

			foreach (char c in newLoginPwd)
			{
				ascValue = (int)c;

				//check num
				if (ascValue >= 48 && ascValue <= 57)
				{
					minNumChar = minNumChar + 1;
				}
				//check AlphaChar
				if ((ascValue >= 65 && ascValue <= 90) || (ascValue >= 97 && ascValue <= 122))
				{
					minAlphaChar = minAlphaChar + 1;
				}
				//check NonAlphaChar
				if ((ascValue >= 33 && ascValue <= 47) || (ascValue >= 58 && ascValue <= 64) || (ascValue >= 91 && ascValue <= 96) || (ascValue >= 123 && ascValue <= 126))
				{
					minNonAlphaChar = minNonAlphaChar + 1;
				}

			}

			if (minNonAlphaChar < SecurityPortal.MinNonAlphaChar)
			{
				throw new SecurityExceptionToUser(string.Format(Resources.Exception.MinNonAlphaChar, SecurityPortal.MinNonAlphaChar.ToString()));
			}

			if (minAlphaChar < SecurityPortal.MinAlphaChar)
			{
				throw new SecurityExceptionToUser(string.Format(Resources.Exception.MinAlphaChar, SecurityPortal.MinAlphaChar.ToString()));
			}

			if (minNumChar < SecurityPortal.MinNumChar)
			{
				throw new SecurityExceptionToUser(string.Format(Resources.Exception.MinNumChar, SecurityPortal.MinNumChar.ToString()));
			}
			#endregion

			#region "Password reuse prevention"
			if (!_IUserProfile.CheckLastPwd(userID, _IEncrypt.Encrypt(newLoginPwd), SecurityPortal.PasswordReusePreventionCount)) 
			{
				throw new SecurityExceptionToUser(string.Format(Resources.Exception.PasswordReusePrevention, SecurityPortal.PasswordReusePreventionCount.ToString()));
			}
			#endregion

			_IUserProfile.ModifyUserLoginPwd(userID, _IEncrypt.Encrypt(newLoginPwd));
		}

		#endregion

		public bool ValidateUser(string username, string password, bool autoSaveSate)
		{
			throw new NotImplementedException();
		}

		public bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public string EncryptPwd(string pwd)
		{
			return _IEncrypt.Encrypt(pwd);
		}
	}
}
