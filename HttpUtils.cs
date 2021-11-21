using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoMihoyoBBS_1
{
    class HttpUtils
    {
        public static string http_Get(string url)
        {
            //初始化缓存
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            string content = string.Empty;

            try
            {
                //Header
                req = WebRequest.CreateHttp(url);
                req.Method = "GET";
                req.Accept = "application/json,text/plain;*/*;";
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36 Edg/88.0.705.63";
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");

            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            try
            {
                res = req.GetResponse() as HttpWebResponse;

                if (!(res.StatusCode.ToString() == "OK"))
                {
                    Console.WriteLine("数据获取错误,错误码:" + res.StatusCode.ToString());
                    return "Error";
                }

                StreamReader streamReader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                content = streamReader.ReadToEnd();
                //Console.WriteLine(content);

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            return null;

        }

        public static string http_Get1(string url)
        {
            //初始化缓存
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            string content = string.Empty;

            try
            {
                //Header
                req = WebRequest.CreateHttp(url);
                req.Method = "GET";

                req.UserAgent = "okhttp/4.8.0";
                req.Headers.Add("DS", Tools.Get_ds(false, false));
                req.Headers.Add("cookie", $"stuid={Program.config.mihoyobbs_Stuid};stoken={Program.config.mihoyobbs_Stoken}");
                req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type);
                req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version);
                req.Headers.Add("x-rpc-sys_version", "6.0.1");
                req.Headers.Add("x-rpc-channel", "mihoyo");
                req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());
                req.Headers.Add("x-rpc-device_name", Tools.CreateRandomString(new Random().Next(1, 10)));
                req.Headers.Add("x-rpc-device_model", "Mi 10");
                req.Headers.Add("Referer", "https://app.mihoyo.com");
                req.Headers.Add("Host", "bbs-api.mihoyo.com");
                //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                //req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");

            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            try
            {
                res = req.GetResponse() as HttpWebResponse;

                if (!(res.StatusCode.ToString() == "OK"))
                {
                    Console.WriteLine("数据获取错误,错误码:" + res.StatusCode.ToString());
                    return "Error";
                }

                StreamReader streamReader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                content = streamReader.ReadToEnd();
                //Console.WriteLine(content);

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            return null;

        }

        public static string http_Get2(string url)
        {
            //初始化缓存
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            string content = string.Empty;

            try
            {
                //Header
                req = WebRequest.CreateHttp(url);
                req.Method = "GET";

                req.Accept = "application/json,text/plain;*/*;";
                req.Headers.Add("DS", Tools.Get_ds(true, true));
                req.Headers.Add("Origin", "https://webstatic.mihoyo.com");
                req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version_old);
                req.UserAgent = "Mozilla/5.0 (Linux; Android 9; Unspecified Device) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 miHoYoBBS/2.3.0";
                req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type_web);
                req.Headers.Add("Referer", "https://webstatic.mihoyo.com/bbs/event/signin-ys/index.html?bbs_auth_required=true&act_id=e202009291139501&utm_source=bbs&utm_medium=mys&utm_campaign=icon");
                //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");
                req.Headers.Add("X-Requested-With", "com.mihoyo.hyperion");
                req.Headers.Add("cookie", Program.config.mihoyobbs_Cookies);
                req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());

            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            try
            {
                res = req.GetResponse() as HttpWebResponse;

                if (!(res.StatusCode.ToString() == "OK"))
                {
                    Console.WriteLine("数据获取错误,错误码:" + res.StatusCode.ToString());
                    return "Error";
                }

                StreamReader streamReader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                content = streamReader.ReadToEnd();
                //Console.WriteLine(content);

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            return null;

        }

        public static string http_Get3(string url)
        {
            //初始化缓存
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            string content = string.Empty;

            try
            {
                //Header
                req = WebRequest.CreateHttp(url);
                req.Method = "GET";

                req.Accept = "application/json,text/plain;*/*;";
                req.Headers.Add("DS", Tools.Get_ds(true, true));
                req.Headers.Add("Origin", "https://webstatic.mihoyo.com");
                req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version_old);
                req.UserAgent = "Mozilla/5.0 (Linux; Android 9; Unspecified Device) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 miHoYoBBS/2.3.0";
                req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type_web);
                req.Headers.Add("Referer", $"https://webstatic.mihoyo.com/bh3/event/euthenia/index.html?bbs_presentation_style=fullscreen&bbs_game_role_required=bh3_cn&bbs_auth_required=true&act_id={setting.honkai3rd_Act_id}&utm_source=bbs&utm_medium=mys&utm_campaign=icon");
                //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");
                req.Headers.Add("X-Requested-With", "com.mihoyo.hyperion");
                req.Headers.Add("cookie", Program.config.mihoyobbs_Cookies);
                req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());

            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            try
            {
                res = req.GetResponse() as HttpWebResponse;

                if (!(res.StatusCode.ToString() == "OK"))
                {
                    Console.WriteLine("数据获取错误,错误码:" + res.StatusCode.ToString());
                    return "Error";
                }

                StreamReader streamReader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                content = streamReader.ReadToEnd();
                //Console.WriteLine(content);

                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Handler: {0}", e.ToString());
            }

            return null;

        }

        public static string http_Post(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            if (content != "")
            {
                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string http_Post1(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.UserAgent = "okhttp/4.8.0";
            req.Headers.Add("DS", Tools.Get_ds(false, false));
            req.Headers.Add("cookie", $"stuid={Program.config.mihoyobbs_Stuid};stoken={Program.config.mihoyobbs_Stoken}");
            req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type);
            req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version);
            req.Headers.Add("x-rpc-sys_version", "6.0.1");
            req.Headers.Add("x-rpc-channel", "mihoyo");
            req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());
            req.Headers.Add("x-rpc-device_name", Tools.CreateRandomString(new Random().Next(1, 10)));
            req.Headers.Add("x-rpc-device_model", "Mi 10");
            req.Headers.Add("Referer", "https://app.mihoyo.com");
            req.Headers.Add("Host", "bbs-api.mihoyo.com");

            if (content != "")
            {
                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string http_Post2(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Accept = "application/json,text/plain;*/*;";
            req.Headers.Add("DS", Tools.Get_ds(true, true));
            req.Headers.Add("Origin", "https://webstatic.mihoyo.com");
            req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version_old);
            req.UserAgent = "Mozilla/5.0 (Linux; Android 9; Unspecified Device) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 miHoYoBBS/2.3.0";
            req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type_web);
            req.Headers.Add("Referer", "https://webstatic.mihoyo.com/bbs/event/signin-ys/index.html?bbs_auth_required=true&act_id=e202009291139501&utm_source=bbs&utm_medium=mys&utm_campaign=icon");
            //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");
            req.Headers.Add("X-Requested-With", "com.mihoyo.hyperion");
            req.Headers.Add("cookie", Program.config.mihoyobbs_Cookies);
            req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());

            if (content != "")
            {
                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string http_Post3(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Accept = "application/json,text/plain;*/*;";
            req.Headers.Add("DS", Tools.Get_ds(true, true));
            req.Headers.Add("Origin", "https://webstatic.mihoyo.com");
            req.Headers.Add("x-rpc-app_version", setting.mihoyobbs_Version_old);
            req.UserAgent = "Mozilla/5.0 (Linux; Android 9; Unspecified Device) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 miHoYoBBS/2.3.0";
            req.Headers.Add("x-rpc-client_type", setting.mihoyobbs_Client_type_web);
            req.Headers.Add("Referer", $"https://webstatic.mihoyo.com/bh3/event/euthenia/index.html?bbs_presentation_style=fullscreen&bbs_game_role_required=bh3_cn&bbs_auth_required=true&act_id={setting.honkai3rd_Act_id}&utm_source=bbs&utm_medium=mys&utm_campaign=icon");
            //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,en-US;q=0.8");
            req.Headers.Add("X-Requested-With", "com.mihoyo.hyperion");
            req.Headers.Add("cookie", Program.config.mihoyobbs_Cookies);
            req.Headers.Add("x-rpc-device_id", Tools.Get_deviceid());

            if (content != "")
            {
                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
