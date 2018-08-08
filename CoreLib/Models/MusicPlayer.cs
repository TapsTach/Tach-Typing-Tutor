using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreExtLib;
using NAudio.Wave;

namespace CoreLib
{
    public static class MusicPlayer
    {
        static WavePlayer player = new WavePlayer();
        static Mp3Player mp3Player = new Mp3Player();
        static Mp3Player effectPlayer = new Mp3Player();
        static bool isMp3 = false;
        static Random rand = new Random();
        static object lockObject = new object();
        private static bool paused;
        static bool Playing { get; set; }
        static MusicPlayer()
        {
            player.Finished += Player_Finished;
            player.Cut = true;

            mp3Player.Finished += Player_Finished;
            mp3Player.Cut = true;

            Speaker.OnFinishedSpeaking += Speaker_OnFinishedSpeaking;
            Speaker.OnStartedSpeaking += Speaker_OnStartedSpeaking;

        }

        private static void Speaker_OnStartedSpeaking()
        {
            if (isMp3)
            {
                if (mp3Player.State == Mp3Player.PlayingState.Playing)
                {
                    mp3Player.Volume = 0.03;
                }
                return;
            }
            if (player.State == WavePlayer.PlayingState.Playing)
            {
                player.Volume = 0.03;
            }


        }

        private static void Speaker_OnFinishedSpeaking()
        {
            if (isMp3)
            {
                if (mp3Player.State == Mp3Player.PlayingState.Playing)
                {
                    mp3Player.Volume = 1;
                }
                return;
            }
            if (player.State == WavePlayer.PlayingState.Playing)
            {
                player.Volume = 1;
            }
        }

        private static void Player_Finished()
        {
            Playing = false;
            if (rand.Next(0, 3) == 2)
                PlayEffect();
            else
                PlayAsync();
        }

        private static void PlayEffect()
        {
            try
            {
                string dir = "sounds/effects";
                if (!Directory.Exists(dir))
                    return;
                var files = Directory.GetFiles(dir).Where(f => f.EndsWith(".mp3")).ToArray();
                if (files.Length > 0)
                {
                    Task.Run(() =>
                    {

                        effectPlayer.Load(files.ChooseRandomly());
                        effectPlayer.Play();

                        Thread.Sleep(200);
                        //don't want it to always do the same action always.
                        if (rand.Next(0, 10) % 2 == 0)
                        {
                            if (isMp3)
                            {
                                LowerVolume(mp3Player, 5, 150);
                            }
                        }

                        PlayAsync();

                        //don't want it to always do the same action always.
                        if (rand.Next(0, 10) % 2 == 0)
                        {
                            effectPlayer.Load(files.ChooseRandomly());
                            effectPlayer.Play();
                        }
                    });
                }
            }
            catch
            {

            }
        }

        public static async void PlayAsync()
        {
            if (Settings.GetSettings().MusicFiles.Count == 0 || !Settings.GetSettings().MusicOn)
                return;
            try
            {
                if (paused)
                {
                    await ResumeAsync();
                    return;
                }

                if (Playing) return;

                var musicFile = Settings.GetSettings().MusicFiles.ChooseRandomly();
                string file = musicFile.FileName;
                string path = musicFile.FilePath;
                byte[] buffer = null;
                if (file.ToLower().EndsWith(".mp3"))
                {
                    isMp3 = true;
                    await Task.Run(() =>
                    {
                        mp3Player.Load(path);
                        ///buffer = await ConvertMp3ToWaveAsync(path);
                        mp3Player.Play();
                    });
                    Playing = true;
                    return;
                }
                isMp3 = false;
                buffer = File.ReadAllBytes(path);
                player.Load(buffer);
                player.Play();
                Playing = true;
            }
            catch (Exception ex)
            {

                Playing = false;
            }
        }

        private async static Task<byte[]> ConvertMp3ToWaveAsync(string path)
        {
            return await Task.Run(() =>
            {
                using (Mp3FileReader reader = new Mp3FileReader(path))
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(reader))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            WaveFileWriter.WriteWavFileToStream(stream, pcm);
                            return stream.GetBuffer();

                        }
                    }
                }
            });
        }

        public async static void PauseAsync()
        {

            if (isMp3)
            {
                if (mp3Player.State != Mp3Player.PlayingState.Playing)
                    return;
                paused = true;
                await Task.Run(() =>
                {

                    LowerVolume(mp3Player);
                    lock (lockObject)
                    {
                        mp3Player.Pause();
                    }
                });
                return;
            }

            if (player.State != WavePlayer.PlayingState.Playing)
                return;
            paused = true;
            await Task.Run(() =>
                {

                    player.Volume = 1;
                    for (int i = 0; i < 10; i++)
                    {
                        player.Volume -= ((double)1 / 10);
                        Thread.Sleep(150);
                    }
                    lock (lockObject)
                    {
                        player.Pause();
                    }
                });
        }


        public static async Task ResumeAsync()
        {
            if (isMp3)
            {

                if (mp3Player.State != Mp3Player.PlayingState.Paused)
                    return;
                paused = false;
                await Task.Run(() =>
                {
                    lock (lockObject)
                        mp3Player.Resume();
                    RaiseVolume(mp3Player);

                });
                return;
            }
            if (player.State != WavePlayer.PlayingState.Paused)
                return;
            paused = false;
            await Task.Run(() =>
            {
                lock (lockObject)
                    player.Resume();
                for (int i = 0; i < 10; i++)
                {
                    player.Volume += ((double)1 / 10);
                    Thread.Sleep(150);
                }
                player.Volume = 1;

            });
        }

        private static void LowerVolume(Mp3Player player, int max = 10, int sleepTime = 150)
        {
            player.Volume = 1;
            for (int i = 0; i < max; i++)
            {
                player.Volume -= ((double)1 / 10);
                Thread.Sleep(sleepTime);
            }
        }

        private static void RaiseVolume(Mp3Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                player.Volume += ((double)1 / 10);
                Thread.Sleep(150);
            }
            player.Volume = 1;
        }

        public static void Stop()
        {
            player.Stop();
        }
    }

}
