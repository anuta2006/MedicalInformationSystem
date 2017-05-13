namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface ISha512Cryptographer
    {
        string ComputeHash(string text);
    }
}
