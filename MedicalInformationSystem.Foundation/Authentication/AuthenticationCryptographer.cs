using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;

namespace MedicalInformationSystem.Foundation.Authentication
{
    [UsedImplicitly]
    public class AuthenticationCryptographer : IAuthenticationCryptographer
    {
        private const int SaltBytesCount = 32;

        private readonly ICryptographicRng _cryptographicRng;
        private readonly ISha512Cryptographer _sha512Cpyptoghrapher;

        public AuthenticationCryptographer(ICryptographicRng cryptographicRng, ISha512Cryptographer sha512Cryptographer)
        {
            _cryptographicRng = cryptographicRng;
            _sha512Cpyptoghrapher = sha512Cryptographer;
        }

        public string ComputeHash(string passwordSalt, string password)
        {
            return _sha512Cpyptoghrapher.ComputeHash(passwordSalt + password);
        }

        public string GeneratePasswordSalt()
        {
            return _cryptographicRng.GenerateRandomString(SaltBytesCount);
        }
    }
}
