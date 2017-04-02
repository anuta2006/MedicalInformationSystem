using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalInformationSystemAgent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly UnityBootstrapper _medicalInformationAgentBootstrapper;


        public App()
        {
            _medicalInformationAgentBootstrapper = new MedicalServiceBootstrapper();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _medicalInformationAgentBootstrapper.Run();
        }
    }
}
