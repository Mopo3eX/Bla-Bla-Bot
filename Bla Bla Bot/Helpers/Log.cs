using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bla_Bla_Bot.Helpers
{
    public static class Log
    {
        public static void LogWrite(string Module,string Info)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string log = $"[{date} {time}][{Module}] {Info}";
            MainWindow.sdispatcher.BeginInvoke((Action)(() => MainWindow.staticLog.Text += log + "\r\n"));

            if (!Directory.Exists(".\\Log\\"))
                Directory.CreateDirectory(".\\Log\\");
            File.AppendAllText($".\\Log\\{date}.log", log + "\r\n");
        }
    }
}
