using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TawmFramework
{
    static class Mapper
    {
        /// <summary>
        /// Stores mappings of view and viewModel
        /// The key is the view and value is the viewModel
        /// </summary>
        static internal Dictionary<Type, Type> Mappings { get; } 
        static internal Dictionary<IDialog, Type> OpenDialogMappings { get; set; } 

        static Mapper()
        {
            Mappings = new Dictionary<Type, Type>(); 
            OpenDialogMappings = new Dictionary<IDialog, Type>();
        }

        /// <summary>
        /// Gets Type of a view model associated with the given dialog type
        /// </summary>
        /// <param name="dialogType"></param>
        /// <returns></returns>
        public static Type GetViewType(Type dialogType)
        {
            return Mappings.Where(m => m.Key == dialogType).FirstOrDefault().Value;
        }


        /// <summary>
        /// Gets Type of a view model associated with the given dialog type
        /// And create return its instance
        /// </summary>
        /// <param name="dialogType"></param>
        /// <returns></returns>
        public static object GetViewModelTypeInstance(Type dialogType)
        {
            return Activator.CreateInstance(GetViewType(dialogType));
        }

        /// <summary>
        /// Gets the <![CDATA[Type]]> of a dialog associated with the given viewModel
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        public static Type GetDialogType(Type viewModelType)
        {
            return Mappings.Where(m => m.Value == viewModelType).FirstOrDefault().Key;
        }

        /// <summary>
        /// Create a new instance of the dialog using view model type to create the appropiate dialog
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        public static object GetDialogInstance(Type viewModelType)
        {
            return Activator.CreateInstance(GetDialogType(viewModelType));
        }

        /// <summary>
        /// Gets a dialog from a dictionary of opened dialogs
        /// Using a view model type given
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        public static IDialog GetOpenedDialog(Type viewModelType)
        {
            return OpenDialogMappings.Where(m => m.Value == (viewModelType)).FirstOrDefault().Key;
        }
    }
}
