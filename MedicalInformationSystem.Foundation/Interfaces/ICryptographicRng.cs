namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface ICryptographicRng
    {
        string GenerateRandomString(int bytesCount);
    }
}
