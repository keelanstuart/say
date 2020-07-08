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
		static public SpeechSynthesizer synth = new SpeechSynthesizer();
		static public string[] voicename = new string[(int)VoiceGender.Neutral + 1];

		static void Main(string[] args)
		{
			synth.SetOutputToDefaultAudioDevice();


			bool first = true;
			foreach (InstalledVoice voice in synth.GetInstalledVoices())
			{
				VoiceInfo info = voice.VoiceInfo;

				string AudioFormats = "";
				foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
				{
					AudioFormats += String.Format("{0}\n",
					fmt.EncodingFormat.ToString());
				}

				// extract the names for gendered voices, but make sure we have no empties
				if (first)
				{
					for (int i = (int)VoiceGender.NotSet; i <= (int)VoiceGender.Neutral; i++)
						voicename[i] = info.Name;
				}
				else
					voicename[(int)info.Gender] = info.Name;

#if DEBUG
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
#endif
				first = false;
			}

			if (args.Count() > 0)
			{
				foreach (string arg in args)
				{
					if (SelectVoiceFromString(arg))
						continue;

					synth.Speak(arg);
				}
			}
			else
			{
				string line;

				if (Console.IsInputRedirected)
				{
					// redirects do something special-- they let the synth process SSML tags
					using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
					{
						string ssml = reader.ReadToEnd();

						synth.SpeakSsml(ssml);
					}
				}
				else
				{
					// read lines from the console and say them until, ostensibly, ctrl-z is pressed
					while (!string.IsNullOrEmpty(line = Console.ReadLine()))
					{
						// switch to a different voice?
						if (SelectVoiceFromString(line))
						{
							Console.WriteLine("[new voice selected: \"" + synth.Voice.Name + "\"");
							continue;
						}

						synth.Speak(line);
					}
				}
			}
		}

		static public bool SelectVoiceFromString(string s)
		{
			if (s[0] == '/')
			{
				string lcarg = s.ToLower();

				if (lcarg.CompareTo("/m") == 0)
					synth.SelectVoice(voicename[(int)VoiceGender.Male]);
				else if (lcarg.CompareTo("/f") == 0)
					synth.SelectVoice(voicename[(int)VoiceGender.Female]);
				else if (lcarg.CompareTo("/n") == 0)
					synth.SelectVoice(voicename[(int)VoiceGender.Neutral]);

				return true;
			}

			return false;
		}
	}
}