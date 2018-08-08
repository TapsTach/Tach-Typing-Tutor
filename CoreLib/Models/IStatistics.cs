using System;

namespace CoreLib
{
    public interface IStatistics
    {
        int Id { get; set; }
        double SpeedWpm { get; set; }
        int SpeedCpm { get; set; }
        double Accuracy { get; set; }
        double TrueAccuracy { get; set; }
        int Correct { get; set; }
        int Errors { get; set; }
        long Entries { get; set; }
        TimeSpan TimeTaken { get; set; }
        string TimeTakenString { get; set; }
    }
}
