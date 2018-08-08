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

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for PercentageMeter.xaml
    /// </summary>
    public partial class PercentageMeter : UserControl
    {
        private double prevValue = -90;

        private double currValue = 0;

        private readonly static DependencyProperty ValueProperty;

        private DoubleAnimation angleAnimation;

        public string Header
        {
            get
            {
                return this.tblHeader.Text;
            }
            set
            {
                this.tblHeader.Text = value;
            }
        }

        public float HeaderFontSize
        {
            get
            {
                return (float)this.tblHeader.FontSize;
            }
            set
            {
                this.tblHeader.FontSize = (double)value;
            }
        }

        public double Value
        {
            get
            {
                return (double)base.GetValue(PercentageMeter.ValueProperty);
            }
            set
            {
                base.SetValue(PercentageMeter.ValueProperty, value);
            }
        }

        static PercentageMeter()
        {
            PercentageMeter.ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(PercentageMeter), new FrameworkPropertyMetadata(0d, new PropertyChangedCallback(PercentageMeter.OnPropertyChanged)));
        }

        public PercentageMeter()
        {
            this.InitializeComponent();
            this.angleAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(1000),
                From = new double?(this.prevValue),
                To = new double?(this.currValue)
            };
        }

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((PercentageMeter)sender).Update((double)e.NewValue);
        }

        public void Update(double value)
        {
            if (value >= 80)
            {
                this.currValue = value / 10 * 18 - 90;
            }
            else
            {
                this.currValue = value / 10 * 18.75 - 90;
            }
            this.tblValue.Text = Math.Round(value, 2).ToString();
            this.angleAnimation.From = new double?(this.prevValue);
            this.angleAnimation.To = new double?(this.currValue);
            this.meterLineAngleRotTrans.BeginAnimation(RotateTransform.AngleProperty, this.angleAnimation);
            this.prevValue = this.currValue;
        }
    }

}

