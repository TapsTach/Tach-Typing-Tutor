using System.Windows.Input;
using CoreLib;
using TawmFramework;

namespace VMLib
{
    public class EditLessonVM : VMBase
    {

        public string LessonName
        {
            get => Lesson.Name;
            set
            {
                Lesson.Name = value;
                Notify();
            }
        }

        public string LessonText
        {
            get => Lesson.Text;
            set
            {
                Lesson.Text = value;
                Notify();
            }

        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public Lesson Lesson { get; set; }

        public EditLessonVM()
        {
            CancelCommand = new RelayCommand(() =>
            {
                Close();
            });

            SaveCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(LessonName))
                {
                    Notifier.ShowMessage("Lesson name cannot be empty.", "Invalid");
                    return;
                }
                if (string.IsNullOrEmpty(LessonText))
                {
                    Notifier.ShowMessage("Lesson text cannot be empty.", "Invalid");
                    return;
                }

                LessonRepository.UpdateUserLesson((UserLesson)Lesson);
                Notifier.ShowMessage("Lesson saved successfully.", "Success");
                

                Close();
            });
        }
        void Close()
        {
            DialogService.CloseDialog(GetType());
        }
    }
}
