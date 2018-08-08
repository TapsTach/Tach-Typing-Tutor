using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static CoreLib.Runner;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        #region Fields
        internal Canvas Container;

        GraphLine speedLine;
        GraphLine accuracyLine;
        GraphLine trueAccuracyLine;

        DispatcherTimer timer = new DispatcherTimer();
        Rectangle rec;
        #endregion

        public Graph()
        {
            this.InitializeComponent();
            this.Container = this.can;
            this.timer.Interval = TimeSpan.FromMilliseconds(1000);
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.speedLine = new GraphLine(this) { Color = Brushes.Red };
            this.accuracyLine = new GraphLine(this) { Color = Brushes.Green };
            this.trueAccuracyLine = new GraphLine(this) { Color = Brushes.Yellow };

        }

        private void Rec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            Rectangle r = sender as Rectangle;
            // only open the color dialog if the user clicks the rectangle twice
            if (rec != null && rec == r)
            {
                if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.Drawing.Color color = cd.Color;
                    Brush b = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                    r.Fill = b;
                }
                rec = null;
            }
            else rec = r;
        }

        #region DependencyProperties

        #region State
        
        public RunnerState State
        {
            get { return (RunnerState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(RunnerState), typeof(Graph), new PropertyMetadata(RunnerState.Iddle, (d, e) =>
            {
                var graph = d as Graph;
                switch ((RunnerState)e.NewValue)
                {
                    case RunnerState.Paused:
                        graph.timer.Stop();
                        break;
                    case RunnerState.Running:
                        graph.timer.Start();
                        break;
                    case RunnerState.Iddle:
                        graph.timer.Stop();
                        graph.Clear();
                        break;
                }
            }));


        #endregion

        #region SpeedWpmProperty
        public double SpeedWpm
        {
            get { return (double)GetValue(SpeedWpmProperty); }
            set { SetValue(SpeedWpmProperty, value); }
        }
        public static readonly DependencyProperty SpeedWpmProperty =
            DependencyProperty.Register("SpeedWpm", typeof(double), typeof(Graph), new PropertyMetadata(0d));

        #endregion

        #region AccuracyProperty
        public double Accuracy
        {
            get { return (double)GetValue(AccuracyProperty); }
            set { SetValue(AccuracyProperty, value); }
        }

        public static readonly DependencyProperty AccuracyProperty =
            DependencyProperty.Register("Accuracy", typeof(double), typeof(Graph), new PropertyMetadata(0d));

        #endregion

        #region TrueAccuracyProperty


        public double TrueAccuracy
        {
            get { return (double)GetValue(TrueAccuracyProperty); }
            set { SetValue(TrueAccuracyProperty, value); }
        }

        public static readonly DependencyProperty TrueAccuracyProperty =
            DependencyProperty.Register("TrueAccuracy", typeof(double), typeof(Graph), new PropertyMetadata(0d));
        #endregion
        #endregion

        #region Methods

        void Clear()
        {
            speedLine.Reset();
            accuracyLine.Reset();
            trueAccuracyLine.Reset();
        }
        void Stop()
        {
            this.timer.Stop();
            this.Container.Children.Clear();
            this.speedLine.Reset();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.speedLine.Update(SpeedWpm);
            this.accuracyLine.Update(Accuracy);
            this.trueAccuracyLine.Update(TrueAccuracy);
        }
        #endregion
    }
    public class GraphLine
    {
        private double X;

        private double X2;

        private double Y;

        private double Y2;

        private DoubleAnimation ruleAnimation;

        private Graph parent;

        private Brush color;

        private double thickness;

        private Canvas can;

        private List<Line> lines = new List<Line>();

        public Brush Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                foreach (Line v in this.lines)
                {
                    v.Stroke = value;
                }
            }
        }

        public double Thickness
        {
            get
            {
                return this.thickness;
            }
            set
            {
                this.thickness = value;
                foreach (Line v in this.lines)
                {
                    v.StrokeThickness = value;
                }
            }
        }

        public GraphLine(Graph parent)
        {
            this.Reset();
            this.parent = parent;
            this.can = parent.Container;
            this.thickness = 1.5;
            this.ruleAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1)
            };
        }

        internal void Reset()
        {
            double num = 32;
            double num1 = num;
            this.X2 = num;
            this.X = num1;
            double num2 = 0;
            num1 = num2;
            this.Y2 = num2;
            this.Y = num1;
            foreach (Line l in lines)
                can.Children.Remove(l);
            this.lines.Clear();
        }

        public void Update(double value)
        {
            if (this.X2 + 10 >= this.can.ActualWidth)
            {
                this.Reset();
            }
            this.X2 += 10;
            Line ln = new Line()
            {
                Stroke = this.color,
                StrokeThickness = this.thickness,
                X1 = this.X,
                X2 = this.X,
                Y1 = this.Y2,
                Y2 = value
            };
            this.ruleAnimation.From = new double?(this.X);
            this.ruleAnimation.To = new double?(this.X2);
            this.can.Children.Add(ln);
            this.lines.Add(ln);
            ln.BeginAnimation(Line.X2Property, this.ruleAnimation);
            this.X = this.X2;
            this.Y2 = value;
        }
    }
}
