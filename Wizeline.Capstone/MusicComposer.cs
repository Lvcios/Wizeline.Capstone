using OpenAI_API.Chat;
using OpenAI_API.Models;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Moderation;

namespace Wizeline.Capstone
{
    public class MusicComposer
    {
        public static async Task<List<Chord>> Compose(string prompt)
        {
            OpenAIAPI api = new OpenAIAPI(System.IO.File.ReadAllText(".env"));

            /*
             * Let's create a new chat completion and specify how we need the system acts like.
             * In this case, I wanted the system to as a music composer who will be requested to create chord progressions
             * based on feelings, places, actions and ages among other things the user provides.
             * Also, I ask the API to return the response in a certain format that I can manipulate easily.
             */
            var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.5,
                MaxTokens = 100,
                Messages = new ChatMessage[] {
                    new ChatMessage(ChatMessageRole.System, "You are a music composer. The users will request chord progressions based on what they want by describing feelings, places, music genres, and ages among other things.\r\nYou must return only the chord progression with the notes for each chord. Return a message for each chord and its note like follows :\r\n\r\nFmaj7: F-A-C-E\r\n\r\nDm9: D-F-A-C-E\r\n\r\nGm7: G-Bb-D-F\r\n\r\nC7: C-E-G-Bb \r\n Finally, do not add any description."),
                    new ChatMessage(ChatMessageRole.User, prompt)
                }
            });

            //process the OpenAI output to convert the text to the following object:
            //List<Chord> chordProgression = new List<Chord>();
            //Chord Progression: Fmaj7 - Dm9 - Gm7 - C7
            //Fmaj7: F - A - C - E
            //Dm9: D - F - A - C - E
            //Gm7: G - Bb - D - F
            //C7: C - E - G - Bb
            var reply = result.Choices[0].Message;
            Console.WriteLine($"{reply.Role}: {reply.Content.Trim()}");
            // or
            Console.WriteLine(result);
            Console.WriteLine(reply.Content.Split("\n\n"));

            var chords = reply.Content.Split("\n\n");
            List<Chord> chordProgression = new List<Chord>();
            foreach (var data in chords) 
            {
                var splittedData = data.Split(":");
                var chordNotes = splittedData[1];
                var chordName = splittedData[0];
                chordProgression.Add(new Chord { Name = chordName, Notes = chordNotes});
            }
            

            //chordProgression.Add(new Chord { Name = "Fmaj7", Notes = "F - A - C - E" });
            //chordProgression.Add(new Chord { Name = "Dm9", Notes = "D - F - A - C - E" });
            //chordProgression.Add(new Chord { Name = "Gm7", Notes = "G - Bb - D - F" });
            //chordProgression.Add(new Chord { Name = "C7", Notes = "C - E - G - Bb" });

            return chordProgression;
        }
    }
}
