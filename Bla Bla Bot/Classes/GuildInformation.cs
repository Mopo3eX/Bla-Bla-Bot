using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Classes
{
    public class GuildInformation
    {
        public GuildInformation(DiscordGuild guild)
        {
            Guild = guild;
            Settings = new GuildSettings();
            SaveSettings();
        }
        public GuildInformation(DiscordGuild guild,GuildSettings settings)
        {
            Guild = guild;
            Settings = settings;
        }
        public DiscordGuild Guild;
        public GuildSettings Settings;

        public void SaveSettings()
        {
            string settings = JsonConvert.SerializeObject(Settings);
            File.WriteAllText($".\\Settings\\GUILD {Guild.Id}.json", settings);
        }
        public void LoadSettings()
        {
            if (File.Exists($".\\Settings\\GUILD {Guild.Id}.json"))
            {
                string settings_json = File.ReadAllText($".\\Settings\\GUILD {Guild.Id}.json");
                GuildSettings settings = JsonConvert.DeserializeObject<GuildSettings>(settings_json);
                Settings = settings;
            }
        }
        public override string ToString()
        {
            return $"{Guild.Name}|{Guild.Id}";
        }
    }
}
