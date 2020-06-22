using Bla_Bla_Bot.Classes;
using Bla_Bla_Bot.Helpers;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Components
{
    public static class Anti_Flood
    {
        public static Dictionary<DiscordGuild, Dictionary<DiscordUser, int>> cache = new Dictionary<DiscordGuild, Dictionary<DiscordUser, int>>();
        public static Dictionary<DiscordMember, MuteInfo> muted = new Dictionary<DiscordMember, MuteInfo>();
        public static Task Client_MessageCreated(DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            MainWindow.EventsCount++;
            if(e.Guild==null)
                return Task.CompletedTask;
            if (!MainWindow.GuildsDictionary.ContainsKey(e.Guild.Id))
                return Task.CompletedTask;
            GuildInformation info = MainWindow.GuildsDictionary[e.Guild.Id];
            if(!info.Settings.BotActive)
                return Task.CompletedTask;
            if(!info.Settings.antiFlood.Active)
                return Task.CompletedTask;
            if(info.Settings.antiFlood.Exceptions.Exists(x => x.ChannelID == e.Channel.Id))
                return Task.CompletedTask;
            if(e.Author.IsBot)
                return Task.CompletedTask;
            if (!cache.ContainsKey(e.Guild))
                cache.Add(e.Guild, new Dictionary<DiscordUser, int>());
            if (!cache[e.Guild].ContainsKey(e.Message.Author))
                cache[e.Guild].Add(e.Author, 0);
            
            cache[e.Guild][e.Author]++;
            if (cache[e.Guild][e.Author] >= info.Settings.antiFlood.CountInMinute)
            {
                Log.LogWrite("Anti Flood", $"Обнаружили превышение сообщений на сервере {info} от пользователя {e.Author}.");
                try
                {
                    if (info.Settings.antiFlood.HowDo == Classes.Settings.DoIs.Kick)
                    {
                        DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                        if (member != null)
                            member.RemoveAsync("Anti Flood Mention");
                        e.Message.DeleteAsync("Anti Flood Mention");
                        Log.LogWrite("Anti Flood", $"Кикнули пользователя {e.Author} с сервера {info}.");
                    }
                    else if (info.Settings.antiFlood.HowDo == Classes.Settings.DoIs.Ban)
                    {
                        DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                        if (member != null)
                            member.BanAsync(7, "Anti Flood Mention");
                        Log.LogWrite("Anti Flood", $"Забанили пользователя {e.Author} на сервере {info}.");
                    }
                    else if (info.Settings.antiFlood.HowDo == Classes.Settings.DoIs.WarnAndDelete)
                    {
                        DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                        e.Message.DeleteAsync("Anti Flood Mention");
                        member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены.");
                        MainWindow.MessagesCount++;
                        Log.LogWrite("Anti Flood", $"Предупредили пользователя {e.Author}.");
                    }
                    else if (info.Settings.antiFlood.HowDo == Classes.Settings.DoIs.Mute)
                    {
                        DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                        DiscordRole role = info.Guild.GetRole(info.Settings.RoleMute.RoleID);
                        if(member.Roles.ToList().Contains(role))
                            return Task.CompletedTask;
                        member.GrantRoleAsync(role);
                        MuteInfo inf = new MuteInfo();
                        inf.Time = MainWindow.CountTime;
                        inf.Role = role;
                        if(muted.ContainsKey(member))
                            return Task.CompletedTask;
                        muted.Add(member, inf);
                        e.Message.DeleteAsync("Anti Flood Mention");
                        member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены и вам выдана роль мута на 1 минуты.");
                        MainWindow.MessagesCount++;
                        Log.LogWrite("Anti Flood", $"Наказали пользователя {e.Author}.");
                    }
                }
                catch (Exception er)
                {
                    Log.LogWrite("Anti Flood Mention", $"Ошибка при применении наказания. {er.Message}\r\n{er.StackTrace}");
                }
            }
            return Task.CompletedTask;
        }
        public static void RemoveOlder()
        {
            foreach (var line in muted)
            {
                if (!line.Value.Active)
                    continue;
                if ((MainWindow.CountTime - line.Value.Time) > 60)
                {
                    line.Value.Active = false;
                    line.Key.RevokeRoleAsync(line.Value.Role);
                    Log.LogWrite("Send Emote", $"Удалили роль мута {line.Key}.");
                }
            }
        }
    }
    public class MuteInfo
    {
        public int Time = 0;
        public DiscordRole Role = null;
        public bool Active = true;
    }
}
