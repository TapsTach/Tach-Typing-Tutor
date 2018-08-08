using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace TachTypingTutor_v1._06._18
{
    public class NotifierButtonsVisibilityConvertor : ConvertorBase<NotifierButtonsVisibilityConvertor>
    {
        public static NotifierButtonsVisibilityConvertor Instance { get; set; }

         = new NotifierButtonsVisibilityConvertor();
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var buttonString = value.ToString();
            var parameterString  = parameter.ToString();
            Visibility vis = Visibility.Collapsed;
            
            switch(buttonString)
            {
                case "Ok":
                    if(parameterString == "Ok")
                    {
                        vis = Visibility.Visible;
                    }
                    break;
                case "YesNo":
                    if (parameterString == "Yes" || parameterString == "No")
                        vis = Visibility.Visible;
                    break;
                case "YesNoCancel":
                    if (parameterString == "Yes" || parameterString == "No" || parameterString == "Cancel")
                        vis = Visibility.Visible;
                    break;
                default:
                    Debugger.Break();
                    break;
            }
            return vis;
        }

       
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
