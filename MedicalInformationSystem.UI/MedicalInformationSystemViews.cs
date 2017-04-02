using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI
{
    public static class MedicalInformationSystemViews
    {
        private const string LandingViewName = nameof(Views.StartUp.LandingView);
        private const string SignUpViewName = nameof(Views.StartUp.SignUpView);
        private const string SignInViewName = nameof(Views.StartUp.SignInView);

        public static string LandingView => LandingViewName;

        public static string SignUpView => SignUpViewName;

        public static string SignInView => SignInViewName;
    }
}
