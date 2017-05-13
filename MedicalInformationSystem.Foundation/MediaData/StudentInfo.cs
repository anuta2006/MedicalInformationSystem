namespace MedicalInformationSystem.Foundation.MediaData
{
    public class StudentInfo
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string Patronymic { get; }

        public StudentInfo(string firstName, string lastName, string patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }
    }
}
