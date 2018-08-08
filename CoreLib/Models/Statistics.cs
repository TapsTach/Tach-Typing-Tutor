using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLib
{
    public class Statistics : IStatistics
    {
        TimeSpan timespan;
        public int Id { get; set; }
        public double SpeedWpm { get; set; }
        public int SpeedCpm { get; set; }
        public double Accuracy { get; set; }
        public double TrueAccuracy { get; set; }
        public int Correct { get; set; }
        public int Errors { get; set; }
        public long Entries { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public TimeSpan TimeTaken
        {
            get => timespan;
            set
            {
                timespan = value;
                TimeTakenString = $"{TimeTaken.Hours}h:{TimeTaken.Minutes}m:{TimeTaken.Seconds}s";
            }
        }
        public string TimeTakenString { get; set; }
        public string Date { get; set; }
        
        public static Statistics SumUpAverage(IEnumerable<Statistics> statistics)
        {
            Statistics stats = new Statistics();

            stats.Accuracy = Math.Round(statistics.Average(s => s.Accuracy), 2);
            stats.TrueAccuracy = Math.Round(statistics.Average(s => s.TrueAccuracy), 2);

            stats.SpeedWpm = Math.Round(statistics.Average(s => s.SpeedWpm), 2);
            stats.SpeedCpm = (int)Math.Round(statistics.Average(s => s.SpeedCpm), 2);

            stats.Correct = statistics.Sum(s => s.Correct);
            stats.Errors = statistics.Sum(s => s.Errors);
            stats.Entries = statistics.Sum(s => s.Entries);
            stats.TimeTaken = new TimeSpan(statistics.Select(s => s.TimeTaken).Sum(t => t.Ticks));

            return stats;
        }

    }

}
