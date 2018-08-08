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
    /// Interaction logic for HandsHomeLeft.xaml
    /// </summary>
    public partial class HandsHomeLeft : UserControl
    {
        DoubleAnimation opAn;
        Path finger;
        Path[] paths;
        public HandsHomeLeft()
        {
            InitializeComponent();
            opAn = new DoubleAnimation()
            {
                To = .7,
                Duration = TimeSpan.FromMilliseconds(600),
                RepeatBehavior = RepeatBehavior.Forever,
            };
            paths = new Path[] {
                A,S,D,F,LT
            };
        }

        public void Highlight(string letter)
        {
            switch (letter)
            {
                case "A":
                    finger = A;
                    break;
                case "S":
                    finger = S;
                    break;
                case "D":
                    finger = D;
                    break;
                case "F":
                    finger = F;
                    break;
                case "LT":
                    finger = LT;
                    break;
                default:
                    finger = null;
                    break;
            }
            if (finger != null)
                finger.Opacity = .5;
        }
        
        public void StopHighlight()
        {
            foreach (Path an in paths) an.Opacity = 0;
        }
    }
}
