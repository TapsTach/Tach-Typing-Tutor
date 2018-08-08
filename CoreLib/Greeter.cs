using CoreExtLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class Greeter
    {
        enum TimeOfTheDay
        {
            Evening, Morning, Afternoon
        }
        static TimeOfTheDay GetTimeOfDay()
        {
            DateTime time = DateTime.Now;
            if (time.Hour > 16 && time.Hour < 24)
                return TimeOfTheDay.Evening;
            else if (time.Hour > 11 && time.Hour < 17)
                return TimeOfTheDay.Afternoon;
            else return TimeOfTheDay.Morning;
        }

        static string greetings1;
        static string greetings3;
        static void SetGreetings()
        {
            switch (GetTimeOfDay())
            {
                case TimeOfTheDay.Evening:
                    greetings1 = new string[]
                    {
                        "Hello",
                        "Hie",
                        "Greetings",
                        "Good Evening",
                        "Evening"
                    }.ChooseRandomly();
                    greetings3 = new string[]
                    {
                        "I trust you had a wonderful day. ",
                        "I hope you had a wonderful day. ",
                        "I hope you are doing good. ",
                        "I trust you are doing good. ",
                        "I trust you are fine. ",
                        "I trust you are doing just fine. ",
                        "I trust you are doing fine. ",
                    }.ChooseRandomly();
                    break;
                case TimeOfTheDay.Morning:
                    greetings1 = new string[]
                    {
                        "Hello",
                        "Hie",
                        "Greetings",
                        "Good morning",
                        "Morning"
                    }.ChooseRandomly();
                    greetings3 = new string[]
                    {
                         "I hope you are doing good. ",
                        "I trust you are doing good. ",
                        "I trust you are doing great. ",
                        "I trust you are doing well. ",
                        "I trust you are fine. ",
                        "I trust you are doing just fine. ",
                        "I trust you are doing fine. ",
                    }.ChooseRandomly();
                    break;
                case TimeOfTheDay.Afternoon:
                    greetings1 = new string[]
                    {
                        "Hello",
                        "Hie",
                        "Greetings",
                        "Afternoon",
                        "Good afternoon",
                        "Good day"
                    }.ChooseRandomly();
                    greetings3 = new string[]
                    {
                        "I trust you are having a wonderful day. ",
                        "I hope you are having a wonderful day. ",
                        "I hope you are doing good. ",
                        "I trust you are doing good. ",
                        "I trust you are doing great. ",
                        "I trust you are doing well. ",
                        "I trust you are fine.",
                        "I trust you are doing just fine. ",
                        "I trust you are doing fine. ",
                    }.ChooseRandomly();
                    break;
                default:
                    break;
            }
        }

     

        public static bool Greeted { get; internal set; }

        public async static Task Greet()
        {
            SetGreetings();
            string message = greetings1 +", "+ greetings3 + " Welcome!.  ";
            await Speaker.SpeakAsync(message, CancellationToken.None);
            Greeted = true;

        }
    }
}
