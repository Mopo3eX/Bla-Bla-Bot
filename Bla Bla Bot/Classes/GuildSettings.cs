using Bla_Bla_Bot.Classes.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes
{
    public class GuildSettings
    {
        public GuildSettings()
        {
            
        }
        public bool BotActive = false;
        public AntiFloodMentions antiFloodMetions = new AntiFloodMentions();
        public AntiFlood antiFlood = new AntiFlood();
        public SendEmote sendEmote = new SendEmote();
        public RoleInfo RoleMute = new RoleInfo();
        public GuardRole guardRole = new GuardRole();
    }
    public class RoleInfo
    {
        public string RoleName = "";
        public ulong RoleID = 0;
        public override string ToString()
        {
            return RoleName;
        }
    }
}
