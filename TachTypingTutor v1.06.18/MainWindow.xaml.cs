using CoreLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using VMLib;

namespace TachTypingTutor_v1._06._18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TWindow
    {
      
        public MainWindow()
        {
            InitializeComponent();
            Settings.SettingsChangedRaised += Settings_SettingsChangedRaised;
            App.ChangeTheme();
        }

        private void Settings_SettingsChangedRaised()
        {
            App.ChangeTheme();
        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsEnabled)
                input.Append(e.Text);
            
        }
        MainVM viewModel;
        private void ShowImportLesson_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file(*.txt; *.dat)| *.txt; *.dat; ";
            if (openFileDialog.ShowDialog().Value )
            {
                if (viewModel == null)
                    viewModel = (MainVM)DataContext;

                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(openFileDialog.FileName, File.ReadAllText(openFileDialog.FileName));
                viewModel.ImportCommand.Execute(kvp);

            }

        }
    }
}
