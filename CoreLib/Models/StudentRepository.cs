using System;
using System.Collections.Generic;

namespace CoreLib
{
    public static class StudentRepository
    {
        static List<Student> students;

        static StudentRepository()
        {
            students = Database.Query<Student>();
        }
        public static Student Logged
        {
            get; set;
        }
        public static List<Student> GetStudents()
        {
            return students;
        }

        public static void Add(Student student)
        {
            Database.Insert(student);
            students = Database.Query<Student>();
        }

        public static void DeleteLoggedStudent()
        {
            Database.Delete(Logged);
            students = Database.Query<Student>();
        }


        public static void LockLoggedUserAccount(string password)
        {
            if (password == null)
                Logged.Password = "";
            else
                Logged.Password = password;
            Database.Update(Logged);
        }
    }
}
