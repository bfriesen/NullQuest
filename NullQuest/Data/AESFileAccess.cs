using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.IO;

namespace NullQuest.Data
{
    class AESFileAccess : IFileAccess
    {
        private readonly IFileAccess _fileAccess;
        private readonly bool _disableEncryption;

        public AESFileAccess(IFileAccess fileAccess, bool disableEncryption)
        {
            _fileAccess = fileAccess;
            _disableEncryption = disableEncryption;
        }

        public bool DoesFileExist
        {
            get { return _fileAccess.DoesFileExist; }
        }

        public StreamReader CreateReader()
        {
            try
            {
                // If we *can* read the file as encrypted use that.
                CreateEncryptedReader().ReadToEnd();
                return CreateEncryptedReader();
            }
            catch
            {
                return _fileAccess.CreateReader();
            }
        }

        public StreamWriter CreateWriter()
        {
            if (_disableEncryption)
            {
                return _fileAccess.CreateWriter();
            }
            else
            {
                using (SymmetricAlgorithm algorithm = GetAlgorithm())
                {
                    CryptoStream cs = new CryptoStream(_fileAccess.CreateWriter().BaseStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                    return new StreamWriter(cs);
                }
            }
        }

        private StreamReader CreateEncryptedReader()
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithm())
            {
                CryptoStream cs = new CryptoStream(_fileAccess.CreateReader().BaseStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
                return new StreamReader(cs);
            }
        }

        private SymmetricAlgorithm GetAlgorithm()
        {
            Rijndael algo = Rijndael.Create();
            algo.Padding = PaddingMode.PKCS7;
            algo.Key = Convert.FromBase64String("LEWHRsdfhewrkh34lkRHah==");
            algo.IV = new byte[] { 43, 125, 183, 126, 71, 164, 226, 45, 246, 20, 161, 9, 71, 126, 120, 219 };
            return algo;
        }
    }
}
