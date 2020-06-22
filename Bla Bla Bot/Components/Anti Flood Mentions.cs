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
    public static class Anti_Flood_Mentions
    {
        public static Dictionary<DiscordGuild, Dictionary<DiscordUser, InfoMentions>> cache = new Dictionary<DiscordGuild, Dictionary<DiscordUser, InfoMentions>>();
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
            if(!info.Settings.antiFloodMetions.Active)
                return Task.CompletedTask;
            if (e.Author.IsBot)
                return Task.CompletedTask;
            if (!cache.ContainsKey(e.Guild))
                cache.Add(e.Guild, new Dictionary<DiscordUser, InfoMentions>());
            if (!cache[e.Guild].ContainsKey(e.Message.Author))
                cache[e.Guild].Add(e.Author, new InfoMentions());
            foreach(var mention in e.MentionedUsers)
            {
                if(cache[e.Guild][e.Author].MentionsMembers.ContainsKey(mention))
                {
                    if(cache[e.Guild][e.Author].MentionsMembers[mention]>=info.Settings.antiFloodMetions.CountInMinute)
                    {
                        Log.LogWrite("Anti Flood Mention", $"Обнаружили превышение упоминаний пользователей на сервере {info} от пользователя {e.Author}.");
                        try
                        {
                            if(info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Kick)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                if (member != null)
                                    member.RemoveAsync("Anti Flood Mention");
                                e.Message.DeleteAsync("Anti Flood Mention");
                                Log.LogWrite("Anti Flood Mention", $"Кикнули пользователя {e.Author} с сервера {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Ban)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                if (member != null)
                                    member.BanAsync(7, "Anti Flood Mention");
                                Log.LogWrite("Anti Flood Mention", $"Забанили пользователя {e.Author} на сервере {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.WarnAndDelete)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены.");
                                MainWindow.MessagesCount++;
                                Log.LogWrite("Anti Flood Mention", $"Предупредили пользователя {e.Author} на сервере {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Mute)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                DiscordRole role = info.Guild.GetRole(info.Settings.RoleMute.RoleID);
                                if (member.Roles.ToList().Contains(role))
                                    return Task.CompletedTask;
                                member.GrantRoleAsync(role);
                                MuteInfo inf = new MuteInfo();
                                inf.Time = MainWindow.CountTime;
                                inf.Role = role;
                                muted.Add(member, inf);
                                e.Message.DeleteAsync("Anti Flood Mention");
                                member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены и вам выдана роль мута на 1 минуты.");
                                MainWindow.MessagesCount++;
                                Log.LogWrite("Anti Flood Mention", $"Наказали пользователя {e.Author}.");
                            }
                        }
                        catch (Exception er)
                        {
                            Log.LogWrite("Anti Flood Mention", $"Ошибка при применении наказания. {er.Message}\r\n{er.StackTrace}");
                        }
                        return Task.CompletedTask;
                    }
                    else
                    {
                        cache[e.Guild][e.Author].MentionsMembers[mention]++;
                    }
                }
                else
                {
                    cache[e.Guild][e.Author].MentionsMembers.Add(mention, 1);
                }
            }
            foreach (var mention in e.MentionedRoles)
            {
                if (cache[e.Guild][e.Author].MentionsRole.ContainsKey(mention))
                {
                    if (cache[e.Guild][e.Author].MentionsRole[mention] >= info.Settings.antiFloodMetions.CountInMinute)
                    {
                        Log.LogWrite("Anti Flood Mention", $"Обнаружили превышение упоминаний ролей на сервере {info} от пользователя {e.Author}.");
                        try
                        {
                            if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Kick)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                if (member != null)
                                    member.RemoveAsync("Anti Flood Mention");
                                e.Message.DeleteAsync("Anti Flood Mention");
                                Log.LogWrite("Anti Flood Mention", $"Кикнули пользователя {e.Author} с сервера {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Ban)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                if (member != null)
                                    member.BanAsync(7, "Anti Flood Mention");
                                Log.LogWrite("Anti Flood Mention", $"Забанили пользователя {e.Author} на сервере {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.WarnAndDelete)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены.");
                                MainWindow.MessagesCount++;
                                Log.LogWrite("Anti Flood Mention", $"Предупредили пользователя {e.Author} на сервере {info}.");
                            }
                            else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Mute)
                            {
                                DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                                DiscordRole role = info.Guild.GetRole(info.Settings.RoleMute.RoleID);
                                if (member.Roles.ToList().Contains(role))
                                    return Task.CompletedTask;
                                member.GrantRoleAsync(role);
                                MuteInfo inf = new MuteInfo();
                                inf.Time = MainWindow.CountTime;
                                inf.Role = role;
                                muted.Add(member, inf);
                                e.Message.DeleteAsync("Anti Flood Mention");
                                member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены и вам выдана роль мута на 1 минуты.");
                                MainWindow.MessagesCount++;
                                Log.LogWrite("Anti Flood Mention", $"Наказали пользователя {e.Author}.");
                            }
                        }
                        catch (Exception er)
                        {
                            Log.LogWrite("Anti Flood Mention", $"Ошибка при применении наказания. {er.Message}\r\n{er.StackTrace}");
                        }
                        return Task.CompletedTask;
                    }
                    else
                    {
                        cache[e.Guild][e.Author].MentionsRole[mention]++;
                    }
                }
                else
                {
                    cache[e.Guild][e.Author].MentionsRole.Add(mention, 1);
                }
            }
            if(e.Message.MentionEveryone)
            {
                cache[e.Guild][e.Author].MentionEveryone++;
                if (cache[e.Guild][e.Author].MentionEveryone >= info.Settings.antiFloodMetions.CountInMinute)
                {
                    Log.LogWrite("Anti Flood Mention", $"Обнаружили превышение упоминаний всех на сервере {info} от пользователя {e.Author}.");
                    try
                    {
                        if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Kick)
                        {
                            DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                            if (member != null)
                                member.RemoveAsync("Anti Flood Mention");
                            e.Message.DeleteAsync("Anti Flood Mention");
                            Log.LogWrite("Anti Flood Mention", $"Кикнули пользователя {e.Author} с сервера {info}.");
                        }
                        else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Ban)
                        {
                            DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                            if (member != null)
                                member.BanAsync(7, "Anti Flood Mention");
                            Log.LogWrite("Anti Flood Mention", $"Забанили пользователя {e.Author} на сервере {info}.");
                        }
                        else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.WarnAndDelete)
                        {
                            DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                            member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены.");
                            MainWindow.MessagesCount++;
                            Log.LogWrite("Anti Flood Mention", $"Предупредили пользователя {e.Author} на сервере {info}.");
                        }
                        else if (info.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Mute)
                        {
                            DiscordMember member = info.Guild.GetMemberAsync(e.Author.Id).Result;
                            DiscordRole role = info.Guild.GetRole(info.Settings.RoleMute.RoleID);
                            if (member.Roles.ToList().Contains(role))
                                return Task.CompletedTask;
                            member.GrantRoleAsync(role);
                            MuteInfo inf = new MuteInfo();
                            inf.Time = MainWindow.CountTime;
                            inf.Role = role;
                            muted.Add(member, inf);
                            e.Message.DeleteAsync("Anti Flood Mention");
                            member.SendMessageAsync("Уважаемый пользователь, вы слишком часто отправляете сообщения на сервере, администрация может расценить это как флуд.\r\nВаши сообщения автоматически удалены и вам выдана роль мута на 1 минуты.");
                            MainWindow.MessagesCount++;
                            Log.LogWrite("Anti Flood Mention", $"Наказали пользователя {e.Author}.");
                        }
                    }
                    catch (Exception er)
                    {
                        Log.LogWrite("Anti Flood Mention", $"Ошибка при применении наказания. {er.Message}\r\n{er.StackTrace}");
                    }
                    return Task.CompletedTask;
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
    public class InfoMentions
    {
        public Dictionary<DiscordUser, int> MentionsMembers = new Dictionary<DiscordUser, int>();
        public Dictionary<DiscordRole, int> MentionsRole = new Dictionary<DiscordRole, int>();
        public int MentionEveryone = 0;
    }
}
