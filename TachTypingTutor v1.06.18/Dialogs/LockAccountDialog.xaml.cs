using System.Windows.Controls;
using VMLib;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for LockAccountDialog.xaml
    /// </summary>
    public partial class LockAccountDialog : TWindow
    {
        public LockAccountDialog()
        {
            InitializeComponent();
        }

    
        private void PasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (DataContext == null)
                return;

            var viewModel = DataContext as LockAccountVM;
            if (viewModel == null)
                return;

            var passwordBox = sender as PasswordBox;

            if (passwordBox.Name == "password")
            {
                viewModel.Password = passwordBox.Password;
            }
            else
            {
                viewModel.PasswordConfirmation = passwordBox.Password;
                if (e.Key == System.Windows.Input.Key.Enter)
                    viewModel.LockCommand.Execute(null);
            }
        }
    }
}
