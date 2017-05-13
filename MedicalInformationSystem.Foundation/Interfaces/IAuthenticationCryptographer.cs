namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IAuthenticationCryptographer
    {
        string GeneratePasswordSalt();

        string ComputeHash(string passwordSalt, string password);
    }
}
