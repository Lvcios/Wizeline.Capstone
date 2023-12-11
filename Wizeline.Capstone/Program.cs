using Hearn.Midi;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using static Hearn.Midi.MidiConstants;
using static Hearn.Midi.MidiStreamWriter;

namespace Wizeline.Capstone
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Lets provide the user prompt
            string instructions = "Give me a chord progression that makes me remember old but very good times nostalgically. " +
                "Consider I'm a Mexican person who grew up in a town near the beach in Veracruz. ";

            //the MusicComposer class peforms the request to OpenAI API
            var chordProgression =  await MusicComposer.Compose(instructions);

            //List<Chord> chordProgression = new List<Chord>();
            //Chord Progression: Fmaj7 - Dm9 - Gm7 - C7
            //Fmaj7: F - A - C - E
            //Dm9: D - F - A - C - E
            //Gm7: G - Bb - D - F
            //C7: C - E - G - Bb

            //chordProgression.Add(new Chord { Name = "Fmaj7", Notes = "F - A - C - E" });
            //chordProgression.Add(new Chord { Name = "Dm9", Notes = "D - F - A - C - E" });
            //chordProgression.Add(new Chord { Name = "Gm7", Notes = "G - Bb - D - F" });
            //chordProgression.Add(new Chord { Name = "C7", Notes = "C - E - G - Bb" });

            MusicEngine.CreateMIDI(chordProgression);
        }
    }
}