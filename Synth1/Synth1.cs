using System.Windows.Forms;
using System.Media;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Security.Policy;
using System.Threading.Channels;

namespace Synth1
{
    public partial class Synth1 : Form
    {
        private const int SAMPLE_RATE = 44100; //how many samples of audio we want to generate for every second (44100 numbers representing the amplitude for every second of audio)
        private const short BITS_PER_SAMPLE = 16; //bit depth; what can be the maximum value of every sample (every sample can have up to 16 digits in binary)
        public Synth1()
        { 
            InitializeComponent();
        }

        private void Synth1_KeyDown(object sender, KeyEventArgs e)
        {
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)]; //data of wave divided into bytes
            float frequency = 440f;
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)); //sine wave table
            }
            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short)); //splitting every short into 2 bytes
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunk2Size = SAMPLE_RATE * 1 * blockAlign;//== NumSamples * NumChannels * BitsPerSample/8
                                                                 //This is the number of bytes in the data.
                                                                 //You can also think of this as the size
                                                                 //of the read of the subchunk following this
                                                                 //number.
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });//ChunkID, Contains the letters "RIFF" in ASCII form
                binaryWriter.Write(36 + subChunk2Size);          //ChunkSize, 36 + SubChunk2Size, or more precisely:
                                                                 //4 + (8 + SubChunk1Size) + (8 + SubChunk2Size)
                                                                 //This is the size of the rest of the chunk
                                                                 //following this number.This is the size of the
                                                                 //entire file in bytes minus 8 bytes for the
                                                                 //two fields not included in this count:
                                                                 //ChunkID and ChunkSize.
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E' });//Format, Contains the letters "WAVE" in ASCII form
                binaryWriter.Write(new[] { 'f', 'm', 't', ' ' });//Subchunk1ID, Contains the letters "fmt " in ASCII form
                binaryWriter.Write(16);                          //Subchunk1Size, 16 for PCM.  This is the size of the rest of the Subchunk which follows this number
                binaryWriter.Write((short)1);                    //AudioFormat(2 bytes), PCM = 1 (i.e. Linear quantization)
                                                                 //Values other than 1 indicate some
                                                                 //form of compression.
                binaryWriter.Write((short)1);                    //NumChannels(2 bytes),  Mono = 1, Stereo = 2, etc.
                binaryWriter.Write(SAMPLE_RATE);                 //SampleRate, 8000, 44100, etc.
                binaryWriter.Write(SAMPLE_RATE * 1 * blockAlign);//== SampleRate * NumChannels * BitsPerSample/8
                binaryWriter.Write(1 * blockAlign);              //== NumChannels * BitsPerSample/8 (2 bytes) The number of bytes for one sample including all channels.
                binaryWriter.Write(BITS_PER_SAMPLE);             //BitsPerSample (2 bytes), 8 bits = 8, 16 bits = 16, etc.
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });//Subchunk2ID, Contains the letters "data" in ASCII form
                binaryWriter.Write(subChunk2Size);               //== NumSamples * NumChannels * BitsPerSample/8
                binaryWriter.Write(binaryWave);
                memoryStream.Position = 0;
                new SoundPlayer(memoryStream).Play();
            }
        }

        private void Synth1_Load(object sender, EventArgs e)
        {

        }
    }
}
