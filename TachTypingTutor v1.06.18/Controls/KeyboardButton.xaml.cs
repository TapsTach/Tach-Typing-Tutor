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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for KeyboardButton.xaml
    /// </summary>
    public partial class KeyboardButton : UserControl
    {
        #region HighlightingColor
        public Brush HighlightingColor
        {
            get { return (Brush)GetValue(HighlightingColorProperty); }
            set { SetValue(HighlightingColorProperty, value); }
        }
        public static readonly DependencyProperty HighlightingColorProperty =
            DependencyProperty.Register("HighlightingColor", typeof(Brush), typeof(KeyboardButton), new PropertyMetadata(Brushes.LightGreen, HighlightingColorPropertyChanged));

        private static void HighlightingColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var kbtn = sender as KeyboardButton;
            kbtn.rectHighlight.Stroke = (Brush)e.NewValue;
            kbtn.rectBackHighlight.Fill = (Brush)e.NewValue;
            kbtn.rectBackHighlight.Stroke = null;
        }


        #endregion

        #region highlighted
        public bool Highlighted
        {
            get { return (bool)GetValue(HighlightedProperty); }
            set { SetValue(HighlightedProperty, value); }
        }
        public static readonly DependencyProperty HighlightedProperty =
            DependencyProperty.Register("Highlighted", typeof(bool), typeof(KeyboardButton), new PropertyMetadata(false, HighlightedPropertyChanged));

        private static void HighlightedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var kbtn = d as KeyboardButton;
            bool value = (bool)e.NewValue;
            if (value)
            {
                kbtn.rectHighlight.Visibility = Visibility.Visible;
                kbtn.rectBackHighlight.Visibility = Visibility.Visible;
            }
            else
            {
                kbtn.rectHighlight.Visibility = Visibility.Collapsed;
                kbtn.rectBackHighlight.Visibility = Visibility.Collapsed;
            }
        }


        #endregion

        #region SizeProperty
        public Size Size
        {
            get { return (Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(Size), typeof(KeyboardButton), new PropertyMetadata(new Size(0, 0), ButtonSizeChanged));

        static void ButtonSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var kbtn = sender as KeyboardButton;
            var size = (Size)e.NewValue;

            kbtn.Width = size.Width;
            kbtn.Height = size.Height;

            //make the inner rectangle 23 percent smaller.
            double sizePerc = 0;
            if (size.Height > size.Width)
                sizePerc = size.Width * .23;
            else sizePerc = size.Height * .23;

            kbtn.innerRect.Height = size.Height - sizePerc;
            kbtn.innerRect.Width = size.Width - sizePerc;

            //make sure the textblockes are inside the inner rectangle

            double perc = (kbtn.Height * .075);
            kbtn.txtUpper.Margin = new Thickness(perc * 2 + perc, perc, 0, 0);
            kbtn.txtLower.Margin = new Thickness(0, 0, perc * 2 + perc, perc * 2);
        }
        #endregion

        #region IsPressedProperty
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(KeyboardButton), new PropertyMetadata(false, IsPressedPropertyChanged));

        static void IsPressedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var kbtn = sender as KeyboardButton;
            var value = (bool)e.NewValue;
            Size size = kbtn.Size;
            if (value)
            {
                // kbtn.outerCan.RenderTransform = transTransform;
                kbtn.outerCan.RenderTransform = scaleTrans;
            }
            else
            {
                //kbtn.outerCan.RenderTransform = null;
                kbtn.outerCan.RenderTransform = null;

            }
        }
        #endregion
        #region BumbHighligthColor
        public Brush BumpHighlightColor
        {
            get { return (Brush)GetValue(BumpHighlightColorProperty); }
            set { SetValue(BumpHighlightColorProperty, value); }
        }

        public static readonly DependencyProperty BumpHighlightColorProperty =
            DependencyProperty.Register("BumpHighlightColor", typeof(Brush), typeof(KeyboardButton), new PropertyMetadata(Brushes.Black, (sender, e) =>
            {
                KeyboardButton kbb = sender as KeyboardButton;
                kbb.bump.Stroke = (Brush)e.NewValue;
            }
            ));
        #endregion

        public string UpperText
        {
            get => upperText;
            set
            {
                upperText = value;
                txtUpper.Text = value;
            }
        }
        public string LowerText
        {
            get => lowerText;
            set
            {
                lowerText = value;
                txtLower.Text = value;
            }
        }
        public string MiddleText
        {
            get => middleText;
            set
            {
                middleText = value;
                txtMiddle.Text = value;
            }
        }

        public bool HasBump
        {
            get => hasBump;
            set
            {
                hasBump = value;
                if (value)
                {
                    bump.Visibility = Visibility.Visible;
                }
                else
                {
                    bump.Visibility = Visibility.Collapsed;
                }
            }
        }

        string upperText, lowerText, middleText;
        bool hasBump;
        // static TranslateTransform transTransform;


        static ScaleTransform scaleTrans;
        public KeyboardButton()
        {
            InitializeComponent();
            //  transTransform = new TranslateTransform()
            // {
            //     X = Height * .15
            ///  };
            scaleTrans = new ScaleTransform()
            {
                ScaleX = .95,
                ScaleY = .9
            };
        }
    }
}