using System;

namespace MedicalInformationSystem.Services.DataContracts
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; }

        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
