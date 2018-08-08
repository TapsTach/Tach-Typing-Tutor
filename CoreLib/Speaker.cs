using System;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class Speaker
    {
        static SpeechSynthesizer speaker = new SpeechSynthesizer();
        private static bool finished;
        public static event Action OnStartedSpeaking;
        public static event Action OnFinishedSpeaking;
        public static bool IsSpeaking { get; set; }
        static Speaker()
        {
            speaker.SpeakCompleted += Speaker_SpeakCompleted;
        }

        private static void Speaker_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            finished = true;
            IsSpeaking = false;
            OnFinishedSpeaking?.Invoke();
        }

        public async static Task SpeakAsync(string message, CancellationToken token)
        {
            await Task.Run(() =>
            {
                OnStartedSpeaking?.Invoke();
                IsSpeaking = true;
                finished = false;
                speaker.SpeakAsync(message);
                while (!finished)
                {
                    if (token.IsCancellationRequested)
                        speaker.SpeakAsyncCancelAll();
                    Thread.Sleep(200);
                }
            });
        }
    }
}
