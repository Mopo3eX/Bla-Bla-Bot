using Bla_Bla_Bot.Classes;
using Bla_Bla_Bot.Helpers;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Bla_Bla_Bot.Components
{
    public static class GuardRole
    {
        public static Task Client_GuildRoleDeleted(DSharpPlus.EventArgs.GuildRoleDeleteEventArgs e)
        {
            MainWindow.EventsCount++;
            if(e.Guild==null)
                return Task.CompletedTask;
            if (!MainWindow.GuildsDictionary.ContainsKey(e.Guild.Id))
                return Task.CompletedTask;
            GuildInformation info = MainWindow.GuildsDictionary[e.Guild.Id];
            if(!info.Settings.BotActive)
                return Task.CompletedTask;
            if(!info.Settings.guardRole.Active)
                return Task.CompletedTask;
            if(!info.Settings.guardRole.GuardList.Exists(x => x.RoleID == e.Role.Id))
                return Task.CompletedTask;
            Log.LogWrite("Guard Role", $"ВНИМАНИЕ! Удалена защищаемая роль {e.Role} на сервере {e.Guild}!\r\n" +
                $"Настройки роли:\r\n" +
                $"Позиция: {e.Role.Position}\r\n" +
                $"Права: {e.Role.Permissions}\r\n" +
                $"Название: {e.Role.Name}\r\n" +
                $"Цвет: {e.Role.Color}");
            return Task.CompletedTask;
        }
        public static List<DiscordRole> ignore = new List<DiscordRole>();
        public static List<DiscordRole> ignoreposition = new List<DiscordRole>();
        public static Task Client_GuildRoleUpdated(DSharpPlus.EventArgs.GuildRoleUpdateEventArgs e)
        {
            MainWindow.EventsCount++;
            if (e.Guild == null)
                return Task.CompletedTask;
            if (!MainWindow.GuildsDictionary.ContainsKey(e.Guild.Id))
                return Task.CompletedTask;
            GuildInformation info = MainWindow.GuildsDictionary[e.Guild.Id];
            if (!info.Settings.BotActive)
                return Task.CompletedTask;
            if (!info.Settings.guardRole.Active)
                return Task.CompletedTask;
            if (!info.Settings.guardRole.GuardList.Exists(x => x.RoleID == e.RoleBefore.Id))
                return Task.CompletedTask;
            Thread.Sleep(3000);
            if (e.RoleBefore.Permissions != e.RoleAfter.Permissions || e.RoleBefore.Name != e.RoleAfter.Name || e.RoleBefore.Color.Value != e.RoleAfter.Color.Value)
            {
                if (ignore.Contains(e.RoleBefore))
                {
                    ignore.Remove(e.RoleBefore);
                    return Task.CompletedTask;
                }
                ignore.Add(e.RoleAfter);
                Log.LogWrite("Guard Role", $"ВНИМАНИЕ! Изменена защищаемая роль {e.RoleBefore} на сервере {e.Guild}!\r\n" +
                    $"Новые настройки роли:\r\n" +
                    $"Позиция: {e.RoleAfter.Position}\r\n" +
                    $"Права: {e.RoleAfter.Permissions}\r\n" +
                    $"Название: {e.RoleAfter.Name}\r\n" +
                    $"Цвет: {e.RoleAfter.Color}\r\n" +
                    $"Старые настройки роли:\r\n" +
                    $"Позиция: {e.RoleBefore.Position}\r\n" +
                    $"Права: {e.RoleBefore.Permissions}\r\n" +
                    $"Название: {e.RoleBefore.Name}\r\n" +
                    $"Цвет: {e.RoleBefore.Color}\r\n" +
                    $"Восстанавливаем.");
                e.Guild.UpdateRoleAsync(e.RoleAfter, e.RoleBefore.Name, e.RoleBefore.Permissions, e.RoleBefore.Color, e.RoleBefore.IsHoisted, reason: "Role Guard");
            }
            Thread.Sleep(3000);
            if (ignoreposition.Contains(e.RoleBefore) && e.RoleBefore.Position != e.RoleAfter.Position)
            {
                ignoreposition.Remove(e.RoleBefore);
                return Task.CompletedTask;
            }
            if (e.RoleBefore.Position != e.RoleAfter.Position)
            {
                ignoreposition.Add(e.RoleAfter);
                e.Guild.UpdateRolePositionAsync(e.RoleAfter, e.RoleBefore.Position, "Role Guard");
            }
            return Task.CompletedTask;
        }

    }

}
