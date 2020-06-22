using Bla_Bla_Bot;
using Bla_Bla_Bot.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bla_Bla_Bot.Helpers
{
    class FindGIF
    {
        public static string GetRandomGIF(string query,int offset=0)
        {
            string json = GET("https://api.tenor.com/v1/search", $"q={HttpUtility.UrlEncode(query)}&key=H7MAPXWXYFP9&limit=50&pos={offset}");
            TenorAPI result = JsonConvert.DeserializeObject<TenorAPI>(json);
            string gifurl = null;
            while(gifurl == null)
            {
                Result rnd_result = result.Results[MainWindow.rnd.Next(0, result.Results.Length)];
                foreach(var media in rnd_result.Media)
                {
                    if(media.ContainsKey("gif"))
                    {
                        gifurl = media["gif"].Url.AbsoluteUri;
                        break;
                    }

                }
            }
            //result.Results.Length
            return gifurl;
        }
        private static string GET(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "?" + Data);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
    }
}
