using CoreLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class PerfomanceViewModel : VMBase
    {
        #region Average statistics properties
        public double AverageAccuracy
        {
            get => LessonStatisticsStore.GetAverage().Accuracy;
            set { }
        }

        public double AverageTrueAccuracy
        {
            get => LessonStatisticsStore.GetAverage().TrueAccuracy;
            set { }
        }

        public double AverageWpmSpeed
        {
            get => LessonStatisticsStore.GetAverage().SpeedWpm;
            set { }
        }

        public double AverageCpmSpeed
        {
            get => LessonStatisticsStore.GetAverage().SpeedCpm;
            set { }
        }

        public int TotalErrors
        {
            get => (int)(LessonStatisticsStore.GetAverage().Entries -LessonStatisticsStore.GetAverage().Correct);
            set { }
        }

        public int TotalCorrect
        {
            get => LessonStatisticsStore.GetAverage().Correct;
            set { }
        }

        public long TotalEntries
        {
            get => LessonStatisticsStore.GetAverage().Entries;
            set { }
        }

        public string TotalTimeTaken
        {
            get => LessonStatisticsStore.GetAverage().TimeTakenString;
            set { }
        }

        public int TotalLength
        {
            get => LessonStatisticsStore.GetAverage().LessonLength;
            set { }
        }
        #endregion


        #region  History properties
        public List<LessonStatistics> SessionHistoryItems
        {
            get => LessonStatisticsStore.GetStatistics();
         
        }

        public List<Statistics> AlltimeHistoryItems
        {
            get => StatisticsRepository.GetStatistics().OrderBy(l=>l.Id).ToList();
        }

        #endregion

        #region Character
        enum SortMode
        {
            Slowest, LeastAccurate, LeastEntered, None
        }
        SortMode mode;

        List<PerfMonLetter> letters;

        public ICommand SpeedCommand { get; set; }
        public ICommand AccuracyCommand { get; set; }
        public ICommand EntriesCommand { get; set; }

        public void Slowest()
        {
            if (mode == SortMode.Slowest)
            {
                Letters = PerfomanceMonitor.GetLetters().OrderByDescending(l => l.AverageTime).ToList();
                mode = SortMode.None;
                ModeLabel = "Sorted By Slowest Descending";
            }
            else
            {
                Letters = PerfomanceMonitor.GetLetters().OrderBy(l => l.AverageTime).ToList();
                mode = SortMode.Slowest;
                ModeLabel = "Sorted By Slowest Ascending";
            }
        }

        public void LeastAccurate()
        {
            if (mode == SortMode.LeastAccurate)
            {
                Letters = PerfomanceMonitor.GetLetters().OrderByDescending(l => l.Accuracy).ToList();
                mode = SortMode.None;
                ModeLabel = "Sorted By Accuracy Descending";
            }
            else
            {
                Letters = PerfomanceMonitor.GetLetters().OrderBy(l => l.Accuracy).ToList();

                mode = SortMode.LeastAccurate;
                ModeLabel = "Sorted By Accuracy Ascending";
            }
        }

        public void LeastEntered()
        {
            if (mode == SortMode.LeastEntered)
            {
                Letters = PerfomanceMonitor.GetLetters().OrderByDescending(l => l.Entries).ToList();
                mode = SortMode.None;
                ModeLabel = "Sorted By Entries Descending";
            }
            else
            {
                Letters = PerfomanceMonitor.GetLetters().OrderBy(l => l.Entries).ToList();
                mode = SortMode.LeastEntered;
                ModeLabel = "Sorted By Entries Ascending";
            }
        }

        public List<PerfMonLetter> Letters
        {
            get => letters;
            set
            {
                letters = value;
                Notify();
            }
        }
        string modeLabel;
        public string ModeLabel
        {
            get => modeLabel; set
            {
                modeLabel = value;
                Notify();
            }
        }
        #endregion
        public ICommand CloseCommand { get; set; }

        #region constructor
        public PerfomanceViewModel()
        {
            CloseCommand = new RelayCommand(() => DialogService.CloseDialog(GetType()));
            SpeedCommand = new RelayCommand(() => Slowest());
            EntriesCommand = new RelayCommand(() => LeastEntered());
            AccuracyCommand = new RelayCommand(() => LeastAccurate());
            Letters = PerfomanceMonitor.GetLetters().OrderBy(l => l.CharacterString).ToList();
        }

        #endregion
    }
}
