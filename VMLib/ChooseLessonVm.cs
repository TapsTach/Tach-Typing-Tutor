using CoreLib;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class ChooseLessonVm : VMBase
    {
        string title;
        string commandTitle;
        public enum EditMode
        {
            Edit, NoneEdit
        }
        public EditMode Mode { get; set; } = EditMode.NoneEdit;
        public string CommandTitle
        {
            get => commandTitle;
            set
            {
                commandTitle = value;
                Notify();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }
        public Lesson Selected { get; set; }

        public List<Lesson> Lessons
        {
            get
            {

                if (Mode == EditMode.Edit)
                    return LessonRepository.GetUserLessons();
                else return LessonRepository.Lessons;
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ChooseCommand { get; set; }

        public ChooseLessonVm()
        {
            InitializeCommands();
            CommandTitle = "Choose";
            Title = "Choose lesson";
        }

        private void InitializeCommands()
        {
            CancelCommand = new RelayCommand(() =>
            {
                Selected = null;
                Close();
            });
            ChooseCommand = new RelayCommand(() =>
            {
                if (Selected == null)
                {
                    Notifier.ShowMessage("Nothing selected. Please select an item.", "Void");
                }
                else
                    Close();
            });


        }

        private void Close()
        {
            DialogService.CloseDialog(GetType());
        }
    }
}
