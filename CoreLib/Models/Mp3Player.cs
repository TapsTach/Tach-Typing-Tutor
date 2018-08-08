
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

public class Mp3Player
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
    BlockAlignReductionStream reductionStream;
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

    public Mp3Player()
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
            if (provider.Position > provider.Length -100)
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


    public void Load(string fileName)
    {
        soundBuffer = File.ReadAllBytes(fileName);
        Load();
    }
    void Load()
    {
        DisposeSound();
        
        WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(new MemoryStream(soundBuffer)));
        output = new DirectSoundOut();
        reductionStream = new BlockAlignReductionStream(pcm);
        provider = new WaveChannel32(reductionStream);
        output.Init(provider);


        cuttingEdge = rand.Next(0, (int)(soundBuffer.Length / 2));

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
        try
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                    output.Stop();
                output.Dispose();

            }
            if (reductionStream != null)
            {
                reductionStream.Dispose();
                reductionStream.Close();
            }
            if (provider != null)
            {
                provider.Dispose();
                provider.Close();
            }
        }
        catch {
        }
    }

    #endregion
}

