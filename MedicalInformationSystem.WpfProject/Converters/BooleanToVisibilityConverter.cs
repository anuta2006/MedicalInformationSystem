using System.Windows;

namespace MedicalInformationSystem.WpfProject.Converters
{
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
