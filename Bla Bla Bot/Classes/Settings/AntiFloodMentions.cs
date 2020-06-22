using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes.Settings
{
    public class AntiFloodMentions
    {
        public AntiFloodMentions()
        {

        }
        public bool Active = false;
        public int CountInMinute = 5;
        public DoIs HowDo = DoIs.Kick;
    }

    public enum DoIs
    {
        Ban,
        Kick,
        WarnAndDelete,
        Mute
    }
}