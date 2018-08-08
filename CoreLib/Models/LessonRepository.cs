using CoreExtLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLib
{
    public static class LessonRepository
    {
        static List<Lesson> lessons;
        static Lesson currentLesson;
        static Stack<int> prevPracLessons = new Stack<int>();
        static Queue<int> nextPracLessons = new Queue<int>();
        static List<Lesson> userLessons;
        static List<Lesson> articles;

        static LessonRepository()
        {
            Load();
            //DatabaseInitializer.Seed();
            Settings.SettingsChangedRaised += Settings_SettingsChangedRaised;

        }

        public static void Load()
        {
            userLessons = Database.Query<UserLesson>(constraint: $"where ownerId = {StudentRepository.Logged.Id}").Select(less => (Lesson)less).ToList();
            articles = Database.Query<Article>().Select(less => (Lesson)less).ToList();

            LoadLessons();
        }
        private static void Settings_SettingsChangedRaised()
        {
            LoadLessons();
        }

        public static void PopulateUserLessons()
        {

            foreach (var less in Database.Query<DefaultLesson>())
            {
                Database.Insert(new UserLesson()
                {
                    Name = less.Name,
                    Text = less.Text,
                    OwnerId = StudentRepository.Logged.Id
                });

            }
            Load();
        }

        static public List<Lesson> Lessons
        {
            get
            {
                if (lessons == null)
                    LoadLessons();
                return lessons;
            }
        }
        static void RefreshLessons()
        {
            userLessons = Database.Query<UserLesson>(constraint: $"where ownerId = {StudentRepository.Logged.Id}").Select(less => (Lesson)less).ToList();
            if (Settings.GetSettings().LessonMode == LessonMode.User)
                lessons = userLessons;
        }
        public static void AddLesson(UserLesson lesson)
        {
            Database.Insert(lesson);
            RefreshLessons();
        }

        public static List<Lesson> GetUserLessons()
        {
            return userLessons;
        }

        public static List<Lesson> GetPracticeLessons()
        {
            return articles;
        }
        public static Lesson GetNextLesson()
        {
            if (Settings.GetSettings().IsUserLessonMode)
            {
                if (Lessons.Count == 0)
                    return null;
                if (Settings.GetSettings().UserLessonIndex >= Lessons.Count - 1)
                    Settings.GetSettings().UserLessonIndex = -1;

                return currentLesson = Lessons[++Settings.GetSettings().UserLessonIndex];
            }
            else
            {
                if (nextPracLessons.Count > 0)
                {
                    currentLesson = articles[nextPracLessons.Dequeue()];
                }
                else
                {
                    currentLesson = articles.ChooseRandomly();
                }

                prevPracLessons.Push(articles.IndexOf(currentLesson));
                return currentLesson;
            }
        }
        static void LoadLessons()
        {
            switch (Settings.GetSettings().LessonMode)
            {
                case LessonMode.User:
                    lessons = userLessons;
                    if (Settings.GetSettings().UserLessonIndex > 1)
                        Settings.GetSettings().UserLessonIndex--;
                    break;
                case LessonMode.Practice:
                    lessons = articles;
                    break;
                default:
                    lessons = new List<Lesson>();
                    break;
            }

            GetNextLesson();

        }
        public static Lesson GetPrevLesson()
        {
            if (Settings.GetSettings().IsUserLessonMode)
            {
                if (Settings.GetSettings().UserLessonIndex > 0)
                {
                    return currentLesson = Lessons[(--Settings.GetSettings().UserLessonIndex)];
                }
                return Lessons[Settings.GetSettings().UserLessonIndex];
            }
            else
            {
                if (prevPracLessons.Count > 0)
                {
                    currentLesson = articles[prevPracLessons.Pop()];
                }
                else
                {
                    GetNextLesson();
                }

                nextPracLessons.Enqueue(articles.IndexOf(currentLesson));
                return currentLesson;
            }
        }

        public static Lesson GetCurrentLesson()
        {
            if (currentLesson == null)
                GetNextLesson();
            return currentLesson;
        }

        public static void DeleteUserLesson(UserLesson lesson)
        {
            Database.Delete(lesson);

            RefreshLessons();
        }

        public static void UpdateUserLesson(UserLesson lesson)
        {
            Database.Update(lesson);
            RefreshLessons();
        }

        public static void DeleteAllUserLesson()
        {
            Database.DeleteAll<UserLesson>(constraint: $"where ownerId = {StudentRepository.Logged.Id}");
        }

        public static void SetCurrentLesson(Lesson lesson)
        {
            currentLesson = lesson;
        }
    }
}
