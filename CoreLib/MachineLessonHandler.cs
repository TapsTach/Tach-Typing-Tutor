using CoreExtLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CoreLib.ScriptEngine;

namespace CoreLib
{
    public static class MachineLessonHandler
    {
        public enum DesiredView
        {
            Board, View
        }
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        public static event Action<CommandAction> ActionExecuted;
        static List<MachineLesson> machineLessons;
        static List<Article> articles;
        static MachineLesson currentLesson;
      
        static bool reachedMaxLen;
        static MachineLessonHandler()
        {
            machineLessons = Database.Query<MachineLesson>();
            articles = Database.Query<Article>();
            //string s = File.ReadAllText(@"C:\Users\Tapiwa Tachiona\Documents\lesson1.txt");
            //MachineLesson l = machineLessons[0];
            //l.Text = s;
            //Database.Update(l);

        }

        public static void Next()
        {
            if (!UiEnabled)
                return;
            if (machineLessons.Count == 0)
                return;
            if (!reachedMaxLen)
                ++Settings.GetSettings().MachineLessonIndex;
            Go();
        }

        public static void Prev()
        {
            if (!UiEnabled)
                return;
            if (machineLessons.Count == 0)
                return;
            if (Settings.GetSettings().MachineLessonIndex > 0)
            {
                --Settings.GetSettings().MachineLessonIndex;
            }

            Go();
        }
        static void Go()
        {
            if (!UiEnabled)
                return;
            var lessons = machineLessons.Where(l => l.Level == Settings.GetSettings().MachineLessonIndex).ToList();
            if (lessons.Count > 0)
            {
                reachedMaxLen = false;

                currentLesson = lessons.ChooseRandomly();
                executingActions = GetActionMapping(currentLesson.Text);
                Execute(executingActions);
            }
            else
            {
                reachedMaxLen = true;
                MakeLessonFromArticle();
            }
        }

        public static void Skip()
        {
            tokenSource.Cancel();
            //executingActions = executingActions.Where(a => a.Command == CommandAction.lesson).ToList();
            //Execute(executingActions);
        }
        public static void Restart()
        {
            if (!UiEnabled)
                return;
            if (Settings.GetSettings().MachineLessonIndex < 0)
                Settings.GetSettings().MachineLessonIndex = 0;
            else if (Settings.GetSettings().MachineLessonIndex >= machineLessons.Count)
                Settings.GetSettings().MachineLessonIndex = machineLessons.Count - 1;

            currentLesson = machineLessons[Settings.GetSettings().MachineLessonIndex];

            Go();

        }
        private static void MakeLessonFromArticle()
        {
            Article art = articles.ChooseRandomly();
            Lesson = art;
            UnHighlight = false;
            ActionExecuted?.Invoke(CommandAction.lesson);
        }

        public async static void GoMachine()
        {
            if (!Greeter.Greeted)
                await Greeter.Greet();
            Restart();
        }

        public static string Text { get; set; }
        public static Lesson Lesson { get; set; }
        public static string HighlightedKeys { get; set; }
        public static string Highlighted { get; set; }
        public static DesiredView View { get; set; }
        public static bool Keyboard { get; set; }
        public static bool UiEnabled { get; set; } = true;
        public static bool UnHighlight { get; set; }

        static List<CommandMapping> executingActions;
        private async static void Execute(List<CommandMapping> actions)
        {
            foreach (var v in actions)
            {
                if (Settings.GetSettings().IsTutorial == false && v.Command != CommandAction.lesson)
                    continue;
                switch (v.Command)
                {
                    case CommandAction.highlightKeys:
                        HighlightedKeys = v.Parameter;
                        UnHighlight = false;
                        break;
                    case CommandAction.type:
                        // Text = dic.Value;
                        UnHighlight = false;
                        break;
                    case CommandAction.print:
                        Text = v.Parameter;
                        UnHighlight = false;
                        break;
                    case CommandAction.highlight:
                        Highlighted = v.Parameter;
                        UnHighlight = false;
                        break;
                    case CommandAction.showBoard:
                        View = DesiredView.Board;
                        UnHighlight = false;
                        break;
                    case CommandAction.showView:
                        View = DesiredView.View;
                        UnHighlight = false;
                        break;
                    case CommandAction.lesson:
                        string[] splitString = v.Parameter.Split("<<");
                        if (splitString.Length != 2)
                            splitString = new string[] { v.Parameter, $"Tutor Lesson {Settings.GetSettings().MachineLessonIndex}" };

                        Lesson = new Lesson()
                        {
                            Text = splitString[0],
                            Name = splitString[1]
                        };
                        UnHighlight = false;
                        break;
                    case CommandAction.disableUI:
                        UnHighlight = false;
                        UiEnabled = false;
                        break;
                    case CommandAction.enableUI:
                        UnHighlight = false;
                        UiEnabled = true;
                        break;
                    case CommandAction.showKeyboard:
                        UnHighlight = false;
                        Keyboard = true;
                        break;
                    case CommandAction.hideKeyboard:
                        UnHighlight = false;
                        Keyboard = false;
                        break;
                    case CommandAction.unHiglight:
                        UnHighlight = true;
                        break;
                    case CommandAction.speak:
                        await Speaker.SpeakAsync(v.Parameter, tokenSource.Token);
                        UnHighlight = false;
                        break;
                    case CommandAction.delay:
                        try
                        {
                            await Task.Delay(int.Parse(v.Parameter), tokenSource.Token);
                        }
                        catch { }
                        UnHighlight = false;
                        break;
                    default:
                        break;
                }
                ActionExecuted?.Invoke(v.Command);

                if (tokenSource.Token.IsCancellationRequested)
                {
                    tokenSource = new CancellationTokenSource();
                    //break;
                }

            }
        }

    }
}
