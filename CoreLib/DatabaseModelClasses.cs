namespace CoreLib
{

    public enum LessonMode
    {
        User, Practice, Machine
    }
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }

    public class Article : Lesson
    {
      
    }

    public class DefaultLesson : Lesson
    {
      
    }
    public class UserLesson : Lesson
    {
        public int OwnerId { get; set; }
       
    }

    public class MachineLesson : Lesson
    {
        public int Level { get; set; }
       
    }
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LetterPerfomance
    {
        public int Id { get; set; }
        public int Character { get; set; }
        public string CharacterString { get; set; }
        public double AverageTime { get; set; }
        public int Mistakes { get; set; }
        public int Correct { get; set; }
        public int Entries { get; set; }
        public int UserId { get; set; }

    }

    public class Dictionary
    {
        public int Id { get; set; }
        public string Word { get; set; }

    }

}
