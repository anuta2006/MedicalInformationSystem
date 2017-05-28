using MedicalInformationSystem.UI.Views;
using MedicalInformationSystem.UI.Views.Class;
using MedicalInformationSystem.UI.Views.Edit;
using MedicalInformationSystem.UI.Views.Reports;
using MedicalInformationSystem.UI.Views.StartUp;
using MedicalInformationSystem.UI.Views.Student;
using MedicalInformationSystem.UI.Views.StudentEdit;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;

namespace MedicalInformationSystem.UI
{
    public class MedicalInformationSystemMainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MedicalInformationSystemMainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(LandingView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(SignUpView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(SignInView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(MainView));

            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.StudentRegion, typeof(StudentsView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.StudentRegion, typeof(StudentEditView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.StudentRegion, typeof(StudentReceptionView));

            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.ClassInfoRegion, typeof(ClassesInfoView));

            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.ReportsRegion, typeof(ReportsView));

            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.EditRegion, typeof(EditView));

           // _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.StudentEditRegion, typeof(StudentEditView));

            _regionManager.Regions[MedicalInformationSystemRegions.MainRegion].NavigationService.RequestNavigate(new Uri(MedicalInformationSystemViews.LandingView, UriKind.Relative));
        }
    }
}
