using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoMihoyoBBS_1
{
    class Tools
    {
        private static string Md5(string content)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(content ?? ""));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in result)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }

        public static string CreateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);

            const string randomStringTemplate = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int pos = random.Next(0, randomStringTemplate.Length);
                builder.Append(randomStringTemplate[pos]);
            }

            return builder.ToString();
        }

        public static string Get_ds(bool web, bool web_old)
        {
            string n;
            if (web)
            {
                if (web_old)
                {
                    n = setting.mihoyobbs_Salt_web_old;
                }
                else
                {
                    n = setting.mihoyobbs_Salt_web;
                }
            }
            else
            {
                n = setting.mihoyobbs_Salt;
            }
            long time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string random = CreateRandomString(6);
            string check = Md5($"salt={n}&t={time}&r={random}");
            return $"{time},{random},{check}";
        }

        public static string Get_deviceid()
        {
            return Guid.NewGuid().ToString("D").Replace("-", "").ToUpper();
        }

        public static string Get_item(JToken raw_data)
        {
            string temp_Name = raw_data["name"].ToString();
            string temp_Cnt = raw_data["cnt"].ToString();
            return $"{temp_Name}x{temp_Cnt}";
        }
    }
}
