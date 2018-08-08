using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class StatisticsMeasurer
    {
        static StatisticStruct current;
        public async static Task<StatisticStruct> GetStatisticsAsync(TimeSpan timeSpan, int strokes, string expectedText, string enteredText)
        {
            return await Task.Run(() =>
             {
                 int errors = 0;
                 int entries = enteredText.Length;
                 int correctedErrors = strokes - entries;

                 for (int i = 0; i < enteredText.Length; i++)
                 {
                     if(expectedText[i] != enteredText[i])
                     {
                         errors++;
                     }
                 }

                 StatisticStruct stats = new StatisticStruct();
                 stats.Entries = entries;
                 stats.Errors = errors;
                 stats.Correct = entries - (errors + correctedErrors);
                 stats.TimeTaken = timeSpan;
                 stats.TimeTakenString = $"{timeSpan.Hours}h:{timeSpan.Minutes}m:{timeSpan.Seconds}s";

                 stats.SpeedWpm = GetSpeedWPM(timeSpan.TotalMinutes, entries, errors);
                 stats.SpeedCpm = GetSpeedCpm(timeSpan.TotalMinutes, entries, errors);
                 stats.TrueAccuracy = GetAccuracy(entries, errors + correctedErrors);
                 stats.Accuracy = GetAccuracy(entries, errors);

                 return current =  stats;
             });

        }

        static double GetAccuracy(int entries, int errors)
        {
            return entries == 0 ? 100 :Math.Round(((double)(entries - errors) / entries) * 100,2);
        }

        static double GetSpeedWPM(double time, int numberOfEntries, int errors)
        {
            
            double grossSpeed = GetGrossSpeedWpm(numberOfEntries, time);
            double netSpeed = 0d;

            //penalize errors
            if (errors > 0)
            {
                netSpeed = grossSpeed - (errors * time);
               // Debugger.Break();
                Debug.WriteLine($"gross speed is {grossSpeed}  net speed is {netSpeed}");
            }
            else netSpeed = grossSpeed;
            netSpeed = netSpeed < 0 ? 0 : netSpeed;
            return Math.Round(netSpeed, 2);

        }
        static int GetSpeedCpm(double time, int entries, int errors)
        {
            //Net CPM = CPM - ( Characters with errors / Time spent in minutes )
            return (int)Math.Round((GetGrossCpmSpeed(time, entries) - (errors / time)) / time);
        }
        static int GetGrossCpmSpeed(double time, int entries)
        {
            //CPM = ( Characters without errors + Characters with errors ) / Time spent in minutes
            return (int)Math.Round(entries / time);

        }
        static double GetGrossSpeedWpm(int entriesLength, double time)
        {
            
            return Math.Round((entriesLength / 5) / time, 2);
        }

        public static StatisticStruct GetCurrentStatistics()
        {
            return current;
        }
    }
}