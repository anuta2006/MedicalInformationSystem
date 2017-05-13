namespace MedicalInformationSystem.Services
{
    public enum AuthorizationResult
    {
        Success,
        UserWithSuchLoginDoesNotExist,
        IncorrectPassword,
        Failed
    }
}
