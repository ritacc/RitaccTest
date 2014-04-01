using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Security
{
    public interface IEncrypt
    {
        string Encrypt(string content);
        string Decrypt(string encrpted);
        bool IsEqual(string encrpted, string original);
    }
}
