using CoreLib;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class LockAccountVM : VMBase
    {
        public ICommand CancelCommand { get; set; }
        public ICommand LockCommand { get; set; }

        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public LockAccountVM()
        {
            CancelCommand = new RelayCommand(() =>
            {
                Close();
            });
            LockCommand = new RelayCommand(() =>
            {

                if (Password == PasswordConfirmation)
                {
                    StudentRepository.LockLoggedUserAccount(Password);

                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        Notifier.ShowMessage("Unlocked", "Success");
                    }
                    else
                    {
                        Notifier.ShowMessage("Locked", "Success");
                    }
                }
                else
                {
                    Notifier.ShowMessage("Password mismatch", "Invalid");
                    return;
                }
                Close();
            });
        }
        void Close()
        {
            DialogService.CloseDialog(GetType());
        }
    }
}
