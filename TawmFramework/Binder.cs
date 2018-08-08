using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace TawmFramework
{
    static class Binder
    {
        public static void BindButtonCommands(List<Button> buttons, object viewModel, out List<Button> openerButtons)
        {
            List<Button> outButtons = new List<Button>();
            foreach (var button in buttons)
            {

                string buttonName = "";
                if (string.IsNullOrWhiteSpace(button.Name))
                {
                    if (button.Content != null)
                        buttonName = button.Content.ToString().Replace(" ", "");
                }
                else buttonName = button.Name;

                if (button.Command == null)
                {
                    if (buttonName != null)
                    {
                        PropertyInfo vmCommandProperty = viewModel.GetType().GetProperty($"{buttonName}Command");
                        if (vmCommandProperty != null && typeof(ICommand).IsAssignableFrom(vmCommandProperty.PropertyType))
                        {
                            Binding propertyBinding = new Binding(vmCommandProperty.Name);
                            button.SetBinding(ButtonBase.CommandProperty, propertyBinding);
                        }
                        else if (buttonName.StartsWith("Open")
                          || buttonName.StartsWith("Show"))
                        {
                            string windowName = buttonName.Replace("Open", "").Replace("Show", "");

                            outButtons.Add(button);
                        }
                    }
                }
                else
                {
                    Debugger.Break();
                }
            }
            openerButtons = outButtons;
        }

        public static void BindTextBoxes(List<TextBox> textBoxes, object viewModel)
        {
            foreach (var textbox in textBoxes)
            {
                var textBoxName = textbox.Name;
                if (viewModel.GetType().GetProperty(textBoxName) != null)
                {
                    Binding binding = new Binding(textBoxName);
                    textbox.SetBinding(TextBox.TextProperty, binding);
                }
            }
        }

        public static void BindTextBlocks(List<TextBlock> textBlocks, object viewModel)
        {
            foreach (var textbox in textBlocks)
            {
                var textBoxName = textbox.Name;
                if (viewModel.GetType().GetProperty(textBoxName) != null)
                {
                    Binding binding = new Binding(textBoxName);
                    textbox.SetBinding(TextBlock.TextProperty, binding);
                }
            }
        }

        public static void BindMenuItemCommands(List<MenuItem> menuItems, object viewModel, out List<MenuItem> outMenuItems)
        {
            List<MenuItem> outMenu = new List<MenuItem>();
            foreach (var button in menuItems)
            {
                string buttonName = "";
                if (string.IsNullOrWhiteSpace(button.Name))
                {
                    if (button.Header != null)
                        buttonName = button.Header.ToString().Replace(" ", "");
                }
                else buttonName = button.Name;

                if (buttonName != null)
                {
                    PropertyInfo vmCommand = viewModel.GetType().GetProperty($"{buttonName}Command");
                    if (vmCommand != null)
                    {
                        Binding propertyBinding = new Binding(vmCommand.Name);
                        button.SetBinding(Button.CommandProperty, propertyBinding);
                    }
                    else if (buttonName.StartsWith("Open")
                      || buttonName.StartsWith("Show"))
                    {
                        string windowName = buttonName.Replace("Open", "").Replace("Show", "");

                        outMenu.Add(button);
                    }
                }
            }
            outMenuItems = outMenu;
        }
    }
}
