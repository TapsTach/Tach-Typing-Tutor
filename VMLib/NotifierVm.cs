using System;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public enum NotifierButtons
    {
        Ok, YesNo, YesNoCancel
    }
    public enum NotifierResult
    {
        Ok, Yes, No, Cancel
    }
    public class NotifierVm : VMBase
    {
        string message, title;
        NotifierButtons buttons;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                Notify();
            }
        }


        public string Title
        {
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }


        public NotifierButtons Buttons
        {
            get => buttons;
            set
            {
                buttons = value;
                Notify();
            }
        }
        public NotifierResult ResultButton { get; private set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }

        public NotifierVm(string message, string title = "", NotifierButtons buttons = NotifierButtons.Ok)
        {
            Message = message;
            Title = title;
            Buttons = buttons;
            InitializeCommands();

        }
        public NotifierVm()
        {
            InitializeCommands();
        }

        void InitializeCommands()
        {
            CloseCommand = CancelCommand = new RelayCommand(() =>
            {
                ResultButton = NotifierResult.Cancel;
                Close();
            });
            YesCommand = new RelayCommand(() =>
            {
                ResultButton = NotifierResult.Yes;
                Close();

            });
            NoCommand = new RelayCommand(() =>
            {

                ResultButton = NotifierResult.No;
                Close();
            });
            OkCommand = new RelayCommand(() =>
            {
                ResultButton = NotifierResult.Ok;
                Close();
            });
        }

        private void Close()
        {
            DialogService.CloseDialog(GetType());
        }

    }
    class Notifier
    {
        public static NotifierResult ShowMessage(string message, string title = "", NotifierButtons buttons = NotifierButtons.Ok)
        {
            var nvm = new NotifierVm(message, title, buttons);
            DialogService.ShowDialog(nvm);
            return nvm.ResultButton;
        }
    }
}
