using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes.Settings
{
    public class AntiFlood
    {
        public AntiFlood()
        {

        }
        public bool Active = false;
        public int CountInMinute = 5;
        public List<ChannelInfo> Exceptions = new List<ChannelInfo>();
        public DoIs HowDo = DoIs.WarnAndDelete;
    }
    public class ChannelInfo
    {
        public string ChannelName = "";
        public ulong ChannelID = 0;
        public override string ToString()
        {
            return ChannelName;
        }
    }
}
