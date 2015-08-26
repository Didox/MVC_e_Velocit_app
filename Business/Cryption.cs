using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.ComponentModel;

namespace Didox.Business
{
    public class Cryption
    {
        public String DecryptString(string encryptedString)
        {
            if (String.IsNullOrEmpty(encryptedString)) return String.Empty;
            try
            {
                using (TripleDESCryptoServiceProvider cypher = new TripleDESCryptoServiceProvider())
                {
                    PasswordDeriveBytes pdb = new PasswordDeriveBytes("DIDOXBUSINESS", new byte[0]);
                    cypher.Key = pdb.GetBytes(16);
                    cypher.IV = pdb.GetBytes(8);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, cypher.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            byte[] data = Convert.FromBase64String(encryptedString);
                            cs.Write(data, 0, data.Length);
                            cs.Close();
                            return Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch { return String.Empty; }
        }

        public String EncryptString(string decryptedString)
        {
            if (String.IsNullOrEmpty(decryptedString)) return String.Empty;
            using (TripleDESCryptoServiceProvider cypher = new TripleDESCryptoServiceProvider())
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes("DIDOXBUSINESS", new byte[0]);
                cypher.Key = pdb.GetBytes(16);
                cypher.IV = pdb.GetBytes(8);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, cypher.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.Unicode.GetBytes(decryptedString);
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public T DecryptObject<T>(object value, T defaultValue)
        {
            if (value == null) return defaultValue;
            try
            {
                Type conversionType = typeof(T);
                if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    conversionType = new NullableConverter(conversionType).UnderlyingType;
                return (T)Convert.ChangeType(DecryptString(Convert.ToString(value)), conversionType);
            }
            catch { return defaultValue; }
        }
    }
}
