using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CoreLib;
namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for InputTab.xaml
    /// </summary>
    public partial class InputTab : UserControl
    {
        List<Run> runs = new List<Run>();
        List<int> errorIndexs = new List<int>();
        int index = 0;
        List<Run> outRuns = new List<Run>();
        SolidColorBrush correctedBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF5F0D0D");
        SolidColorBrush brush = new SolidColorBrush(new Color()
        {
            A=120,
            R=127,
            G=127,
            B=127
        });
        bool fromWithin = false;
        private ColorAnimation colorAnimation;

        SolidColorBrush animatedBrush = new SolidColorBrush();
        public InputTab()
        {
            InitializeComponent();

            colorAnimation = new ColorAnimation()
            {
                From = Brushes.LightGreen.Color,
                To = Brushes.Black.Color,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        /// <summary>
        /// Appends text entered to the input tab.
        /// </summary>
        /// <param name="text"></param>
        internal void Append(string text)
        {
            TypingSounds.PlayTypingSound();
            //Perfom backspacing if the user presses backspace.
            if (text == "\b")
            {
                if (AllowBackspace)
                {
                    index = runs.Count - 1;
                    if (runs.Count > 0)
                    {
                        tblInput.Inlines.Remove(runs[index]);
                        runs.RemoveAt(index);
                    }
                }
            }
            else if (text == "\r")
            {
                return;
            }
            else
            {
                //Check for index bounds
                if (string.IsNullOrWhiteSpace(ComparisonText) || ComparisonText.Length == runs.Count)
                    return;
                Run run = new Run
                {
                    Foreground = new SolidColorBrush(new Color { A = animatedBrush.Color.A, B = animatedBrush.Color.B, R = animatedBrush.Color.R, G = animatedBrush.Color.G }),
                    Text = text
                };
                if (errorIndexs.Contains(index))
                    run.Foreground = correctedBrush;
                if (text != ComparisonText[runs.Count].ToString())
                {
                    TypingSounds.PlayErrorSound();
                    run.Foreground = Brushes.Red;
                    errorIndexs.Add(index);
                    if (text == " ")
                        run.Text = "_";
                    MisMatchCount++;
                    EntireMisMatchCount++;

                }
                runs.Add(run);
                tblInput.Inlines.Add(run);
                Strokes++;
                EnteredText = text;
            }

            fromWithin = true;
            EntryText = tblInput.Text;
            fromWithin = false;

            index = runs.Count;
            if (outRuns.Count > index)
                outRuns[index].Background = brush;
            try
            {
                outRuns[index - 1].Background = Brushes.Transparent;
            }
            catch { }

            PlaceOnCenter();
            Progress = ((double)EntryText.Length / ComparisonText.Length) * 100;
            if (Progress == 100)
                TypingSounds.PlayFinishSound();
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }


        void Clear()
        {
            runs.Clear();
            errorIndexs.Clear();
            outRuns.Clear();
            tblInput.Text = "";
            Strokes = 0;
            index = 0;
            double borderMid = border.ActualWidth / 2;
            tblInput.Margin = tblOutput.Margin = new Thickness(borderMid, 0, 0, 0);

            Progress = 0d;
        }
        /// <summary>
        /// places the input tab to the center of the screen
        /// </summary>
        private void PlaceOnCenter()
        {
            double borderMid = border.ActualWidth / 2;
            double center = borderMid - tblInput.ActualWidth;
            tblInput.Margin = tblOutput.Margin = new Thickness(center, 0, 0, 0);
        }

        #region Strokes


        public int Strokes
        {
            get { return (int)GetValue(StrokesProperty); }
            set { SetValue(StrokesProperty, value); }
        }


        public static readonly DependencyProperty StrokesProperty =
            DependencyProperty.Register("Strokes", typeof(int), typeof(InputTab), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion

        #region comparisonText
        /// <summary>
        /// Original text that the user has to copy from.
        /// </summary>
        public string ComparisonText
        {
            get { return (string)GetValue(ComparisonTextProperty); }
            set { SetValue(ComparisonTextProperty, value); }
        }

        public static readonly DependencyProperty ComparisonTextProperty =
            DependencyProperty.Register("ComparisonText", typeof(string), typeof(InputTab), new PropertyMetadata("", (d, e) =>
             {
                 var tab = d as InputTab;
                 string value = (string)e.NewValue;
                 tab.outRuns.Clear();
                 tab.tblOutput.Inlines.Clear();
                 tab.Clear();
                 foreach (var c in value)
                 {
                     tab.outRuns.Add(new Run() { Text = c.ToString() });
                 }
                 tab.tblOutput.Inlines.AddRange(tab.outRuns);
             }));

        #endregion

        #region EntryText

        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }

        public static readonly DependencyProperty EntryTextProperty =
     DependencyProperty.Register("EntryText", typeof(string), typeof(InputTab), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
     {
         var tab = d as InputTab;
         var text = e.NewValue as string;
         if (!tab.fromWithin)
         {
             tab.Clear();
             if (text != null)
                 foreach (var c in text)
                 {
                     tab.Append(c.ToString());
                 }
         }
     }));
        #endregion

        #region Progress
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(InputTab), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region EnteredText
        /// <summary>
        /// The text entered by the user.
        /// </summary>
        public string EnteredText
        {
            get { return (string)GetValue(EnteredTextProperty); }
            set { SetValue(EnteredTextProperty, value); }
        }

        public static readonly DependencyProperty EnteredTextProperty =
            DependencyProperty.Register("EnteredText", typeof(string), typeof(InputTab), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region AllowsBackspace
        public bool AllowBackspace
        {
            get { return (bool)GetValue(AllowBackspaceProperty); }
            set { SetValue(AllowBackspaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowBackspace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowBackspaceProperty =
            DependencyProperty.Register("AllowBackspace", typeof(bool), typeof(InputTab), new PropertyMetadata(false));


        #endregion
        #region fontsSize
        /// <summary>
        /// Well you know what a fontSize does
        /// </summary>
        public double InputFontSize
        {
            get { return (double)GetValue(InputFontSizeProperty); }
            set { SetValue(InputFontSizeProperty, value); }
        }

        public static readonly DependencyProperty InputFontSizeProperty =
            DependencyProperty.Register("InputFontSize", typeof(double), typeof(InputTab), new PropertyMetadata(0d, (d, e) =>
            {
                var tab = d as InputTab;
                var size = (double)e.NewValue;
                tab.tblInput.FontSize = size;
                tab.tblOutput.FontSize = size;
            }));
        #endregion


        #region EntireMisMatchCount
        public int EntireMisMatchCount
        {
            get { return (int)GetValue(EntireMisMatchCountProperty); }
            set { SetValue(EntireMisMatchCountProperty, value); }
        }

        public static readonly DependencyProperty EntireMisMatchCountProperty =
            DependencyProperty.Register("EntireMisMatchCount", typeof(int), typeof(InputTab), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion
        #region MisMatchCount
        public int MisMatchCount
        {
            get { return (int)GetValue(MisMatchCountProperty); }
            set { SetValue(MisMatchCountProperty, value); }
        }

        public static readonly DependencyProperty MisMatchCountProperty =
                    DependencyProperty.Register("MisMatchCount", typeof(int), typeof(InputTab), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion
    }
}
