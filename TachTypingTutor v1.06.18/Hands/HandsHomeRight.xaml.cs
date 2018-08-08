using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TachTypingTutor_v1._06._18.Hands
{
    /// <summary>
    /// Interaction logic for HandsHomeRight.xaml
    /// </summary>
    public partial class HandsHomeRight : UserControl
    {

        DoubleAnimation opAn;
        Path finger;
        public HandsHomeRight()
        {
            InitializeComponent();
            opAn = new DoubleAnimation()
            {
                To = .7,
                Duration = TimeSpan.FromMilliseconds(600),
                RepeatBehavior = RepeatBehavior.Forever,
            };
        }

        public void Highlight(string letter)
        {
            switch (letter)
            {
                case ";":
                    finger = ltHand.A;
                    break;
                case "L":
                    finger = ltHand.S;
                    break;
                case "K":
                    finger = ltHand.D;
                    break;
                case "J":
                    finger = J;
                    break;
                case " ":
                    finger = ltHand.LT;
                    break;
                default:
                    finger = null;
                    break;
            }
            if (finger != null)
                finger.Opacity = 0.5;
        }

        public void StopHighlight()
        {
            ltHand.StopHighlight();
            J.Opacity = 0;
        }
    }
}