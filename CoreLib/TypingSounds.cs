using System.IO;

namespace CoreLib
{
    public static class TypingSounds
    {
        static WavePlayer typingPlayer = new WavePlayer();
        static WavePlayer errorPlayer = new WavePlayer();
        static WavePlayer finishPlayer = new WavePlayer();
        static WavePlayer coinPlayer = new WavePlayer();
        static TypingSounds()
        {
            typingPlayer.Load(Path.Combine("sounds", "type.wav"));
            errorPlayer.Load(Path.Combine("sounds", "error.wav"));
            finishPlayer.Load(Path.Combine("sounds", "finish.wav"));
            coinPlayer.Load(Path.Combine("sounds", "cn1.wav"));
        }
        

        public static void PlayTypingSound()
        {
            if (Settings.GetSettings().SoundOn)
                typingPlayer.Play();
        }
        public static void PlayErrorSound()
        {
            if (Settings.GetSettings().SoundOn)
                errorPlayer.Play();
        }
        public static void PlayFinishSound()
        {
            if (Settings.GetSettings().SoundOn)
                finishPlayer.Play();
        }

        public static void PlayCoinSound()
        {
            if (Settings.GetSettings().SoundOn)
                coinPlayer.Play();
        }
    }
}
