using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MedicalInformationSystem.Foundation.Cryptography
{
    [UsedImplicitly]
    public class Sha512Cryptographer : ISha512Cryptographer
    {
        public string ComputeHash(string text)
        {
            using (var sha512Algorythm = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha512Algorythm.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
