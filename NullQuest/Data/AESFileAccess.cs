using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace NullQuest.Data
{
    class AESFileAccess : IFileAccess
    {
        private readonly IFileAccess _fileAccess;
        public AESFileAccess(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;
        }

        private SymmetricAlgorithm GetAlgorithm()
        {
            Rijndael algo = Rijndael.Create();
            algo.Padding = PaddingMode.PKCS7;
            algo.Key = Convert.FromBase64String("LEWHRsdfhewrkh34lkRHah==");
            algo.IV = new byte[] { 43, 125, 183, 126, 71, 164, 226, 45, 246, 20, 161, 9, 71, 126, 120, 219 };
            return algo;
        }

        public bool DoesFileExist
        {
            get { return _fileAccess.DoesFileExist; }
        }

        public System.IO.StreamReader CreateReader()
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithm())
            {
                CryptoStream cs = new CryptoStream(_fileAccess.CreateReader().BaseStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
                return new System.IO.StreamReader(cs);
            }
        }

        public System.IO.StreamWriter CreateWriter()
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithm()) {
                CryptoStream cs = new CryptoStream(_fileAccess.CreateWriter().BaseStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                return new System.IO.StreamWriter(cs);
            }
        }
    }
}
