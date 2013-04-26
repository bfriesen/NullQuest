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

        public bool DoesFileExist
        {
            get { return _fileAccess.DoesFileExist; }
        }

        public System.IO.StreamReader CreateReader()
        {
            Rijndael algorithm = Rijndael.Create();
            CryptoStream cs = new CryptoStream(_fileAccess.CreateReader().BaseStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            return new System.IO.StreamReader(cs);
        }

        public System.IO.StreamWriter CreateWriter()
        {
            Rijndael algorithm = Rijndael.Create();
            CryptoStream cs = new CryptoStream(_fileAccess.CreateWriter().BaseStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            return new System.IO.StreamWriter(cs);
        }
    }
}
