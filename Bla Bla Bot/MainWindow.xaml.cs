using Bla_Bla_Bot.Classes;
using Bla_Bla_Bot.Components;
using Bla_Bla_Bot.Helpers;
using Bla_Bla_Bot.Properties;
using DSharpPlus;
using DSharpPlus.Entities;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bla_Bla_Bot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            staticLog = log_box;
            t.Interval = 1000;
            t.Elapsed += T_Elapsed;
            t.AutoReset = true;
            t.Start();
            sdispatcher = Dispatcher;
            Keys.Add(new KeysImgur("b4b7c95aa88f318", "14bf519424e52f9f2020fa453dcfdcdf11f21a41"));
            Keys.Add(new KeysImgur("ab85ef4f9e5c113", "7574530ad6fe3689d908e105d53333c8906195bf"));
        }
        List<KeysImgur> Keys = new List<KeysImgur>();
        public static TextBox staticLog;
        bool Bot_Active = false;
        public static int EventsCount = 0;
        public static int MessagesCount = 0;
        public static int CountTime = 0;
        int waitingtime = 60 * 30;
        public static Dispatcher sdispatcher;
        Timer t = new Timer();

        public static List<GuildInformation> Guilds = new List<GuildInformation>();
        public static Dictionary<ulong,GuildInformation> GuildsDictionary = new Dictionary<ulong, GuildInformation>();
        public static DiscordClient Client { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Bot_Active)
            {
                enable.IsEnabled = false;
                var cfg = new DiscordConfiguration
                {
                    Token = Properties.Resources.TOKEN,
                    TokenType = TokenType.Bot,

                    AutoReconnect = true,
                    LogLevel = LogLevel.Debug,
                    UseInternalLogHandler = true
                };

                Client = new DiscordClient(cfg);
                Client.GuildAvailable += Client_GuildAvailable;
                Client.MessageCreated += Anti_Flood_Mentions.Client_MessageCreated;
                Client.MessageCreated += Anti_Flood.Client_MessageCreated;
                Client.MessageCreated += SendEmote.Client_MessageCreated;
                Client.MessageReactionAdded += SendEmote.Client_MessageReactionAdded;
                Client.GuildRoleDeleted += GuardRole.Client_GuildRoleDeleted;
                Client.GuildRoleUpdated += GuardRole.Client_GuildRoleUpdated;
                list_guilds.Items.Clear();
                Client.ConnectAsync();
                Log.LogWrite("Main", $"Запустили бота.");
                disable.IsEnabled = true;
                Bot_Active = true;
            }
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            CountTime++;
            if (Bot_Active)
            {
                Dispatcher.BeginInvoke((Action)(() => status_label.Content = "Состояние: Включен"));
                SendEmote.RemoveOlder();
                Anti_Flood.RemoveOlder();
                Anti_Flood_Mentions.RemoveOlder();
            }
            else
            {
                Dispatcher.BeginInvoke((Action)(() => status_label.Content = "Состояние: Выключен"));
            }
            Dispatcher.BeginInvoke((Action)(() => eventscount_label.Content = $"Событий: {EventsCount}"));
            Dispatcher.BeginInvoke((Action)(() => messagescount_label.Content = $"Сообщений: {MessagesCount}"));
            if((CountTime % 60)==0)
            {
                Anti_Flood_Mentions.cache.Clear();
                Anti_Flood.cache.Clear();
            }
                   
        }

        private Task Client_GuildAvailable(DSharpPlus.EventArgs.GuildCreateEventArgs e)
        {
            EventsCount++;
            try
            {
                if(Guilds.Exists(x => x.Guild == e.Guild))
                    return Task.CompletedTask;
                if (!Directory.Exists("Settings"))
                    Directory.CreateDirectory("Settings");
                ulong GuildID = e.Guild.Id;
                GuildInformation newguild = null;
                if (File.Exists($".\\Settings\\GUILD {GuildID}.json"))
                {
                    string settings_json = File.ReadAllText($".\\Settings\\GUILD {GuildID}.json");
                    GuildSettings settings = JsonConvert.DeserializeObject<GuildSettings>(settings_json);
                    newguild = new GuildInformation(e.Guild, settings);
                    Guilds.Add(newguild);
                }
                else
                {
                    newguild = new GuildInformation(e.Guild);
                    Guilds.Add(newguild);
                }

                MainWindow.sdispatcher.BeginInvoke((Action)(() => list_guilds.Items.Add(newguild)));
                GuildsDictionary.Add(newguild.Guild.Id, newguild);
                Log.LogWrite("Main", $"Добавили сервер {newguild} в список.");
            }
            catch(Exception er)
            {
                Log.LogWrite("Main", $"Ошибка при добавлении сервера в список. {er.Message}.\r\n{er.StackTrace}");

            }
            return Task.CompletedTask;
        }
        public static Random rnd = new Random();
        

        private void disable_Click(object sender, RoutedEventArgs e)
        {
            if(Bot_Active)
            {
                disable.IsEnabled = false;
                Client.DisconnectAsync();
                enable.IsEnabled = true;
                Bot_Active = false;
                Log.LogWrite("Main", $"Остановили бота.");
            }
        }

        private void list_guilds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count>0)
            {
                EditSettings settings = new EditSettings();
                settings.information = (GuildInformation)e.AddedItems[0];
                settings.ParceSettings();
                settings.ShowDialog();
            }
        }
    }
    class KeysImgur
    {
        public KeysImgur(string id, string secret)
        {
            ClientID = id;
            Secret = secret;
        }
        public string ClientID = "";
        public string Secret = "";
    }
}
