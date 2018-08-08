using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static TawmFramework.Mapper;

namespace TawmFramework
{
    public static class DialogService 
    {

        public static bool? ShowDialog(Type viewModelType)
        {
            //get a mapping where there is a desired viewModel
            var mapping = Mappings.
                 Where(m => m.Value == viewModelType).FirstOrDefault();

            //checks if the type is a window
            //bind with a window binder if so
            if (Activator.CreateInstance(mapping.Key) is Window)
            {
                VVMBinder<Window> binder = new VVMBinder<Window>(mapping.Key, mapping.Value);

                //gets all the buttons with content or name starting with show or open. 
                //making event subscribe to the click event, that will open the window with name specified after show or open.
                foreach (var bt in binder.ShowDialogButtons)
                    bt.Click += (sender, e) =>
                    {
                        ShowWindowDialog(sender, e);
                    };

                //same as of the buttons but here for the menuitems
                foreach (var mn in binder.ShowDialogMenuItems)
                    mn.Click += (sender, e) =>
                    {
                        ShowWindowDialog(sender, e);
                    };

                //add the mapping to the dictionary of open mappings
                OpenDialogMappings.Add(binder.View as IDialog, viewModelType);

                //show the dialog
                return (binder.View as IDialog).ShowDialog();
            }
            else
            {
                return false;
            }

            //event handler
            void ShowWindowDialog(object sender, RoutedEventArgs e)
            {
                string windowName = null;

                if (sender is Button but)
                    windowName = but.Name;
                else if (sender is MenuItem mni)
                    windowName = mni.Name;

                windowName = windowName.Replace("Open", "").Replace("Show", "").Replace("Window", "").Replace("Dialog", "");

                Type type = Mappings.Where(m => m.Key.Name.StartsWith(windowName)).FirstOrDefault().Value;
                if (type != null)
                {
                    ShowDialog(type);
                }
            }
        }
        public static bool? ShowDialog(object viewModel)
        {
            var mapping = Mappings.
                 Where(m => m.Value == viewModel.GetType()).FirstOrDefault();
            if (mapping.Key == null)
                return false;
            if (Activator.CreateInstance(mapping.Key) is Window)
            {
                VVMBinder<Window> binder = new VVMBinder<Window>(mapping.Key, viewModel);


                foreach (var bt in binder.ShowDialogButtons)
                    bt.Click += (sender, e) =>
                    {
                        ShowWindowDialog_Click(sender, e);
                    };

                foreach (var mn in binder.ShowDialogMenuItems)
                    mn.Click += (sender, e) =>
                    {
                        ShowWindowDialog_Click(sender, e);
                    };

                OpenDialogMappings.Add(binder.View as IDialog, viewModel.GetType());
                return (binder.View as IDialog).ShowDialog();
            }
            else
            {

                return false;
            }

        void ShowWindowDialog_Click(object sender, RoutedEventArgs e)
            {
                string windowName = (sender as Button).Name.Replace("Open", "").Replace("Show", "");
                Type type = Mappings.Keys.Where(k => k.Name == windowName).FirstOrDefault();
                if (type != null)
                {

                    ShowDialog(type);
                }
            }
        }

        public static void HideDialog(Type viewModelType)
        {
            var targetDialog = OpenDialogMappings.Where(d => d.Value == viewModelType).Select(d => d.Key).FirstOrDefault();
            if (targetDialog != null)
            {
                targetDialog.Hide();
            }
        }

        public static void CloseDialog(Type viewModelType)
        {
            var targetDialog = OpenDialogMappings.Where(d => d.Value == viewModelType).Select(d => d.Key).FirstOrDefault();
            if (targetDialog != null)
            {
                targetDialog.Close();
                OpenDialogMappings.Remove(targetDialog);
            }
        }

        public static void Show(Type viewModelType)
        {

            var targetDialog = OpenDialogMappings.Where(d => d.Value == viewModelType).Select(d => d.Key).FirstOrDefault();
            if (targetDialog != null)
            {
                targetDialog.Show();
            }
        }
    }
}
