using Bla_Bla_Bot.Classes;
using Bla_Bla_Bot.Classes.Settings;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bla_Bla_Bot
{
    /// <summary>
    /// Логика взаимодействия для EditSettings.xaml
    /// </summary>
    public partial class EditSettings : Window
    {
        public EditSettings()
        {
            InitializeComponent();
        }
        public GuildInformation information;

        public void ParceSettings()
        {
            ActiveBot.IsChecked = information.Settings.BotActive;
            antifloodActive.IsChecked = information.Settings.antiFloodMetions.Active;
            antifloodinminute.Text = information.Settings.antiFloodMetions.CountInMinute.ToString();
            if (information.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Kick)
            {
                DoMentionFloodKick.IsChecked = true;
                DoMentionFloodBan.IsChecked = false;
                DoMentionFloodWarn.IsChecked = false;
                DoMentionFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Ban)
            {
                DoMentionFloodKick.IsChecked = false;
                DoMentionFloodBan.IsChecked = true;
                DoMentionFloodWarn.IsChecked = false;
                DoMentionFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.WarnAndDelete)
            {
                DoMentionFloodKick.IsChecked = false;
                DoMentionFloodBan.IsChecked = false;
                DoMentionFloodWarn.IsChecked = true;
                DoMentionFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFloodMetions.HowDo == Classes.Settings.DoIs.Mute)
            {
                DoMentionFloodKick.IsChecked = false;
                DoMentionFloodBan.IsChecked = false;
                DoMentionFloodWarn.IsChecked = false;
                DoMentionFloodMute.IsChecked = true;
            }
            antifloodActive1.IsChecked = information.Settings.antiFlood.Active;
            antifloodinminute1.Text = information.Settings.antiFlood.CountInMinute.ToString();
            if (information.Settings.antiFlood.HowDo == DoIs.Kick)
            {
                DoAntiFloodKick.IsChecked = true;
                DoAntiFloodBan.IsChecked = false;
                DoAntiFloodWarn.IsChecked = false;
                DoAntiFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFlood.HowDo == DoIs.Ban)
            {
                DoAntiFloodKick.IsChecked = false;
                DoAntiFloodBan.IsChecked = true;
                DoAntiFloodWarn.IsChecked = false;
                DoAntiFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFlood.HowDo == DoIs.WarnAndDelete)
            {
                DoAntiFloodKick.IsChecked = false;
                DoAntiFloodBan.IsChecked = false;
                DoAntiFloodWarn.IsChecked = true;
                DoAntiFloodMute.IsChecked = false;
            }
            else if (information.Settings.antiFlood.HowDo == DoIs.Mute)
            {
                DoAntiFloodKick.IsChecked = false;
                DoAntiFloodBan.IsChecked = false;
                DoAntiFloodWarn.IsChecked = false;
                DoAntiFloodMute.IsChecked = true;
            }
            listexceptionchannels.Items.Clear();
            foreach (var info in information.Settings.antiFlood.Exceptions)
            {
                listexceptionchannels.Items.Add(info);
            }
            ReloadChannels();
            ReloadRoles();
            
            SendEmoteActive.IsChecked = information.Settings.sendEmote.Active;
            SendEmoteList.Items.Clear();
            
            foreach (var info in information.Settings.sendEmote.Emotes)
            {
                SendEmoteList.Items.Add((Emote)info);
                
            }
            GuardRolesActive.IsChecked = information.Settings.guardRole.Active;
        }

        private void ReloadChannels()
        {
            listchannels.Items.Clear();
            foreach (var channel in information.Guild.Channels)
            {
                if (channel.Type == DSharpPlus.ChannelType.Text && !information.Settings.antiFlood.Exceptions.Exists(x => x.ChannelID == channel.Id))
                    listchannels.Items.Add(channel);
            }
        }
        
        private void ReloadRoles(bool ReloadMute=true)
        {
            int num = 0;
            int select = -1;
            GuardRolesList.Items.Clear();
            foreach (var role in information.Guild.Roles)
            {
                if (ReloadMute)
                {
                    MuteRole.Items.Add(role);
                    if (role.Id == information.Settings.RoleMute.RoleID)
                    {
                        select = num;
                    }
                    num++;
                }
                if (!information.Settings.guardRole.GuardList.Exists(x=>x.RoleID==role.Id))
                    GuardRolesList.Items.Add(role);
            }
            GuardRolesListAdded.Items.Clear();
            foreach (var info in information.Settings.guardRole.GuardList)
            {
                GuardRolesListAdded.Items.Add(info);
            }
            if (ReloadMute)
            {
                if (select != -1)
                {
                    MuteRole.SelectedIndex = select;
                    DoAntiFloodMute.IsEnabled = true;
                    DoMentionFloodMute.IsEnabled = true;
                }
                else
                {
                    RoleInfo info = new RoleInfo();
                    information.Settings.RoleMute = info;
                    DoAntiFloodMute.IsEnabled = false;
                    DoMentionFloodMute.IsEnabled = false;
                    if (DoAntiFloodMute.IsChecked == true)
                    {
                        DoAntiFloodWarn.IsChecked = true;
                        information.Settings.antiFlood.HowDo = DoIs.WarnAndDelete;
                    }
                    if (DoMentionFloodMute.IsChecked == true)
                    {
                        DoMentionFloodWarn.IsChecked = true;
                        information.Settings.antiFloodMetions.HowDo = DoIs.WarnAndDelete;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((bool)DoAntiFloodMute.IsChecked)
            {
                if(information.Settings.RoleMute.RoleID==0)
                {
                    MessageBox.Show("Вы не выбрали роль для мута.");
                    return;
                }
            }
            if (information != null)
                information.SaveSettings();
            foreach (var guild in MainWindow.Guilds)
            {
                if (guild.Guild.Id == information.Guild.Id)
                    guild.LoadSettings();
            }
            if(sender!=null)
                Close();
        }

        private void ActiveBot_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.BotActive = (bool)ActiveBot.IsChecked;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach(var guild in MainWindow.Guilds)
            {
                if (guild.Guild.Id == information.Guild.Id)
                    guild.LoadSettings();
            }
            Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            antifloodinminute.IsEnabled = (bool)antifloodActive.IsChecked;
            antiflooddo.IsEnabled = (bool)antifloodActive.IsChecked;
            if (information != null)
                information.Settings.antiFloodMetions.Active = (bool)antifloodActive.IsChecked;
        }

        private void antifloodinminute_TextChanged(object sender, TextChangedEventArgs e)
        {
            int minutes = 5;
            if(int.TryParse(antifloodinminute.Text,out minutes))
            {
                if(information!=null)
                    information.Settings.antiFloodMetions.CountInMinute = minutes;
            }
        }

        private void antifloodActive1_Checked(object sender, RoutedEventArgs e)
        {
            antifloodinminute1.IsEnabled = (bool)antifloodActive1.IsChecked;
            antiflooddo1.IsEnabled = (bool)antifloodActive1.IsChecked;
            exceptions.IsEnabled = (bool)antifloodActive1.IsChecked;
            if (information != null)
                information.Settings.antiFlood.Active = (bool)antifloodActive1.IsChecked;
        }

        private void antifloodinminute1_TextChanged(object sender, TextChangedEventArgs e)
        {
            int minutes = 5;
            if (int.TryParse(antifloodinminute1.Text, out minutes))
            {
                if (information != null)
                    information.Settings.antiFlood.CountInMinute = minutes;
            }
        }

        private void antifloodinminute1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listchannels.SelectedItem == null)
                return;
            DiscordChannel ch = (DiscordChannel)listchannels.SelectedItem;
            ChannelInfo info = new ChannelInfo();
            info.ChannelName = ch.Name;
            info.ChannelID = ch.Id;
            listexceptionchannels.Items.Add(info);
            information.Settings.antiFlood.Exceptions.Add(info);
            ReloadChannels();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listexceptionchannels.SelectedItems.Count == 0)
                return;
            foreach(var item in listexceptionchannels.SelectedItems)
            {
                ChannelInfo info = (ChannelInfo)item;
                int num = information.Settings.antiFlood.Exceptions.FindIndex(x => x.ChannelID == info.ChannelID);
                if(num != -1)
                {
                    information.Settings.antiFlood.Exceptions.RemoveAt(num);
                }
            }
            listexceptionchannels.Items.Clear();
            foreach (var info in information.Settings.antiFlood.Exceptions)
            {
                listexceptionchannels.Items.Add(info);
            }
            ReloadChannels();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(MessageBox.Show("Желаете сохранить изменения?","Сохранить?",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                Button_Click(null, null);
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            SendEmoteAdd.IsEnabled = (bool)SendEmoteActive.IsChecked;
            SendEmoteDelete.IsEnabled = (bool)SendEmoteActive.IsChecked;
            SendEmoteList.IsEnabled = (bool)SendEmoteActive.IsChecked;
            information.Settings.sendEmote.Active = (bool)SendEmoteActive.IsChecked;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Emote emote = new Emote();
            emote.EmoteName = SendEmoteCommand.Text;
            emote.Find = SendEmoteText.Text;
            emote.Text = SendEmoteTextAns.Text;
            information.Settings.sendEmote.Emotes.Add(emote);
            SendEmoteList.Items.Add(emote);
        }

        private void SendEmoteDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in SendEmoteList.SelectedItems)
            {
                Emote emote = (Emote)item;
                int num = information.Settings.sendEmote.Emotes.FindIndex(x => x.EmoteName == emote.EmoteName);
                if (num != -1)
                {
                    information.Settings.sendEmote.Emotes.RemoveAt(num);
                }
            }
            SendEmoteList.Items.Clear();
            foreach (var info in information.Settings.sendEmote.Emotes)
            {
                SendEmoteList.Items.Add(info);
            }
        }

        private void antifloodActive_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked(sender, e);
        }

        private void antifloodActive1_Unchecked(object sender, RoutedEventArgs e)
        {
            antifloodActive1_Checked(sender, e);
        }

        private void SendEmoteActive_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Checked_1(sender, e);
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (information != null)
            {
                information.Settings.antiFlood.HowDo = Classes.Settings.DoIs.Kick;
            }
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            if (information != null)
            {
                information.Settings.antiFlood.HowDo = Classes.Settings.DoIs.Ban;
            }
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            if (information != null)
            {
                information.Settings.antiFlood.HowDo = Classes.Settings.DoIs.WarnAndDelete;
            }
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            if (information != null)
            {
                information.Settings.antiFlood.HowDo = Classes.Settings.DoIs.Mute;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DiscordRole role = (DiscordRole)MuteRole.SelectedItem;
                RoleInfo info = new RoleInfo();
                if (role == null)
                {
                    information.Settings.RoleMute = info;
                    DoAntiFloodMute.IsEnabled = false;
                    DoMentionFloodMute.IsEnabled = false;
                    if (DoAntiFloodMute.IsChecked == true)
                    {
                        DoAntiFloodWarn.IsChecked = true;
                        information.Settings.antiFlood.HowDo = DoIs.WarnAndDelete;
                    }
                    if (DoMentionFloodMute.IsChecked == true)
                    {
                        DoMentionFloodWarn.IsChecked = true;
                        information.Settings.antiFloodMetions.HowDo = DoIs.WarnAndDelete;
                    }
                    return;
                }
                info.RoleName = role.Name;
                info.RoleID = role.Id;
                information.Settings.RoleMute = info;
                DoAntiFloodMute.IsEnabled = true;
                DoMentionFloodMute.IsEnabled = true;
            }
            catch { }
        }

        private void DoMentionFloodKick_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.antiFloodMetions.HowDo = DoIs.Kick;
        }

        private void DoMentionFloodBan_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.antiFloodMetions.HowDo = DoIs.Ban;
        }

        private void DoMentionFloodWarn_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.antiFloodMetions.HowDo = DoIs.WarnAndDelete;
        }

        private void DoMentionFloodMute_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.antiFloodMetions.HowDo = DoIs.Mute;
        }

        private void GuardRolesActive_Checked(object sender, RoutedEventArgs e)
        {
            if (information != null)
                information.Settings.guardRole.Active = (bool)GuardRolesActive.IsChecked;
            GuardRolesAdd.IsEnabled = (bool)GuardRolesActive.IsChecked;
            GuardRolesDel.IsEnabled = (bool)GuardRolesActive.IsChecked;
            GuardRolesList.IsEnabled = (bool)GuardRolesActive.IsChecked;
            GuardRolesListAdded.IsEnabled = (bool)GuardRolesActive.IsChecked;

        }

        private void GuardRolesAdd_Click(object sender, RoutedEventArgs e)
        {
            if (GuardRolesList.SelectedItem == null)
                return;
            DiscordRole rl = (DiscordRole)GuardRolesList.SelectedItem;
            RoleInfo info = new RoleInfo();
            info.RoleName = rl.Name;
            info.RoleID = rl.Id;
            information.Settings.guardRole.GuardList.Add(info);
            ReloadRoles(false);
        }

        private void GuardRolesDel_Click(object sender, RoutedEventArgs e)
        {
            if (GuardRolesListAdded.SelectedItems.Count == 0)
                return;
            foreach (var item in GuardRolesListAdded.SelectedItems)
            {
                RoleInfo info = (RoleInfo)item;
                int num = information.Settings.guardRole.GuardList.FindIndex(x => x.RoleID == info.RoleID);
                if (num != -1)
                {
                    information.Settings.guardRole.GuardList.RemoveAt(num);
                }
            }
            ReloadRoles(false);
        }
    }
}
