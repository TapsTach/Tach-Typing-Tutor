using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static TawmFramework.Mapper;
namespace TawmFramework
{
    public static class PageService
    {
        static List<KeyValuePair<string, object>> pageHistory = new List<KeyValuePair<string, object>>();
        public static void Navigate(Type parentViewModelType, string container, Type viewModelType)
        {
            //get a mapping where there is a desired viewModel
            var mapping = Mappings.
                 Where(m => m.Value == viewModelType).FirstOrDefault();

            //checks if the type is a window
            //bind with a window binder if so
            if (Activator.CreateInstance(mapping.Key) is Page)
            {
                VVMBinder<Page> binder = new VVMBinder<Page>(mapping.Key, mapping.Value);

                //gets all the buttons with content or name starting with show or open. 
                //making event subscribe to the click event, that will open the window with name specified after show or open.
                foreach (var bt in binder.ShowDialogButtons)
                    bt.Click += (sender, e) =>
                    {
                        ShowWindowDialog_Click(sender, e);
                    };

                //same as of the buttons but here for the menuitems
                foreach (var mn in binder.ShowDialogMenuItems)
                    mn.Click += (sender, e) =>
                    {
                        ShowWindowDialog_Click(sender, e);
                    };

                //add the mapping to the dictionary of open mappings
                // OpenDialogMappings.Add(binder.View as IDialog, viewModelType);

                //Navigate
                //pages can be hosted only within a frame or a window element
                Page page = binder.View;

                IDialog host = GetOpenedDialog(parentViewModelType);

                ///TODO TODO TODO HERE
                try
                {
                    var frame = ElementService.FindElements<Frame>(host as Window).Single(s => s.Name == container);
                    frame.Content = page;
                    pageHistory.Add(new KeyValuePair<string, object>(container, frame.Content));

                }
                catch
                {
                    throw new Exception($"{host.GetType().FullName} does not contain a Frame with name '{container}'");
                }


            }
            else
            {

            }

            //event handler
            void ShowWindowDialog_Click(object sender, RoutedEventArgs e)
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

                }
            }
        }

        public static void Navigate(Type parentViewModelType, string container)
        {




        }

        public static void GoBack(Type parentViewModelType, string container
            )
        {
            IDialog host = GetOpenedDialog(parentViewModelType);
            try
            {
                var contentHistory = pageHistory.Where(kvp => kvp.Key == container).Single();
                pageHistory.Remove(contentHistory);
                
                try
                {
                    var frame = ElementService.FindElements<Frame>(host as Window).Single(s => s.Name == container);
                    frame.Content = contentHistory.Value;

                }
                catch
                {
                    throw new ArgumentException($"{host.GetType().FullName} does not contain a Frame with name '{container}'");
                }
            }
            catch
            {
                throw new ArgumentException($"Navigation history does not contain a Frame with name '{container}'");
            }
            ///TODO TODO TODO HERE


        }

        public static void Next(Type type)
        {

        }
    }
}
