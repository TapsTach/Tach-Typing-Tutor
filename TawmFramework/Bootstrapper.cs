using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TawmFramework
{
    public static class Bootstrapper
    {
        static List<Window> windowsList = new List<Window>();
        static Dictionary<Type, Type> Mappings;
     
        public static void Load(Type viewModelClassType)
        {
            Mappings = Mapper.Mappings;

            //gets the assembly of the given viewModel type
            var assm = viewModelClassType.Assembly;

            //uses the assembly to retrieve all public types wich might be view models, according to the naming convertion
            var viewModelsTypes = assm.GetExportedTypes();
            var viewModels = viewModelsTypes.Where(t =>
            t.FullName.ToLower().EndsWith("viewmodel")
            || t.FullName.ToLower().EndsWith("vm")
            || t.FullName.ToLower().EndsWith("viewm")
            || t.FullName.ToLower().EndsWith("vmodel"));

            //gets
            var dialogs = Assembly.GetCallingAssembly().GetExportedTypes().Where(type =>
                type.FullName.ToLower().EndsWith("window")
                || type.FullName.ToLower().EndsWith("dialog")
                || type.FullName.ToLower().EndsWith("view")
                || type.FullName.ToLower().EndsWith("page")
                || type.FullName.ToLower().EndsWith("control"));

            foreach (var dialog in dialogs)
            {
                var formattedDialogName = dialog.Name
                    .Replace("Window", "")
                    .Replace("Dialog", "")
                    .Replace("View", "")
                    .Replace("Page", "")
                    .Replace("Control", "");

                var match = viewModels.Where(vm => vm.Name
                    .ToLower()
                    .Replace("viewmodel", "")
                    .Replace("vm", "")
                    .Replace("viewm", "")
                    .Replace("vmodel", "")
                    .Equals(formattedDialogName.ToLower()))
                    .FirstOrDefault();

                if (match != null)
                {
                    Mappings.Add(dialog, match);
                }
            }

        }

        /// <summary>
        /// Loads view and view model mappings.
        /// And starts the window of the type specified
        /// </summary>
        /// <param name="windowTypeClass">class from the window you want to open</param>
        public static void Start(Type windowTypeClass)
        {
            //get the viewModelType associated with the given windowType
            var viewModelType = Mappings[windowTypeClass];

            //use the viewModelType to show the dialog
            DialogService.ShowDialog(viewModelType);
        }


    }
}