namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IEntityControllerFactory<in TEntity, out TController>
    {
        TController CreateFrom(TEntity entity);
    }
}
