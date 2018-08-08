using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreLib
{
    public static class StatisticsRepository
    {
        static List<Statistics> statistics;
        static bool hasToDayStatistics = false;
        static string toDate;
        static StatisticsRepository()
        {

            //get all the statistics in a database
            statistics = Database.Query<Statistics>(constraint: $"Where userId = {StudentRepository.Logged.Id}");

            //assign toDate here just in case the user spans over to the other day while using the software.
            toDate = DateTime.Now.ToShortDateString();

            //assign hasTodaystatistics
            hasToDayStatistics = statistics.Where(s => s.Date == toDate).Count() > 0;
            //if there are statistics with today's date then add them to session's statistics.
            if (hasToDayStatistics)
            {
                Statistics todayStats = statistics.Where(s => s.Date == DateTime.Now.ToShortDateString()).FirstOrDefault();

                string[] timeSpanSplit = todayStats.TimeTakenString.Replace("m", "").Replace("s", "").Replace("h", "").Split(':');
                TimeSpan todayTimeSpan = new TimeSpan();
               todayTimeSpan= todayTimeSpan.Add(TimeSpan.FromHours(int.Parse(timeSpanSplit[0])));
               todayTimeSpan =todayTimeSpan.Add(TimeSpan.FromMinutes(int.Parse(timeSpanSplit[1])));
                todayTimeSpan= todayTimeSpan.Add(TimeSpan.FromSeconds(int.Parse(timeSpanSplit[2])));
                todayStats.TimeTaken = todayTimeSpan;
                sessionStatistics.Add(todayStats);
            }
        }

        static List<Statistics> sessionStatistics = new List<Statistics>();
        public static void Add(Statistics stats)
        {
            sessionStatistics.Add(stats);
        }


        public static void Save()
        {
            if (sessionStatistics.Count <= 1)
                return;
            Statistics stats = Statistics.SumUpAverage(sessionStatistics);

            stats.UserId = StudentRepository.Logged.Id;
            stats.Date = toDate;

            //update if the database contains today's statistics
            if (hasToDayStatistics)
            {
                Database.Update(stats, constraint: $"where Date = '{toDate}' and userId = {StudentRepository.Logged.Id}");

                //replace with updated statistics everytime you save.
                var tarStat = statistics.Where(s => s.Date == toDate).FirstOrDefault();
                if (tarStat != null)
                {
                    statistics.Remove(tarStat);
                    statistics.Add(stats);
                }

            }
            else
            {
                Database.Insert(stats);
                hasToDayStatistics = true;
                statistics.Add(stats);
            }

        }

        public static List<Statistics> GetStatistics()
        {
            return statistics;
        }

        public static Statistics GetAverage()
        {
            return Statistics.SumUpAverage(sessionStatistics);
        }

        public static void DeleteLoggedUserStatistics()
        {
            Database.DeleteAll<Statistics>(constraint: $"where userId = {StudentRepository.Logged.Id}");
            statistics.Clear();
            sessionStatistics.Clear();
            hasToDayStatistics = false;
        }
    }
}
