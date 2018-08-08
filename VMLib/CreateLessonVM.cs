using CoreLib;
using System.Linq;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class CreateLessonVM : VMBase
    {
        public string LessonName { get; set; }
        public string LessonText { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateLessonVM()
        {
            InitializeCommands();
        }

        void InitializeCommands()
        {
            CreateCommand = new RelayCommand(() =>
            {
                Create();
            });

            CloseCommand = CancelCommand = new RelayCommand(() =>
            {
                Close();
            });
        }

        void Close()
        {
            DialogService.CloseDialog(GetType());
        }

        void Create()
        {

            if (LessonRepository.Lessons.Where(l => l.Name == LessonName).Count() > 0)
            {
                NotifierVm nvm = new NotifierVm() { Message = "Lesson name already exists. Please choose another one.", Buttons = NotifierButtons.Ok, Title = "Invalid Entry!" };
                DialogService.ShowDialog(nvm);
                return;
            }

            if (string.IsNullOrEmpty(LessonName))
            {
                DialogService.ShowDialog(new NotifierVm("Lesson Name cannot be empty!.", "Invalid Entry!"));
                return;
            }
            if (string.IsNullOrEmpty(LessonText))
            {
                DialogService.ShowDialog(new NotifierVm("Lesson text cannot be empyt!.", "Invalid Entry"));
                return;
            }

            LessonRepository.AddLesson(new UserLesson()
            {
                Name = LessonName,
                Text = new string(LessonText.Replace("\r", "").Replace("\n", "").Take(300).ToArray()),
                OwnerId = StudentRepository.Logged.Id
            });
            DialogService.ShowDialog(new NotifierVm("Lesson created successfully!.", "Success"));
            Close();

        }


    }
}
