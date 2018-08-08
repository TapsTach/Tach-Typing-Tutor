using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using CoreExtLib;
using System.Collections.ObjectModel;

namespace CoreLib
{
    public class Settings
    {
        bool machineLessonMode, userLessonMode, practiceLessonMode;
        public static event Action SettingsChangedRaised;
        #region Properties
        public double FontSize { get; set; }
        public string FontWeight { get; set; }
        public Theme Theme { get; set; }
        public LessonMode LessonMode
        {
            get
            {
                if (IsMachineLessonMode)
                    return LessonMode.Machine;
                else if (IsUserLessonMode)
                    return LessonMode.User;
                else return LessonMode.Practice;
            }
        }
        public bool IsMachineLessonMode
        {
            get => machineLessonMode;
            set
            {
                if (value)
                {
                    machineLessonMode = value;
                    practiceLessonMode = userLessonMode = false;
                }

            }
        }

        public bool IsUserLessonMode
        {
            get => userLessonMode;
            set
            {
                if (value)
                {
                    userLessonMode = value;
                    machineLessonMode = practiceLessonMode = false;
                }
            }
        }
        public int UserLessonIndex { get; set; } = -1;
        public int MachineLessonIndex { get; set; } = -1;
        public bool IsPracticeLessonMode
        {
            get => practiceLessonMode;
            set
            {
                if (value)
                {
                    practiceLessonMode = value;
                    machineLessonMode = userLessonMode = false;
                    //  LessonMode = "practice";
                }
            }
        }
        public List<Theme> Themes { get; set; }
        = new List<Theme>
        {
            new Theme{Name="Light", Color="LightYellow"},
            new Theme{Name="Dark", Color="DarkGray"},
        };

        public bool SoundOn { get; set; }
        public bool MusicOn { get; set; }

        public ObservableCollection<MusicFile> MusicFiles { get; set; } = new ObservableCollection<MusicFile>();
        public bool IsTutorial { get; set; }
        public List<object> UserInterfaces { get; set; }
        public object UserInterface { get; set; }
        public bool AllowBackspace { get; set; } = true;
        #endregion

        #region Constructor
        public Settings()
        {
            //Defaults
            Theme = Themes[0];
            FontSize = 35;
            FontWeight = "Regular";
            IsUserLessonMode = true;
            IsTutorial = true;

        }
        ~Settings()
        {
            Save();
        }
        #endregion

        static Settings settings;
        public static Settings GetSettings()
        {
            if (settings == null)
                if (!Load())
                    settings = new Settings();
            return settings;
        }

        public static Settings GetNewSettings()
        {
            Settings sets = new Settings();
            sets.CopyPropertiesFrom(GetSettings());
            return sets;
        }
        public static void ResetSettings()
        {
            settings = new Settings();
            string file = Path.Combine(Database.DataFolder, $"settings{StudentRepository.Logged.Id}.json"); ;
            if (File.Exists(file))
                File.Delete(file);
        }
        public static void AddMusicFile(MusicFile file)
        {
            Settings.GetSettings().MusicFiles.Add(file);
        }
        public static void SetSettings(Settings preferences)
        {
            settings = preferences;
        }

        public static void RaiseSettingChangedEvent()
        {
            SettingsChangedRaised?.Invoke();
        }

        ///TODO change path to a data folder path
        static string settingsFile = Path.Combine(Database.DataFolder, $"settings{StudentRepository.Logged.Id}.json");
        public static void Save()
        {
            if (settings == null)
                return;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var serString = serializer.Serialize(settings);
            File.WriteAllText(settingsFile, serString);
        }

        public static bool Load()
        {
            string serString = "";
            if (File.Exists(settingsFile))
            {
                serString = File.ReadAllText(settingsFile);
            }
            else
                return false;

            settings = new JavaScriptSerializer().Deserialize<Settings>(serString);
            return true;

        }
    }
}
