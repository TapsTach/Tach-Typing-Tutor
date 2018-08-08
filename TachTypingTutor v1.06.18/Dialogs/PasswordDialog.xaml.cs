using VMLib;

namespace TachTypingTutor_v1._06._18.Dialogs
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : TWindow
    {
        public PasswordDialog()
        {
            InitializeComponent();
            viewModel = (PasswordViewModel)DataContext;
        }
        PasswordViewModel viewModel;
        private void passwordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(viewModel == null)
                viewModel = (PasswordViewModel)DataContext;
            viewModel.Password = passwordBox.Password;
        }
    }
}
