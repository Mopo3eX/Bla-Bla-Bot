using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes.Settings
{
    public class SendEmote
    {
        public SendEmote()
        {

        }
        public bool Active = false;
        public List<Emote> Emotes = new List<Emote>();
    }
    public class Emote
    {
        public string EmoteName = "";
        public string Find = "";
        public string Text = "";
        public override string ToString()
        {
            return $"{EmoteName} | {Find}";
        }
    }
}
