using CoreLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class SettingsVM : VMBase
    {
        Settings settings;

        //GENERAL SETTINGS
        public bool AllowBackspace
        {
            get => settings.AllowBackspace;
            set { settings.AllowBackspace = value; }
        }
        public Theme Theme
        {
            get => settings.Theme;
            set { settings.Theme = value; }
        }
        public List<Theme> Themes
        {
            get => settings.Themes;
            set { }
        }



        public double FontSize
        {
            get => settings.FontSize;
            set
            {
                settings.FontSize = value;
            }
        }
        public List<double> FontSizes { get; set; }
               = new List<double>
               {
            10, 12, 14, 18, 25, 30, 35, 40, 45, 50
               };



        public string FontWeight
        {
            get => settings.FontWeight;
            set { settings.FontWeight = value; }
        }
        public List<string> FontWeights { get; set; }
            = new List<string>
                {
                     "Regular", "SemiBold", "Bold"
                };


        public bool Machine
        {
            get => settings.IsMachineLessonMode;
            set { settings.IsMachineLessonMode = value; }
        }

        public bool User
        {
            get => settings.IsUserLessonMode;
            set { settings.IsUserLessonMode = value; }
        }

        public bool Practice
        {
            get => settings.IsPracticeLessonMode;
            set { settings.IsPracticeLessonMode = value; }
        }

        public bool Tutorials
        {
            get => settings.IsTutorial;
            set { settings.IsTutorial = value; }
        }
        public List<object> UserInterfaces
        {
            get => settings.UserInterfaces;
            set { settings.UserInterfaces = value; }
        }
        public object UserInterface
        {
            get => settings.UserInterface;
            set
            {
                settings.UserInterface = value;
            }
        }
        //AUDIO SETTINGS
        public bool AudioOn
        {
            get => SoundOn || MusicOn;
            set
            {
                SoundOn = value;
                MusicOn = value;
                Notify(nameof(SoundOn));
                Notify(nameof(MusicOn));
                Notify();
            }
        }

        public bool SoundOn
        {
            get => settings.SoundOn;
            set { settings.SoundOn = value; }
        }

        public bool MusicOn
        {
            get => settings.MusicOn;
            set { settings.MusicOn = value; }
        }



        public ObservableCollection<MusicFile> Files
        {
            get => Settings.GetSettings().MusicFiles;
            set { }
        }


        //COMMANDS

        public ICommand AddMusicCommand { get; set; }
        public ICommand DoneCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public SettingsVM()
        {
            settings = Settings.GetNewSettings();
            AddMusicCommand = new RelayCommand(() =>
            {
            });

            DoneCommand = new RelayCommand(() =>
            {
                Close();
                ApplyCommand.Execute(null);
            });

            CancelCommand = new RelayCommand(() =>
            {
                Close();
            });

            ApplyCommand = new RelayCommand(() =>
            {
                Settings.SetSettings(settings);
                Settings.Save();
                Settings.RaiseSettingChangedEvent();
            });
        }

        void Close()
        {
            DialogService.CloseDialog(GetType());
        }
    }
}
