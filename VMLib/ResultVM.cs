using System;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class ResultVM : VMBase
    {
        public enum RequestedCommand
        {
            Redo, Next
        }
        public double SpeedWpm { get; set; }
        public double SpeedCpm { get; set; }
        public double Accuracy { get; set; }
        public double TrueAccuracy { get; set; }

        public int Strokes { get; set; }
        public int LessonLength { get; set; }
        public int Errors { get; set; }
        public int Corrections { get; set; }
        public int TotalErrors { get; set; }
        public string ElapsedTime { get; set; }
        public int Rate { get; set; } = 0;
        public ICommand RedoCommand { get; set; }
        public ICommand NextCommand { get; set; }

        public event Action<RequestedCommand> RequestedAction;
        public ResultVM()
        {
            InitializeCommands();
        }
        void InitializeCommands()
        {
            RedoCommand = new RelayCommand(() =>
             {
                 RequestedAction?.Invoke(RequestedCommand.Redo);
                 Close();
             });

            NextCommand = new RelayCommand(() =>
            {
                RequestedAction?.Invoke(RequestedCommand.Next);
                Close();
            });


        }
        void Close()
        {
            DialogService.CloseDialog(GetType());
        }

        public void CalculateRate()
        {
            double rating = 0;
            if (TrueAccuracy > 99)
            {
                rating += 6;
            }
            else if (TrueAccuracy > 97)
            {
                rating += 5;
            }
            else if (TrueAccuracy > 95)
            {
                rating += 4.5;
            }
            else if (TrueAccuracy > 90)
            {
                rating += 3;
            }
            else if (TrueAccuracy > 80)
            {
                rating += 2;
            }
            else if (TrueAccuracy > 70)
            {
                rating += 1;
            }

            if (SpeedWpm > 80)
            {
                rating += 6;
            }
            else if (SpeedWpm > 60)
            {
                rating += 5;
            }
            else if (SpeedWpm > 50)
            {
                rating += 4;
            }
            else if (SpeedWpm > 40)
            {
                rating += 3.5;
            }
            else if (SpeedWpm > 30)
            {
                rating += 2.5;
            }
            else  if (SpeedWpm > 20)
            {
                rating += 1.5;
            }
            else if (SpeedWpm > 15)
            {
                rating += 1;
            }
            else if (SpeedWpm > 10)
            {
                rating -= 1;
            }
            else 
            {
                rating -= 2;
            }
            double d = rating / 2;
            Rate = (int)Math.Round(d);
        }
    }
}
