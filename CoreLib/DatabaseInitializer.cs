using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace CoreLib
{
    class DatabaseInitializer
    {
     
        public static  void Seed()
        {
            Debugger.Break();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string folder = "dataImport";
          //  FactData factD = serializer.Deserialize<FactData>(File.ReadAllText(Path.Combine(folder, "fact.json")));
          //  foreach (var data in factD.Facts)
          //  {
          //      Article art = new Article
          //      {
          //          Name = data.Name,
          //          Text = data.Fact,
          //      };
          //     Database.Insert(art);
          //  }


          //  JokeData joke = serializer.Deserialize<JokeData>(File.ReadAllText(Path.Combine(folder, "joke.json")));
          //  foreach (var data in joke.Jokes)
          //  {
          //      Article art = new Article
          //      {
          //          Name = data.Name,
          //          Text = data.Joke,
          //      };
          //      Database.Insert(art);
          //  }

          // QouteData qoute = serializer.Deserialize<QouteData>(File.ReadAllText(Path.Combine(folder, "qoute.json")));
          //  foreach (var fact in qoute.Qoutes)
          //  {
          //      Article art = new Article
          //      {
          //          Name = fact.Name,
          //          Text = fact.Qoute,
          //      };
          //      Database.Insert(art);
          //  }

          //DefaultLessonData Dlesson = serializer.Deserialize<DefaultLessonData>(File.ReadAllText(Path.Combine(folder, "defaultLesson.json")));

          //  foreach (var lesson in Dlesson.DefaultLessons)
          //  {
          //      DefaultLesson ls = new DefaultLesson()
          //      {
          //          Name = lesson.Name,
          //          Text = new string(lesson.Lesson.Take(100).ToArray())
          //      };
          //      Database.Insert(ls);
          //  }

          

            MachineLessonData machineLesson = serializer.Deserialize<MachineLessonData>(File.ReadAllText(Path.Combine(folder, "lessonScript.json")));

            foreach(var v in machineLesson.Lessons)
            {
                MachineLesson lesson = new MachineLesson()
                {
                    Text = v.Script,
                    Level = v.Level,

                };
                Database.Insert(lesson);
            }
            

        }
    }


    #region Dummy classes
    class Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeRestriction { get; set; }
        public string Category { get; set; }
    }
    class FactData : Base
    {
        public FactData()
        {

        }
        public string Fact { get; set; }
        public List<FactData> Facts{ get; set; }
    }
    class JokeData : Base
    {
        public string Joke { get; set; }
        public List<JokeData> Jokes { get;  set; }
    }
    class DefaultLessonData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lesson { get; set; }
        public List<DefaultLessonData> DefaultLessons { get;  set; }
    }
    class QouteData : Base
    {
        public string Qoute { get; set; }
        public List<QouteData> Qoutes { get; set; }
    }

    class DictionaryData
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
    class MachineLessonData : MachineLesson
    {
        public string Script { get; set; }
        public List<MachineLessonData> Lessons { get; set; }
    }
    #endregion
}
