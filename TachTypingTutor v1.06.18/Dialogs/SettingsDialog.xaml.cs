using CoreLib;
using Microsoft.Win32;
using System.Linq;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : TWindow
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "Music file (*.mp3;*.wav)|*.mp3;*.wav;";
            if (!dialog.ShowDialog().Value)
                return;
            foreach (string file in dialog.FileNames)
            {

                string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                MusicFile musicFile = new MusicFile()
                {
                    FilePath = file,
                    FileName = fileName
                };
                if (Settings.GetSettings().MusicFiles.Select(m => m.FileName).Contains(fileName))
                    continue;
                Settings.AddMusicFile(musicFile);
            }
        }

        private void remove_click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lst.SelectedItem != null)
                while (lst.SelectedItems.Count > 0)
                {
                    Settings.GetSettings().MusicFiles.RemoveAt(0);
                }
        }
    }
}
