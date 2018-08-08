using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace TawmFramework
{
    public static class ElementService
    {
        public static List<T> FindElements<T>(UIElement container) where T : UIElement
        {
            List<T> elements = new List<T>();
            var children = LogicalTreeHelper.GetChildren(container).OfType<UIElement>();
            foreach (var child in children)
            {
                if (child is T but)
                {
                    elements.Add(but);
                }
                elements.AddRange(FindElements<T>(child));
            }
            return elements;
        }

      
    }
}
