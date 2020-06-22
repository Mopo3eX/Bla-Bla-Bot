using Bla_Bla_Bot.Classes;
using Bla_Bla_Bot.Classes.Settings;
using Bla_Bla_Bot.Helpers;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Components
{
    public static class SendEmote
    {
        public static Dictionary<DiscordMessage, InfoEmote> cache = new Dictionary<DiscordMessage, InfoEmote>();
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
            if(!info.Settings.sendEmote.Active)
                return Task.CompletedTask;
            if (e.Author.IsBot)
                return Task.CompletedTask;
            if (e.Message.Content[0]!='!')
                return Task.CompletedTask;
            Regex regex = new Regex(@"!([a-zA-Z]+) ([a-zA-Zа-яА-Я ]+)");
            MatchCollection matches = regex.Matches(e.Message.Content);
            if (matches.Count > 0)
            {
                e.Channel.TriggerTypingAsync();
                string command = matches[0].Groups[1].Value;
                Emote emot = null;
                if (command != "emote")
                {
                    emot = info.Settings.sendEmote.Emotes.Find(x => x.EmoteName.ToLower() == command.ToLower());
                }
                else
                {
                    emot = new Emote();
                    emot.EmoteName = "custom";
                    emot.Find = matches[0].Groups[2].Value;
                    emot.Text = "";
                }
                string findtext = emot.Find;
                DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
                builder.Description = $"{e.Message.Author.Mention} {emot.Text}";
                builder.ImageUrl = FindGIF.GetRandomGIF(findtext, MainWindow.rnd.Next(0, 10) * 50);
                DiscordMessage mess =  e.Channel.SendMessageAsync(embed: builder.Build()).Result;
                DiscordEmoji emj = DiscordEmoji.FromName(MainWindow.Client, ":pencil2:");
                mess.CreateReactionAsync(emj);
                InfoEmote inf = new InfoEmote();
                inf.emote = emot;
                inf.usr = e.Author;
                inf.Time = MainWindow.CountTime;
                cache.Add(mess, inf);
                MainWindow.MessagesCount++;
                Log.LogWrite("Send Emote", $"Отправили эмоцию {emot.Find} от пользователя {e.Author}.");
            }
            return Task.CompletedTask;
        }

        public static Task Client_MessageReactionAdded(DSharpPlus.EventArgs.MessageReactionAddEventArgs e)
        {
            MainWindow.EventsCount++;
            if (e.Message.Channel.Guild == null)
                return Task.CompletedTask;
            if(!cache.ContainsKey(e.Message))
                return Task.CompletedTask;
            if(!cache[e.Message].Active)
                return Task.CompletedTask;
            if (e.User != cache[e.Message].usr)
                return Task.CompletedTask;
            DiscordEmoji emj = DiscordEmoji.FromName(MainWindow.Client, ":pencil2:");
            e.Message.DeleteReactionAsync(emj, cache[e.Message].usr, "Update embed");
            e.Channel.TriggerTypingAsync();
            Emote emot = cache[e.Message].emote;
            string findtext = emot.Find;
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
            builder.Description = $"{e.Message.Author.Mention} {emot.Text}";
            builder.ImageUrl = FindGIF.GetRandomGIF(findtext, MainWindow.rnd.Next(0, 10) * 50);
            e.Message.ModifyAsync(embed: builder.Build());
            Log.LogWrite("Send Emote", $"Заменили эмоцию {emot.EmoteName} от пользователя {e.User}.");
            return Task.CompletedTask;
        }

        public static void RemoveOlder()
        {
            foreach(var line in cache)
            {
                if (!line.Value.Active)
                    continue;
                if((MainWindow.CountTime - line.Value.Time)>60)
                {
                    line.Value.Active = false;
                    line.Key.DeleteAllReactionsAsync();
                    Log.LogWrite("Send Emote", $"Отключили возможность обновлять эмоцию на сообщении {line.Key}.");
                }
            }
        }

    }
    public class InfoEmote
    {
        public Emote emote = null;
        public DiscordUser usr = null;
        public int Time = 0;
        public bool Active = true;
    }
}
