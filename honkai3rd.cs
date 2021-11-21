using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoMihoyoBBS_1
{
    class honkai3rd
    {

        private JArray acc_List;

        public honkai3rd()
        {
            acc_List = Getacc_list();
        }

        public JArray Getacc_list()
        {
            logger.println(Type.Info, "正在获取米哈游账号绑定的崩坏3账号列表...");
            JArray cache = new JArray();
            string rawdata = HttpUtils.http_Get3(setting.honkai3rd_Account_info_url);
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            if (!jobject["retcode"].ToString().Equals("0"))
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
            logger.println(Type.Info, $"已获取到{cache.Count}个崩坏3账号信息");
            return cache;
        }

        public JObject Get_today_item(JArray j)
        {
            for (int i = 1; i<=j.Count; i++)
            {
                if (j[i]["status"].ToString() == "0")
                {
                    //Console.WriteLine(j[i - 1].ToString());
                    return JObject.Parse(j[i - 1].ToString());
                }
                if (j[i]["status"].ToString() == "1")
                {
                    //Console.WriteLine(j[i].ToString());
                    return JObject.Parse(j[i].ToString());
                }
                if (i == j.Count - 1 && !j[i]["status"].ToString().Equals("0"))
                {
                    //Console.WriteLine(j[i].ToString());
                    return JObject.Parse(j[i].ToString());
                }
            }
            return null;
        }

        public bool Is_sign(string region, string uid, string nickname)
        {
            string rawdata = HttpUtils.http_Get3(String.Format(setting.honkai3rd_Is_signurl, setting.honkai3rd_Act_id, region, uid));
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            if (jobject["retcode"].ToString() != "0")
            {
                logger.println(Type.Info, "获取账号签到信息失败！");
                Console.WriteLine(rawdata);
                Program.exit(false);
            }
            //Console.WriteLine(jobject["data"]["sign"]["list"].ToString());
            JObject today_Item = Get_today_item(JArray.Parse(jobject["data"]["sign"]["list"].ToString()));
            if (today_Item["status"].ToString().Equals("1"))
            {
                return true;
            }
            else
            {
                logger.println(Type.Info, $"舰长{nickname}今天已经签到过了~\r\n今天获得的奖励是{Tools.Get_item(today_Item)}");
                return false;
            }
        }

        public void Sign_acc()
        {
            if (acc_List.Count != 0)
            {
                foreach (var i in acc_List)
                {
                    logger.println(Type.Info, $"正在为舰长{i["nickname"]}进行签到...");
                    Thread.Sleep(new Random().Next(2, 8) * 1000);
                    var is_data = Is_sign(i["region"].ToString(), i["game_uid"].ToString(), i["nickname"].ToString());
                    if (is_data)
                    {
                        Thread.Sleep(new Random().Next(2, 8) * 1000);
                        string rawdata = HttpUtils.http_Post3(setting.honkai3rd_SignUrl, "{\"act_id\": \"" + setting.honkai3rd_Act_id + "\", \"region\": \"" + i["region"].ToString() + "\", \"uid\": \"" + i["game_uid"].ToString() + "\"}");
                        //Console.WriteLine(rawdata);
                        JObject jobject = JObject.Parse(rawdata);
                        if (jobject.Value<int>("retcode") == 0)
                        {
                            //Console.WriteLine(jobject["data"]["list"]);
                            JToken today_Item = Get_today_item(JArray.Parse(jobject["data"]["list"].ToString()));
                            logger.println(Type.Info, $"舰长{i["nickname"]}签到成功~\r\n今天获得的奖励是{Tools.Get_item(today_Item)}");
                        }
                        else if (jobject.Value<int>("retcode") == -5003)
                        {
                            logger.println(Type.Info, $"舰长{i["nickname"]}今天已经签到过了~");
                        }
                        else
                        {
                            logger.println(Type.Error, "账号签到失败！");
                            Console.WriteLine(rawdata);
                        }
                    }
                }
            }
            else
            {
                logger.println(Type.Warn, "账号没有绑定任何崩坏3账号！");
            }
        }
    }
}
