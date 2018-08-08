using CoreLib;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class SignInViewModel:VMBase
    {
        #region Properties
        public List<Student> Students { get => StudentRepository.GetStudents(); }

        public ICommand CreateNewUserCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Student Selected { get; set; }
        #endregion

        #region Constructor
        public SignInViewModel()
        {
            LoginCommand = new RelayCommand(() =>
            {
                if(Selected != null)
                {
                    StudentRepository.Logged = Selected;

                    if(!String.IsNullOrWhiteSpace(Selected.Password))
                    {
                        PasswordViewModel pvm = new PasswordViewModel();
                        DialogService.ShowDialog(pvm);
                        if(!pvm.Passed)
                        {
                            return;
                        }
                    }

                    DialogService.HideDialog(GetType());
                    DialogService.ShowDialog(typeof(MainVM));
                    DialogService.CloseDialog(GetType());
                }
            });

            CreateNewUserCommand = new RelayCommand(() => OpenCreateUser());

            CancelCommand = new RelayCommand(() =>
            {
                DialogService.CloseDialog(GetType());
            });
            if (Students.Count == 0)
                OpenCreateUser();
        }

        private void OpenCreateUser()
        {
            DialogService.HideDialog(GetType());
            DialogService.ShowDialog(typeof(CreateUserViewModel));
            DialogService.CloseDialog(GetType());
        }
        #endregion
    }
}
