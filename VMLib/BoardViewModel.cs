using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static CoreLib.MachineLessonHandler;

namespace VMLib
{
    public class BoardViewModel : VMBase
    {
        #region MachineLessonHandler properties
        public string Text
        {
            get => MachineLessonHandler.Text;
            set { }
        }
        public string Background
        {
            get => "Blue";
            set { }
        }
        public string HighlightedKeys
        {
            get => MachineLessonHandler.HighlightedKeys;
            set { }
        }
        public string Highlighted
        {
            get => MachineLessonHandler.Highlighted;
            set { }
        }
     
        public bool UnHighlight
        {
            get => MachineLessonHandler.UnHighlight;
            set { }
        }
        public bool Keyboard
        {
            get => MachineLessonHandler.Keyboard;
            set { }
        }
        public bool UIEnabled
        {
            get => UiEnabled;
            set { }
        }

        public bool TutorialsOff
        {
            get => !Settings.GetSettings().IsTutorial;
            set
            {
                Settings.GetSettings().IsTutorial = !value;
            }

        }
        #endregion

        #region Commands
        public ICommand SkipCommand { get; set; }

        #endregion
        public BoardViewModel()
        {
            ActionExecuted += BoardViewModel_ActionExecuted;
            SkipCommand = new RelayCommand(() =>
            {
                Skip();
            });
        }

        private void BoardViewModel_ActionExecuted(CommandAction obj)
        {
            Notify("");
        }


    }
}
