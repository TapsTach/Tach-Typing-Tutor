using System.Windows;
using TawmFramework;

namespace TachTypingTutor_v1._06._18
{
    public class TWindow:Window, IDialog
    {
        public TWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;

            MouseLeftButtonDown += TWindow_MouseLeftButtonDown;
        }

        private void TWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}
