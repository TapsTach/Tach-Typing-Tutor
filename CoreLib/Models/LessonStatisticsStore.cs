using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLib
{
    public static class LessonStatisticsStore
    {
        static List<LessonStatistics> sessionStatistics = new List<LessonStatistics>();
        public static void Add(LessonStatistics stats)
        {
            sessionStatistics.Add(stats);
        }

        public static List<LessonStatistics> GetStatistics()
        {
            return sessionStatistics;
        }

        public static LessonStatistics GetAverage()
        {
            LessonStatistics stats = new LessonStatistics();
            if (sessionStatistics.Count > 0)
            {
                stats.Accuracy = Math.Round(sessionStatistics.Select(s => s.Accuracy).Average(), 2);
                stats.TrueAccuracy = Math.Round(sessionStatistics.Select(s => s.TrueAccuracy).Average(), 2);
                stats.SpeedWpm = Math.Round(sessionStatistics.Select(s => s.SpeedWpm).Average(), 2);
                stats.Correct = sessionStatistics.Sum(s => s.Correct);
                stats.Errors = sessionStatistics.Sum(s => s.Errors);
                stats.Entries = sessionStatistics.Sum(s => s.Entries);
                stats.SpeedCpm = (int)Math.Round(sessionStatistics.Select(s => s.SpeedCpm).Average());
                stats.LessonLength = sessionStatistics.Sum(s => s.LessonLength);
                foreach (var st in sessionStatistics)
                    stats.TimeTaken += st.TimeTaken;
            }
            return stats;
        }

        public static void Clear()
        {
            sessionStatistics.Clear();
        }
    }
}
