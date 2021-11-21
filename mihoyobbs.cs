using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Newtonsoft.Json.Linq;

namespace AutoMihoyoBBS_1
{
    class mihoyobbs
    {
        public bool bbs_Sign = false;
        public bool bbs_Read_posts = false;
        public int bbs_Read_posts_num = 3;
        public bool bbs_Like_posts = false;
        public int bbs_Like_posts_num = 5;
        public bool bbs_Share = false;
        public int Today_getcoins = 0;
        public int Today_have_getcoins = 0; //这个变量以后可能会用上，先留着了
        public int Have_coins = 0;

        private Dictionary<string, string> postsList = new Dictionary<string, string>();

        public mihoyobbs()
        {
            this.Get_taskslist();
            if (bbs_Read_posts && bbs_Like_posts && bbs_Share)
            {
                return;
            }
            else
            {
                this.Getlist();
            }
        }

        //获取任务列表，用来判断做了哪些任务
        private void Get_taskslist()
        {
            logger.println(Type.Info, "正在获取任务列表");
            string rawdata = HttpUtils.http_Get1(setting.bbs_Taskslist);
            //Console.WriteLine(rawdata);
            JObject Jobject = JObject.Parse(rawdata);
            //Console.WriteLine(Jobject["message"]);
            if (Jobject["message"].ToString().Contains("err"))
            {
                logger.println(Type.Info, "获取任务列表失败，你的cookie可能已过期，请重新设置cookie。");
                Program.exit(true);
            }
            else
            {
                Today_getcoins = Jobject["data"]["can_get_points"].Value<int>();
                Today_have_getcoins = Jobject["data"]["already_received_points"].Value<int>();
                Have_coins = Jobject["data"]["total_points"].Value<int>();
                if (Today_getcoins == 0)
                {
                    bbs_Sign = true;
                    bbs_Read_posts = true;
                    bbs_Like_posts = true;
                    bbs_Share = true;
                }
                else
                {
                    if (Jobject["data"]["states"][0]["mission_id"].Value<int>() >= 62)
                    {
                        logger.println(Type.Info, $"新的一天，今天可以获得{Today_getcoins}个米游币");
                        return;
                    }
                    else
                    {
                        logger.println(Type.Info, $"似乎还有任务没完成，今天还能获得{Today_getcoins}");
                        foreach (var i in Jobject["data"]["states"])
                        {
                            //58是讨论区签到
                            if (i.Value<int>("mission_id") == 58)
                            {
                                if (i.Value<bool>("is_get_award"))
                                {
                                    bbs_Sign = true;
                                }
                            }
                            //59是看帖子
                            else if (i.Value<int>("mission_id") == 59)
                            {
                                if (i.Value<bool>("is_get_award"))
                                {
                                    bbs_Read_posts = true;
                                }
                                else
                                {
                                    bbs_Read_posts_num -= i.Value<int>("happened_times");
                                }
                                
                            }
                            //60是给帖子点赞
                            else if (i.Value<int>("mission_id") == 60)
                            {
                                if (i.Value<bool>("is_get_award"))
                                {
                                    bbs_Like_posts = true;
                                }
                                else
                                {
                                    bbs_Like_posts_num -= i.Value<int>("happened_times");
                                }

                            }
                            //61是分享帖子
                            else if (i.Value<int>("mission_id") == 61)
                            {
                                if (i.Value<bool>("is_get_award"))
                                {
                                    bbs_Share = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Getlist()
        {
            logger.println(Type.Info, "正在获取帖子列表......");
            string rawdata = HttpUtils.http_Get1(String.Format(setting.bbs_Listurl, Program.config.mihoyobbs_List[setting.mihoyobbs_List_Use[0].ToString()]["forumId"].ToString()));
            //Console.WriteLine(rawdata);
            JObject jobject = JObject.Parse(rawdata);
            for (int i = 1; i <= 5; i++)
            {
                this.postsList.Add(jobject["data"]["list"][i]["post"]["post_id"].ToString(), jobject["data"]["list"][i]["post"]["subject"].ToString());
            }
            logger.println(Type.Info, $"已获取{this.postsList.Count}个帖子");
        }

        public void Signin()
        {
            if (bbs_Sign)
            {
                logger.println(Type.Info, "讨论区任务已经完成过了~");
            }
            else
            {
                logger.println(Type.Info, "正在签到......");
                foreach (string id in setting.mihoyobbs_List_Use)
                {
                    string rawdata = HttpUtils.http_Post1(String.Format(setting.bbs_Signurl, int.Parse(id)), "");
                    JObject jobject = JObject.Parse(rawdata);
                    if (!jobject["message"].ToString().Equals("err"))
                    {
                        Console.WriteLine(Program.config.mihoyobbs_List[id]["name"].ToString() + " " + jobject["message"]);
                        Thread.Sleep(new Random().Next(2, 8) * 1000);
                    }
                    else
                    {
                        logger.println(Type.Error, "签到失败，你的cookie可能已过期，请重新设置cookie。");
                        Program.exit(true);
                    }
                }
            }
        }

        public void Readposts()
        {
            if (bbs_Read_posts)
            {
                logger.println(Type.Info, "看帖任务已经完成过了~");
            }
            else
            {
                logger.println(Type.Info, "正在看帖......");
                for (int i = 1; i <= bbs_Read_posts_num; i++)
                {
                    string rawdata = HttpUtils.http_Get1(String.Format(setting.bbs_Detailurl, postsList.ElementAt(i).Key));
                    //Console.WriteLine(rawdata);
                    JObject jobject = JObject.Parse(rawdata);
                    if (jobject["message"].ToString().Equals("OK"))
                    {
                        logger.println(Type.Info, $"看帖：{postsList.ElementAt(i).Value} 成功");
                    }
                    Thread.Sleep(new Random().Next(2, 8) * 1000);
                }
            }
        }

        public void Likeposts()
        {
            if (bbs_Like_posts)
            {
                logger.println(Type.Info, "点赞任务已经完成过了~");
            }
            else
            {
                logger.println(Type.Info, "正在点赞......");
                for (int i = 0; i <= (bbs_Like_posts_num - 1); i++)
                {
                    string rawdata = HttpUtils.http_Post1(setting.bbs_Likeurl, "{\"post_id\": " + postsList.ElementAt(i).Key + ", \"is_cancel\": false}");
                    //Console.WriteLine(rawdata);
                    JObject jobject = JObject.Parse(rawdata);
                    if (jobject["message"].ToString().Equals("OK"))
                    {
                        logger.println(Type.Info, $"点赞：{postsList.ElementAt(i).Value} 成功");
                    }
                    if (Program.config.bbs_Unlike)
                    {
                        Thread.Sleep(new Random().Next(2, 8) * 1000);
                        string rawdata1 = HttpUtils.http_Post1(setting.bbs_Likeurl, "{\"post_id\": " + postsList.ElementAt(i).Key + ", \"is_cancel\": true}");
                        JObject jobject1 = JObject.Parse(rawdata1);
                        if (jobject["message"].ToString().Equals("OK"))
                        {
                            logger.println(Type.Info, $"取消点赞：{postsList.ElementAt(i).Value} 成功");
                        }
                        Thread.Sleep(new Random().Next(2, 8) * 1000);
                    }
                }
            }
        }

        public void Share()
        {
            if (bbs_Share)
            {
                logger.println(Type.Info, "分享任务已经完成过了~");
            }
            else
            {
                logger.println(Type.Info, "正在分享......");
                string rawdata = HttpUtils.http_Get1(String.Format(setting.bbs_Shareurl, postsList.ElementAt(0).Key));
                //Console.WriteLine(rawdata);
                JObject jobject = JObject.Parse(rawdata);
                if (jobject["message"].ToString().Equals("OK"))
                {
                    logger.println(Type.Info, $"分享：{postsList.ElementAt(0).Value} 成功");
                }
                Thread.Sleep(new Random().Next(2, 8) * 1000);
            }
        }
    }
}
