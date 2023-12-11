using Hearn.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hearn.Midi.MidiConstants;
using static Hearn.Midi.MidiStreamWriter;

namespace Wizeline.Capstone
{
    public class MusicEngine
    {
        public static void CreateMIDI(List<Chord> chordProgression)
        {

            //Let's create a new MIDI file
            var midiStream = new FileStream("music.mid", FileMode.OpenOrCreate);

            //Write the chord progressions
            using (var midiStreamWriter = new MidiStreamWriter(midiStream))
            {
                midiStreamWriter
                    .WriteHeader(Formats.MultiSimultaneousTracks, 2);

                midiStreamWriter
                    .WriteStartTrack()
                        .WriteTimeSignature(4, 4)
                        .WriteTempo(120)
                        .WriteString(StringTypes.TrackName, "Example Track")
                    .WriteEndTrack();

                midiStreamWriter.WriteStartTrack();
                foreach (Chord chord in chordProgression)
                {
                    var arrayNotes = chord.Notes.Replace(" ", "").Split('-');
                    

                    foreach (var note in arrayNotes)
                    {
                        midiStreamWriter.WriteNote(0, MusicEngine.ParseNote(note), 127, NoteDurations.WholeNote);
                    }

                    midiStreamWriter.Tick(NoteDurations.WholeNote);
                }
                midiStreamWriter.WriteEndTrack();
            }
        }

        public static MidiNoteNumbers ParseNote(string note)
        {
            note = note.Replace("#", "");
            Enum.TryParse<MidiNoteNumbers>($"{note}4", out MidiNoteNumbers midiNoteNumbers);
            return midiNoteNumbers;
        }
    }
}
