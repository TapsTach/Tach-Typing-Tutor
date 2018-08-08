using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

using static TawmFramework.ElementService;
using static TawmFramework.Binder;
namespace TawmFramework
{
    public class VVMBinder<T> where T:FrameworkElement
    { 
        #region Fields
        Type windowType, viewModelType;
        List<Button> buttons;
        List<TextBox> textControls;
        List<TextBlock> textBlocks;
        List<MenuItem> menuItems;
        #endregion

        #region Properties
        public T View { get; }
        public object ViewModel { get; }
        public List<Button> ShowDialogButtons { get; set; } = new List<Button>();
        public List<MenuItem> ShowDialogMenuItems { get; set; } = new List<MenuItem>();
        #endregion
        VVMBinder(Type windowType)
        {
            this.windowType = windowType;
            View = Activator.CreateInstance(windowType) as T;

            menuItems = FindElements<MenuItem>(View).ToList();
            buttons = FindElements<Button>(View).ToList();
            textBlocks = FindElements<TextBlock>(View).ToList();
            textControls = FindElements<TextBox>(View).ToList();
         
        }
        public VVMBinder(Type windowType, Type viewModelType) : this(windowType)
        {
           this.viewModelType = viewModelType;
            ViewModel = Activator.CreateInstance(viewModelType);
            Bind();
        }
        public VVMBinder(Type windowType, object viewModel):this(windowType)
        {
            this.ViewModel = viewModel;
            Bind();
        }
        void Bind()
        {
            if (View != null)
            {
                
                View.DataContext = ViewModel;

                List<Button> openerButtons = new List<Button>();
                List<MenuItem> openerMenuItems = new List<MenuItem>();

                BindButtonCommands(buttons, ViewModel, out openerButtons);
                BindTextBoxes(textControls, ViewModel);
                BindTextBlocks(textBlocks, ViewModel);
                BindMenuItemCommands(menuItems, ViewModel, out openerMenuItems);

                ShowDialogButtons = openerButtons;
                ShowDialogMenuItems = openerMenuItems;
               
            }
        }

        //void BindText()
        //{
        //    foreach (var textbox in textControls)
        //    {
        //        var textBoxName = textbox.Name;
        //        if (viewModel.GetType().GetProperty(textBoxName) != null)
        //        {
        //            Binding binding = new Binding(textBoxName);

        //            textbox.SetBinding(TextBox.TextProperty, binding);
        //        }
        //    }
        //}
        //void BindTextBlocks()
        //{
        //    foreach (var textbox in textBlocks)
        //    {
        //        var textBoxName = textbox.Name;
        //        if (viewModel.GetType().GetProperty(textBoxName) != null)
        //        {
        //            Binding binding = new Binding(textBoxName);
        //            textbox.SetBinding(TextBlock.TextProperty, binding);
        //        }
        //    }
        //}
       
        

    }
}
