using System;
using System.Collections;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoMihoyoBBS_1
{
    class Program
    {

        public static Config config = new Config("config.json");
        private static Random random = new Random();

        static void Main(string[] args)
        {
            //加载配置文件
            config.load_Config();
            //Console.WriteLine();
            if (config.enable_Config)
            {
                if (config.mihoyobbs_Login_ticket == "" || config.mihoyobbs_Stuid == "" || config.mihoyobbs_Stoken == "")
                {
                    if (config.mihoyobbs_Cookies == "")
                    {
                        logger.println(Type.Info, "请填写Cookies后再运行本程序！");
                        exit(true);
                    }
                    logger.println(Type.Info, "参数不全,正在尝试登录...");
                    login();
                    Thread.Sleep(random.Next(2, 8) * 1000);
                }
                //板块签到排序
                if (config.bbs_Signin_multi)
                {
                    foreach (int i in config.bbs_Signin_multi_list)
                    {
                        setting.mihoyobbs_List_Use.Add(config.mihoyobbs_List[i.ToString()]["id"].ToString());
                    }
                }
                else
                {
                    setting.mihoyobbs_List_Use.Add(config.mihoyobbs_List["5"]["id"].ToString());
                }
                //米游社签到
                if (config.bbs_Global)
                {
                    mihoyobbs bbs = new mihoyobbs();
                    if (bbs.bbs_Sign && bbs.bbs_Read_posts && bbs.bbs_Like_posts && bbs.bbs_Share)
                    {
                        logger.println(Type.Info, $"今天已经全部完成了！一共获得{bbs.Today_have_getcoins}个米游币，目前有{bbs.Have_coins}个米游币");
                    }
                    else
                    {
                        if (config.bbs_Signin)
                        {
                            bbs.Signin();
                        }
                        if (config.bbs_Read_posts)
                        {
                            bbs.Readposts();
                        }
                        if (config.bbs_Read_posts)
                        {
                            bbs.Likeposts();
                        }
                        if (config.bbs_Share)
                        {
                            bbs.Share();
                        }
                    }
                }
                else
                {
                    logger.println(Type.Info, "米游社功能未启用！");
                }
                if (config.genshin_Auto_sign)
                {
                    logger.println(Type.Info, "正在进行原神签到");
                    genshin genshin_Help = new genshin();
                    genshin_Help.Sign_acc();
                    Thread.Sleep(random.Next(2, 8) * 1000);
                }
                else
                {
                    logger.println(Type.Info, "原神签到功能未启用！");
                }

                if (config.honkai3rd_Auto_sign)
                {
                    logger.println(Type.Info, "正在进行崩坏3签到");
                    honkai3rd honkai3rd_Help = new honkai3rd();
                    honkai3rd_Help.Sign_acc();
                }
                else
                {
                    logger.println(Type.Info, "崩坏3签到功能未启用！");
                }
            }
            else
            {
                logger.println(Type.Info, "Config未启用！");
            }
            logger.println(Type.Info, "按任意键退出程序");
            Console.Read();
        }

        public static void exit(bool clear)
        {
            if (clear)
            {
                config.clear_Cookies();
                config.save_Config();
            }
            logger.println(Type.Info, "按任意键退出程序");
            Console.Read();
            Environment.Exit(1);
        }

        private static void login()
        {
            if (config.mihoyobbs_Cookies == "")
            {
                logger.println(Type.Error, "请填入Cookies!");
                exit(true);
            }
            else if (!config.mihoyobbs_Cookies.Contains("login_ticket"))
            {
                logger.println(Type.Error, "cookie中没有'login_ticket'字段,请重新登录米游社，重新抓取cookie!");
                exit(true);
            }
            string[] split = config.mihoyobbs_Cookies.Split("; ");
            foreach (string s in split)
            {
                //Console.WriteLine(s);
                if (s.Contains("login_ticket"))
                {
                    config.mihoyobbs_Login_ticket = s.Replace("login_ticket=", "");
                    string rawJson = HttpUtils.http_Get(String.Format(setting.bbs_Cookieurl, config.mihoyobbs_Login_ticket));
                    JObject jobject = JObject.Parse(rawJson);
                    //Console.WriteLine(rawJson);
                    if (jobject["data"]["msg"].ToString().Equals("成功"))
                    {
                        config.mihoyobbs_Stuid = jobject["data"]["cookie_info"]["account_id"].ToString();
                        string rawJson1 = HttpUtils.http_Get(String.Format(setting.bbs_Cookieurl2, config.mihoyobbs_Login_ticket, config.mihoyobbs_Stuid));
                        //Console.WriteLine(rawJson1);
                        JObject jobject1 = JObject.Parse(rawJson1);
                        config.mihoyobbs_Stoken = jobject1["data"]["list"][0]["token"].ToString();
                        logger.println(Type.Info, "登录成功！");
                        logger.println(Type.Info, "正在保存Config！");
                        config.save_Config();
                    }
                    else
                    {
                        logger.println(Type.Error, $"cookie已失效,请重新登录米游社抓取cookie,错误码{jobject["code"].ToString()}");
                        exit(true);
                    }
                }
            }
        }
    }
}
