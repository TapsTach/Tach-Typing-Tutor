using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using CoreExtLib;
using CoreLib;
using TawmFramework;
using static CoreLib.MachineLessonHandler;
using static CoreLib.Runner;
using static CoreLib.Settings;
namespace VMLib
{
    public class MainVM : VMBase
    {
        #region Fields
        string enteredText;
        string entryText = "";

        int strokes;
        int idle = 0;
        double progress;
        double speedWPM, speedCPM, accuracy, trueAccuracy;
        Lesson lesson;
        Timer timer = new Timer();

        #endregion

        #region Properties

        #region Statistics Properties
        public double SpeedWpm
        {
            get => speedWPM;
            set
            {
                speedWPM = value;
                Notify();
            }
        }

        public double SpeedCpm
        {
            get => speedCPM;
            set
            {
                speedCPM = value;
                Notify();
            }
        }

        public double Accuracy
        {
            get => accuracy;
            set
            {
                accuracy = value;
                Notify();
            }
        }

        public double TrueAccuracy
        {
            get => trueAccuracy;
            set
            {
                trueAccuracy = value;
                Notify();
            }
        }

        public int Strokes
        {
            get => strokes;
            set
            {
                strokes = value;
                Notify();
            }
        }


        #endregion

        public string Username { get => StudentRepository.Logged.Username; }
        /// <summary>
        /// fontsize of the input tab
        /// </summary>
        public double FontSize
        {
            get => GetSettings().FontSize;
            set { }
        }

