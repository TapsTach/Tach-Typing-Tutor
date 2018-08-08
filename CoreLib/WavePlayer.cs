using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoreLib
{
    public class WavePlayer
    {
        Random rand = new Random();
        int cuttingEdge;
        public enum PlayingState
        {
            Paused, Playing, Stopped
        }
        #region events
        public event Action Finished;
        #endregion

        #region fields
        WaveFileReader reader;
        DirectSoundOut output;
        WaveChannel32 provider;
        byte[] soundBuffer;
        Dictionary<string, byte[]> mappings = new Dictionary<string, byte[]>();
        Timer timer = new Timer();
        #endregion


        #region Properties
        public bool Cut { get; set; } = false;
        public PlayingState State
        {
            get
            {
                switch (output.PlaybackState)
                {
                    case PlaybackState.Stopped:
                        return PlayingState.Stopped;

                    case PlaybackState.Playing:
                        return PlayingState.Playing;

                    case PlaybackState.Paused:
                        return PlayingState.Paused;

                    default:
                        return PlayingState.Stopped;
                }

            }
        }

        public double Volume
        {
            //Setting volume not supported on DirectSoundOut, adjust the volume on your WaveProvider instead
            get => provider != null ? provider.Volume : 0;
            set
            {
                if (provider != null)
                    provider.Volume = (float)value;
            }
        }

        #endregion

        public WavePlayer()
        {
            timer.Interval = 200;
            timer.Elapsed += Timer_Elapsed;
            output = new DirectSoundOut();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Cut)
            {
                if (provider.Position > provider.Length - cuttingEdge)
                {
                    Finished?.Invoke();
                }

            }
            else
            {
                if (provider.Position > provider.Length)
                {
                    Finished?.Invoke();
                    timer.Stop();
                }
            }
        }
        #region Methods
        public void Play()
        {
            timer.Start();
            if (provider.Position > 0)
                provider.Position = 0;
            output.Play();
        }
        public void LoadSound(string soundName, string fileName)
        {
            Load(fileName);
        }
        public void LoadSound(string soundName, Stream soundStream)
        {
            DisposeSound();
            byte[] buffer = new byte[soundStream.Length];
            soundStream.Read(buffer, 0, buffer.Length);
            mappings.Add(soundName, buffer);

        }
        public void LoadSound(string soundName, byte[] soundBuffer)
        {

        }

        public void PlaySound(string soundName)
        {
            soundBuffer = mappings.Where(mp => mp.Key == soundName).First().Value;
            Play();
        }

        public void Load(string fileName)
        {
            soundBuffer = File.ReadAllBytes(fileName);
            Load();
        }
        void Load()
        {
            DisposeSound();
            reader = new WaveFileReader(new MemoryStream(soundBuffer));
            output = new DirectSoundOut();
            provider = new WaveChannel32(reader);
            output.Init(provider);
            cuttingEdge = rand.Next(0, (int)(soundBuffer.Length / 2));

        }



        private void Provider_Sample(object sender, SampleEventArgs e)
        {
            Debug.Write($"Left {e.Left}");
            Debug.WriteLine($"      Right {e.Right}");
        }

        public void Load(Stream stream)
        {

        }

        public void Load(byte[] buffer)
        {
            soundBuffer = buffer;
            Load();
        }

        public void Stop()
        {
            output.Stop();
        }
        public void Pause()
        {
            output.Pause();
        }

        public void Resume()
        {
            output.Play();
        }

        public void DisposeSound()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                    output.Stop();
                output.Dispose();

            }
            if (reader != null)
            {
                reader.Dispose();
                reader.Close();
            }
            if (provider != null)
            {
                provider.Dispose();
                provider.Close();
            }

        }

        #endregion
    }
}
