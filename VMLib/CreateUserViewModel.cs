using CoreLib;
using System.Linq;
using System.Windows.Input;
using TawmFramework;

namespace VMLib
{
    public class CreateUserViewModel : VMBase
    {
        #region Properties
        public string Username { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion

        public CreateUserViewModel()
        {
            CreateCommand = new RelayCommand(() =>
            {
                if (Username == null)
                    return;
                if(Username.Trim().Length<3)
                {
                    Notifier.ShowMessage("Please enter at least 3 character for a user name!");
                    return;
                }
                if(StudentRepository.GetStudents().Select(s=>s.Username).Contains(Username))
                {
                    Notifier.ShowMessage($"{Username} already exist. Please specify a different one.");
                    return;
                }
                
                Student student = new Student()
                {
                    Username = Username
                };

                StudentRepository.Add(student);
                StudentRepository.Logged = StudentRepository.GetStudents().Where(s => s.Username == student.Username).First();

                PerfomanceMonitor.PopulateLetters();
                LessonRepository.PopulateUserLessons();
                Notifier.ShowMessage($"{Username} created successfully");

                DialogService.HideDialog(GetType());
                DialogService.ShowDialog(typeof(MainVM));
                DialogService.CloseDialog(GetType());
            });

            CancelCommand = new RelayCommand(() => DialogService.CloseDialog(GetType()));
        }

    }
}
