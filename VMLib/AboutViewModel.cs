using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class AboutViewModel:VMBase
    {
        public ICommand CloseCommand { get; set; }
        public AboutViewModel()
        {
            CloseCommand = new RelayCommand(() =>
            {
                DialogService.CloseDialog(GetType());
            });
        }
    }
}
