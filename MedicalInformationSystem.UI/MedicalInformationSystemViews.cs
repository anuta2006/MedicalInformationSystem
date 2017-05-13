namespace MedicalInformationSystem.UI
{
    public static class MedicalInformationSystemViews
    {
        private const string LandingViewName = nameof(Views.StartUp.LandingView);
        private const string SignUpViewName = nameof(Views.StartUp.SignUpView);
        private const string SignInViewName = nameof(Views.StartUp.SignInView);
        private const string MainViewName = nameof(Views.MainView);

        private const string StudentsViewName = nameof(Views.Student.StudentsView);

        private const string StudentEditViewName = nameof(Views.StudentEdit.StudentEditView);

        private const string ClassesInfoViewName = nameof(Views.Class.ClassesInfoView);

        public static string LandingView => LandingViewName;

        public static string SignUpView => SignUpViewName;

        public static string SignInView => SignInViewName;

        public static string MainView => MainViewName;

        public static string ClassInfoView => ClassesInfoViewName;

        public static string StudentsView => StudentsViewName;

        public static string StudentEditView => StudentEditViewName;
    }
}