        /// <summary>
        /// Font weight of the  input tab
        /// </summary>
        public string FontWeight
        {
            get => GetSettings().FontWeight;
            set { }
        }
        /// <summary>
        /// Key entered
        /// </summary>
        public string EnteredText
        {
            get => enteredText;
            set
            {
                enteredText = value;
                if (Runner.State != RunnerState.Running)
                    Start();
                PerfomanceMonitor.Monitor(value, NextCharacterString);
                MusicPlayer.ResumeAsync();
                idle = 0;
                Notify();
            }
        }
        public bool AllowBackspace
        {
            get => Settings.GetSettings().AllowBackspace;
            set { }
        }
        /// <summary>
        /// all of the text entered by the user
        /// </summary>
        public string EntryText
        {
            get => entryText;
            set
            {
                entryText = value;
                Notify();
                Notify(nameof(NextCharacterString));
            }
        }
        public string NextCharacterString
        {
            get
            {
                if (lesson == null)
                    return "";
                if (entryText.Length < lesson.Text.Length)
                    return lesson.Text[entryText.Length].ToString();

                return "";
            }
            set { }
        }
        /// <summary>
        /// progress
        /// </summary>
        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                if (progress == 100)
                {
                    FinishUpAsync();
                }
                Notify();
            }
        }

        public TimeSpan ElapsedTimeSpan
        {
            get => RunningTime;
            set { }
        }


        public RunnerState State
        {
            get => Runner.State;
            set { }
        }

        /// <summary>
        /// current lesson
        /// </summary>
        public Lesson Lesson
        {
            get => lesson;
            set
            {

                lesson = value;
                if (lesson != null)
                    lesson.Text = lesson.Text.TrimStart().TrimEnd();
                Notify();
            }
        }

        public string StartLabel
        {
            get
            {
                switch (State)
                {
                    case RunnerState.Paused:
                        return "Resume";
                    case RunnerState.Running:
                        return "Pause";
                    case RunnerState.Iddle:
                        return "Start";
                    default:
                        return "Start";

                }
            }
            set { }
        }


        #endregion
        #region Machine properties

        public bool UIEnabled
        {
            get => UiEnabled;
            set { }
        }

        public DesiredView View
        {
            get => MachineLessonHandler.View;
            set { }
        }
        #endregion

        #region CommandProperties
        public ICommand StartCommand { get; set; }
        /// <summary>
        /// navigate to the previous lesson
        /// </summary>
        public RelayCommand PreviousCommand { get; set; }

        /// <summary>
        /// navigate to the next lesson
        /// </summary>
        public RelayCommand NextCommand { get; set; }

        /// <summary>
        /// pause the lesson
        /// </summary>
        public RelayCommand PauseCommand { get; set; }

        /// <summary>
        /// restarts the lesson
        /// </summary>

        public RelayCommand RestartCommand { get; set; }

        public RelayCommand ChooseCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }
        public ICommand ImportCommand { get; set; }

        public RelayCommand DeleteAccountCommand { get; set; }
        public RelayCommand ClearDataCommand { get; set; }

        #endregion
        #region Constructors
        public MainVM()
        {

            InitializeCommands();
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
            SettingsChangedRaised += Settings_SettingsChangedRaised;
            ActionExecuted += MainVM_ActionExecuted;
        }

        private void MainVM_ActionExecuted(CommandAction obj)
        {
            if (obj == CommandAction.lesson)
            {
                Lesson = MachineLessonHandler.Lesson;
                return;
            }
            else if (obj == CommandAction.showBoard)
            {
                PageService.Navigate(GetType(), "Frame", typeof(BoardViewModel));

            }
            else if (obj == CommandAction.showView)
            {
                PageService.GoBack(GetType(), "Frame");
            }
            Notify("");

        }

        private void Settings_SettingsChangedRaised()
        {
            Notify(nameof(FontWeight));
            Notify(nameof(FontSize));
            Notify(nameof(AllowBackspace));
            if (GetSettings().MusicOn == false)
                MusicPlayer.Stop();
            
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (lesson == null)
                return;
            var stats = await StatisticsMeasurer.GetStatisticsAsync(RunningTime, Strokes, lesson.Text, entryText);
            SpeedWpm = stats.SpeedWpm;
            SpeedCpm = stats.SpeedCpm;
            Accuracy = stats.Accuracy;
            TrueAccuracy = stats.TrueAccuracy;
            Notify(nameof(ElapsedTimeSpan));

            //if user does not enter anything for [approx] 4 seconds, pause
            if (idle++ == 8)
            {
                Pause();
                idle = 0;
            }

            // Debug.WriteLine($"Speed {speedWPM} Accuray {Accuracy} TrueAccuracy {TrueAccuracy}");
        }
        #endregion

        #region CommandMethods
        void InitializeCommands()
        {
            NextCommand = new RelayCommand(() =>
            {
                EntryText = "";
                Lesson = null;
                if (GetSettings().LessonMode == LessonMode.Machine)
                {
                    Next();
                }
                else
                    Lesson = LessonRepository.GetNextLesson();
                Stop();
            });
            PreviousCommand = new RelayCommand(() =>
            {
                EntryText = "";
                Lesson = null;
                if (GetSettings().LessonMode == LessonMode.Machine)
                {
                    Prev();
                }
                else
                    Lesson = LessonRepository.GetPrevLesson();
                Stop();
            });
            PauseCommand = new RelayCommand(() =>
            {
                Pause();
            });
            RestartCommand = new RelayCommand(() =>
            {
                Lesson = null;
                Restart();
            });


            ChooseCommand = new RelayCommand(() =>
            {
                ChooseLessonVm cvm = new ChooseLessonVm();
                DialogService.ShowDialog(cvm);
                if (cvm.Selected != null)
                {
                    LessonRepository.SetCurrentLesson(cvm.Selected);
                    Restart();
                }
            });

            EditCommand = new RelayCommand(() =>
            {

                ChooseLessonVm cvm = new ChooseLessonVm()
                {
                    Mode = ChooseLessonVm.EditMode.Edit,
                    Title = "Choose lesson to edit",
                    CommandTitle = "Edit",
                };

                DialogService.ShowDialog(cvm);

                if (cvm.Selected != null)
                {
                    EditLessonVM edlv = new EditLessonVM()
                    {
                        Lesson = cvm.Selected
                    };
                    DialogService.ShowDialog(edlv);
                }
            });

            DeleteCommand = new RelayCommand(() =>
            {
                ChooseLessonVm cvm = new ChooseLessonVm()
                {
                    Mode = ChooseLessonVm.EditMode.Edit,
                    Title = "Choose lesson to delete",
                    CommandTitle = "Delete"
                };

                DialogService.ShowDialog(cvm);

                if (cvm.Selected != null)
                {
                    if (Notifier.ShowMessage($"Are you sure you want to delete [{cvm.Selected.Name}]?", "Delete Confirmation!", NotifierButtons.YesNo) == NotifierResult.Yes)
                    {
                        LessonRepository.DeleteUserLesson((UserLesson)cvm.Selected);
                        Notifier.ShowMessage($"{cvm.Selected.Name} deleted successfully!");
                        return;
                    }
                }
            });



            DeleteAccountCommand = new RelayCommand(() =>
            {
                if (Notifier.ShowMessage("Are you sure you want to delete this account", "Delete Account", NotifierButtons.YesNoCancel) == NotifierResult.Yes)
                {
                    ClearData();
                    LessonRepository.DeleteAllUserLesson();
                    PerfomanceMonitor.DeleteLetterPerfomanceStatistics();
                    StudentRepository.DeleteLoggedStudent();
                    DialogService.CloseDialog(GetType());
                }

            });

            ClearDataCommand = new RelayCommand(() =>
             {
                 if (Notifier.ShowMessage("Are you sure you want to clear data for this account", "Clear Account Data", NotifierButtons.YesNoCancel) == NotifierResult.Yes)
                 {
                     ClearData();
                     Notifier.ShowMessage("Data cleared successfully!.", "Cleared");
                 }

             });

            StartCommand = new RelayCommand(() =>
            {

                if (GetSettings().LessonMode == LessonMode.Machine)
                {
                    if (State == RunnerState.Iddle)
                        GoMachine();
                }
                else
                {
                    if (State == RunnerState.Paused)
                        Start();
                    else if (State == RunnerState.Running)
                        Pause();
                    else
                    {
                        Lesson = LessonRepository.GetCurrentLesson();
                        Start();
                    }
                }
            });

            ImportCommand = new RelayCommand<KeyValuePair<string, string>>((kvp) =>
            {
                if (string.IsNullOrWhiteSpace(kvp.Key) || string.IsNullOrWhiteSpace(kvp.Value))
                {
                    return;
                }
                string name = Path.GetFileNameWithoutExtension(kvp.Key);
                string content = kvp.Value;
                CreateLessonVM vm = new CreateLessonVM()
                {
                    LessonName = name,
                    LessonText = content
                };
                vm.CreateCommand.Execute(null);
            }, (o) => true);
        }

        private void ClearData()
        {
            LessonRepository.DeleteAllUserLesson();
            StatisticsRepository.DeleteLoggedUserStatistics();
            LessonStatisticsStore.Clear();

            PerfomanceMonitor.DeleteLetterPerfomanceStatistics();
            Settings.ResetSettings();
            LessonRepository.PopulateUserLessons();
            PerfomanceMonitor.PopulateLetters();

        }
        #endregion


        void Start()
        {
            //avoid recording time for the first letter. Cause the user will take a little while to press a letter after clicking the start button
            PerfomanceMonitor.JumpMonitoring = true;
            Runner.Start();
            timer.Start();
            Notify(nameof(State));
            Notify(nameof(StartLabel));
            MusicPlayer.PlayAsync();
        }
        void Stop()
        {
            Runner.Stop();
            timer.Stop();
            Notify(nameof(State));
            Notify(nameof(StartLabel));
        }
        void Pause()
        {
            //remove the last time recorded. Cause the user will take a little while to press a letter to clicking the pause button
            PerfomanceMonitor.RemoveLast();
            MusicPlayer.PauseAsync();
            Runner.Pause();
            timer.Stop();
            Notify(nameof(State));
            Notify(nameof(StartLabel));
            // MusicPlayer.Pause();
        }
        void Restart()
        {
            //remove the last time recorded. Cause the user will take a little while to press a letter to clicking the pause button
            EntryText = "";
            PerfomanceMonitor.RemoveLast();
            Lesson = null;
            if (GetSettings().LessonMode == LessonMode.Machine)
            {
                GoMachine();
            }
            else
                Lesson = LessonRepository.GetCurrentLesson();
            Stop();
        }
        private async void FinishUpAsync()
        {
            int errors = 0;
            for (int i = 0; i < entryText.Length; i++)
            {
                if (lesson.Text[i] != entryText[i])
                {
                    errors++;
                }
            }
            int totalErrors = Strokes - EntryText.Length + errors;

            var stats = await StatisticsMeasurer.GetStatisticsAsync(RunningTime, Strokes, lesson.Text, entryText);
            Statistics statistics = new Statistics();
            statistics.CopyPropertiesFrom(stats);
            StatisticsRepository.Add(statistics);

            LessonStatistics lessonStatistics = new LessonStatistics();
            lessonStatistics.CopyPropertiesFrom(stats);
            lessonStatistics.LessonName = lesson.Name;
            lessonStatistics.LessonLength = lesson.Text.Length;
            lessonStatistics.Time = DateTime.Now.ToShortTimeString();
            LessonStatisticsStore.Add(lessonStatistics);

            var resultsViewModel = new ResultVM()
            {
                Accuracy = Accuracy,
                SpeedWpm = SpeedWpm,
                Strokes = Strokes,
                SpeedCpm = SpeedCpm,
                TrueAccuracy = TrueAccuracy,
                TotalErrors = totalErrors,
                ElapsedTime = $"{RunningTime.Hours}h:{RunningTime.Minutes}m:{RunningTime.Seconds}s",
                Corrections = totalErrors - errors,
                Errors = errors,
                LessonLength = lesson.Text.Length,
            };
            resultsViewModel.CalculateRate();

            resultsViewModel.RequestedAction += ViewModel_RequestedAction;
            Stop();

            StatisticsRepository.Save();
            PerfomanceMonitor.Save();

            DialogService.ShowDialog(resultsViewModel);
        }

        private void ViewModel_RequestedAction(ResultVM.RequestedCommand obj)
        {
            switch (obj)
            {
                case ResultVM.RequestedCommand.Redo:
                    RestartCommand.Execute(null);
                    break;
                case ResultVM.RequestedCommand.Next:
                    NextCommand.Execute(null);
                    break;
            }
        }
    }
}
