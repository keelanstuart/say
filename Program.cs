using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.IO;


namespace say
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.SetOutputToDefaultAudioDevice();

                    foreach (InstalledVoice voice in synth.GetInstalledVoices())
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        string AudioFormats = "";
                        foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                        {
                            AudioFormats += String.Format("{0}\n",
                            fmt.EncodingFormat.ToString());
                        }

                        Console.WriteLine(" Name:          " + info.Name);
                        Console.WriteLine(" Culture:       " + info.Culture);
                        Console.WriteLine(" Age:           " + info.Age);
                        Console.WriteLine(" Gender:        " + info.Gender);
                        Console.WriteLine(" Description:   " + info.Description);
                        Console.WriteLine(" ID:            " + info.Id);
                        Console.WriteLine(" Enabled:       " + voice.Enabled);
                        if (info.SupportedAudioFormats.Count != 0)
                        {
                            Console.WriteLine(" Audio formats: " + AudioFormats);
                        }
                        else
                        {
                            Console.WriteLine(" No supported audio formats found");
                        }

                        string AdditionalInfo = "";
                        foreach (string key in info.AdditionalInfo.Keys)
                        {
                            AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                        }

                        Console.WriteLine(" Additional Info - " + AdditionalInfo);
                        Console.WriteLine();

                        //synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Senior);
                        synth.SelectVoice(info.Name);
                        synth.Speak(args[0]);
                    }
                }
            }
            else if (Console.IsInputRedirected)/**/
            {
                //				Console.WriteLine("redirected...");
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    string line;
                    using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
                    {
                        //  nothing gets read here ?!
                        while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                        {
                            Console.WriteLine(line);
                            synth.Speak(line);
                        }
                        synth.Speak(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        Console.WriteLine(line);
                        synth.Speak(line);
                    }
                }
            }
        }
    }
}