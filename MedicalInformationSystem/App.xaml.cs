using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;

namespace MedicalInformationSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly UnityBootstrapper _medicalInformationSystemBootstrapper;

        public App()
        {
            _medicalInformationSystemBootstrapper = new MedicalServiceBootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _medicalInformationSystemBootstrapper.Run();
        }
    }
}
