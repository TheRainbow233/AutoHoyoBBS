using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace AutoMihoyoBBS_1
{
    class genshin
    {

        private JArray acc_List;
        private JObject sign_Give;
        public genshin()
        {
            acc_List = Getacc_list();
            if (acc_List.Count != 0)
            {
                sign_Give = Get_signgive();
            }
        }

        public JArray Getacc_list()
        {
            logger.println(Type.Info, "正在获取米哈游账号绑定原神账号列表...");
            JArray cache = new JArray();
            string rawdata = HttpUtils.http_Get2(setting.genshin_Account_info_url);
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            if (jobject["retcode"].ToString() != "0")
            {
                logger.println(Type.Error, "获取账号列表失败！");
                Program.exit(false);
            }
            foreach (var i in jobject["data"]["list"])
            {
                JObject job = new JObject();
                job.Add("nickname", i["nickname"].ToString());
                job.Add("game_uid", i["game_uid"].ToString());
                job.Add("region", i["region"].ToString());
                cache.Add(job);
            }
            logger.println(Type.Info, $"已获取到{cache.Count}个原神账号信息");
            return cache;
        }

        public JObject Get_signgive()
        {
            logger.println(Type.Info, "正在获取签到奖励列表...");
            string rawdata = HttpUtils.http_Get2(String.Format(setting.genshin_Signlisturl, setting.genshin_Act_id));
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            if (jobject["retcode"].ToString() != "0")
            {
                logger.println(Type.Error, "获取账号列表失败！");
                Console.WriteLine(rawdata);
                Program.exit(false);
            }
            return jobject;
        }

        public JToken Is_sign(string region, string uid)
        {
            string rawdata = HttpUtils.http_Get2(String.Format(setting.genshin_Is_signurl, setting.genshin_Act_id, region, uid));
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            if (jobject["retcode"].ToString() != "0")
            {
                logger.println(Type.Error, "获取账号签到信息失败！");
                Console.WriteLine(rawdata);
                Program.exit(false);
            }
            return jobject["data"];
        }

        public void Sign_acc()
        {
            if (acc_List.Count != 0)
            {
                foreach (var i in acc_List)
                {
                    logger.println(Type.Info, $"正在为旅行者{i["nickname"]}进行签到...");
                    Thread.Sleep(new Random().Next(2, 8) * 1000);
                    var is_data = Is_sign(i["region"].ToString(), i["game_uid"].ToString());
                    if (is_data.Value<bool>("first_bind"))
                    {
                        logger.println(Type.Warn, $"旅行者{i["nickname"]}是第一次绑定米游社，请先手动签到一次");
                    }
                    else
                    {
                        int sign_Days = is_data.Value<int>("total_sign_day") - 1;
                        if (is_data.Value<bool>("is_sign"))
                        {
                            logger.println(Type.Info, $"旅行者{i["nickname"]}今天已经签到过了~\r\n今天获得的奖励是{Tools.Get_item(sign_Give["data"]["awards"][sign_Days])}");
                        }
                        else
                        {
                            Thread.Sleep(new Random().Next(2, 8) * 1000);
                            string rawdata = HttpUtils.http_Post2(setting.genshin_Signurl, "{\"act_id\": \"" + setting.genshin_Act_id + "\", \"region\": \"" + i["region"].ToString() + "\", \"uid\": \"" + i["game_uid"].ToString() + "\"}");
                            //Console.WriteLine(rawdata);
                            JObject jobject = JObject.Parse(rawdata);
                            if (jobject.Value<int>("retcode") == 0)
                            {
                                if (sign_Days == 0)
                                {
                                    logger.println(Type.Info, $"旅行者{i["nickname"]}签到成功~\r\n今天获得的奖励是{Tools.Get_item(sign_Give["data"]["awards"][sign_Days])}");
                                }
                                else
                                {
                                    logger.println(Type.Info, $"旅行者{i["nickname"]}签到成功~\r\n今天获得的奖励是{Tools.Get_item(sign_Give["data"]["awards"][sign_Days + 1])}");
                                }
                            }
                            else if (jobject.Value<int>("retcode") == -5003)
                            {
                                logger.println(Type.Info, $"旅行者{i["nickname"]}今天已经签到过了~\r\n今天获得的奖励是{Tools.Get_item(sign_Give["data"]["awards"][sign_Days])}");
                            }
                            else
                            {
                                logger.println(Type.Error, "账号签到失败！");
                                Console.WriteLine(rawdata);
                            }
                        }
                    }
                }
            }
            else
            {
                logger.println(Type.Error, "账号没有绑定任何原神账号！");
            }
        }
    }
}
