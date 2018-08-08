using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Table("LetterPerfomance")]
    public struct PerfMonLetter
    {
        public int Id { get; set; }
        public string CharacterString { get; set; }
        public double AverageTime { get; set; }
        public int Correct { get; set; }
        public int UserId { get; set; }
        public int Entries { get; set; }

        [NotMapped]
        public int Mistakes { get => Entries - Correct; }
        [NotMapped]
        public TimeSpan Time { get; set; }
        [NotMapped]
        public double Accuracy
        {
            get
            {
                if (Entries == 0)
                    return 0;
                double d = ((double)Correct / Entries);
                return d;
            }

        }
        public override string ToString()
        {
            return $"{Id} {CharacterString}  Corr= {Correct}  ent={Entries} timeSpan{Time} avg Time = {AverageTime}";
        }

    }

    public static class PerfomanceMonitor
    {
        public static TimeSpan prevCapturedTime { get; private set; }
        static List<PerfMonLetter> letters = new List<PerfMonLetter>();
        static List<PerfMonLetter> compiledLetters = new List<PerfMonLetter>();
        public static bool JumpMonitoring { get; set; } = true;
        static PerfomanceMonitor()
        {
            letters = Database.Query<PerfMonLetter>(constraint: $"where userId = {StudentRepository.Logged.Id}");
            compiledLetters.AddRange(letters);
        }
        public static List<PerfMonLetter> GetLetters()
        {
            return compiledLetters.Where(l => l.Entries > 1).ToList();
        }
        public static void RemoveLast()
        {
            if (letters.Count > 0)
                letters.RemoveAt(letters.Count - 1);
        }

        public static void Monitor(string key, string expectedKey)
        {
            if (JumpMonitoring)
            {
                JumpMonitoring = false;
                return;
            }
            PerfMonLetter letter = new PerfMonLetter()
            {
                CharacterString = expectedKey,
                Correct = key == expectedKey ? 1 : 0,

            };
            TimeSpan timeSpan = Runner.RunningTime - prevCapturedTime;
            if (letter.Correct == 1)
                letter.Time = timeSpan;

            prevCapturedTime = Runner.RunningTime;
            letters.Add(letter);
        }

        public static void Save()
        {
            Compile();
            Database.Update(compiledLetters);
        }

        static void Compile()
        {
            compiledLetters.Clear();
            var grouped = letters.GroupBy(l => l.CharacterString);
            foreach (var group in grouped)
            {
                //avoid saving space and if there is no entry.
                if (String.IsNullOrWhiteSpace(group.Key) || group.Count() < 2)
                    continue;
                //get the id from the database. since only the letter that comes from the database has the id, which is generated as an identity.
                int id = group.Where(g => g.Id != 0).FirstOrDefault().Id;

                var average = 0d;
                try
                {
                    average = Math.Round
                    (
                    group.Where(g => g.Time > new TimeSpan(0))
                    .Average(g => g.Time.TotalMilliseconds)
                    );
                }
                catch
                {
                    continue;
                }

                PerfMonLetter letter = new PerfMonLetter()
                {
                    Id = id,
                    CharacterString = group.Key,
                    Entries = group.Count() + group.Sum(g => g.Entries) - 1,
                    Correct = group.Sum(g => g.Correct),
                    UserId = StudentRepository.Logged.Id
                };


                letter.AverageTime = average;
                compiledLetters.Add(letter);
            }


        }

        public static void PopulateLetters()
        {
            List<string> newLetters = new List<string>();
            for (int i = 33; i < 127; i++)
            {
                newLetters.Add(((char)i).ToString());
            }
            Database.Insert(newLetters.Select(l =>
            new PerfMonLetter()
            {
                UserId = StudentRepository.Logged.Id,
                CharacterString = l
            }).ToList()
            );
            letters = Database.Query<PerfMonLetter>(constraint: $"where userId = {StudentRepository.Logged.Id}");
        }

        public static void DeleteLetterPerfomanceStatistics()
        {
            Database.DeleteAll<PerfMonLetter>(constraint: $"where userId = {StudentRepository.Logged.Id}");
            compiledLetters.Clear();
        }
    }


}
