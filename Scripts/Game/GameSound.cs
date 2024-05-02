using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Scripts.Game
{
    public static class GameSound
    {
        static AudioBuffer audioBuffer; static SourceVoice sourceVoice;
        public static void PLaySoundFileLoop(XAudio2 device, string text, string fileName)
        {
            var stream = new SoundStream(File.OpenRead(fileName));
            var waveFormat = stream.Format;
            audioBuffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream,
                LoopCount = AudioBuffer.LoopInfinite
            };
            stream.Close();

            sourceVoice = new SourceVoice(device, waveFormat, true);
            sourceVoice.SubmitSourceBuffer(audioBuffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();
        }
        public static void PLaySoundFileOnce(XAudio2 device, string text, string fileName)
        {
            var stream = new SoundStream(File.OpenRead(fileName));
            var waveFormat = stream.Format;
            audioBuffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream,
            };
            stream.Close();

            sourceVoice = new SourceVoice(device, waveFormat, true);
            sourceVoice.SubmitSourceBuffer(audioBuffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();
        }
    }
}
