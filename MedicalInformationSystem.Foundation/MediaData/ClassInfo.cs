namespace MedicalInformationSystem.Foundation.MediaData
{
    public class ClassInfo
    {
        public string Letter { get; }

        public int Number { get; }

        public ClassInfo(string letter, int number)
        {
            Letter = letter;
            Number = number;
        }
    }
}
