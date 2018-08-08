using CoreLib;
using System;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class PasswordViewModel : VMBase
    {
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public bool Passed { get; set; }
        string password;
        public PasswordViewModel()
        {
            password = StudentRepository.Logged.Password;
            LoginCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    Passed = password == Password;
                    if (Passed)
                        Close();
                    else
                        Notifier.ShowMessage("Password Incorrect");
                }
                else
                    Notifier.ShowMessage("Please enter a password");
            });

            CancelCommand = new RelayCommand(() =>
            {
                Passed = false;
                Close();
            });
        }

        private void Close()
        {
            DialogService.CloseDialog(GetType());
        }
    }
}
