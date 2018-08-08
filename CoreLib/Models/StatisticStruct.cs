using System;

namespace CoreLib
{
    public class StatisticStruct : IStatistics
    {
        public int Id { get; set; }
        public double SpeedWpm { get; set; }
        public int SpeedCpm { get; set; }
        public double Accuracy { get; set; }
        public double TrueAccuracy { get; set; }
        public int Correct { get; set; }
        public int Errors { get; set; }
        public long Entries { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public string TimeTakenString { get; set; }
        public DateTime Date { get; set; }
    }
}
