using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using System;
using System.Security.Cryptography;

namespace MedicalInformationSystem.Foundation.Cryptography
{
    [UsedImplicitly]
    public class CryptographicRng : ICryptographicRng
    {
        public string GenerateRandomString(int bytesCount)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[bytesCount];
                rng.GetBytes(buffer);

                return Convert.ToBase64String(buffer);
            }
        }
    }
}
