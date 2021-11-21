using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoMihoyoBBS_1
{
    class Config
    {

        public bool enable_Config = true;
        public int config_Version = 4;
        public string mihoyobbs_Login_ticket = "";
        public string mihoyobbs_Stuid = "";
        public string mihoyobbs_Stoken = "";
        public string mihoyobbs_Cookies = "";

        public bool bbs_Global = true;
        public bool bbs_Signin = true;
        public bool bbs_Signin_multi = true;
        public ArrayList bbs_Signin_multi_list = new ArrayList();
        public bool bbs_Read_posts = true;
        public bool bbs_Like_posts = true;
        public bool bbs_Unlike = true;
        public bool bbs_Share = true;
        public bool genshin_Auto_sign = true;
        public bool honkai3rd_Auto_sign = false;

        public JObject mihoyobbs_List;

        private string config_name { get; set; }
        
        private string path { get; set; }
        
        public Config(string config_name)
        {
            this.path = Environment.CurrentDirectory + "\\config";
            this.config_name = config_name;
            logger.println(Type.Info, "ConfigPath: " + this.path);
            this.check_File();
        }

        public void clear_Cookies()
        {
            enable_Config = false;
            mihoyobbs_Login_ticket = "";
            mihoyobbs_Stoken = "";
            mihoyobbs_Cookies = "";
        }

        private void check_File()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    logger.println(Type.Warn, "未找到Config目录,已自动创建");
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(Environment.CurrentDirectory + "\\Newtonsoft.Json.dll"))
                {
                    byte[] Save = global::AutoMihoyoBBS_1.Properties.Resources.Newtonsoft_Json;
                    FileStream file = new FileStream(Environment.CurrentDirectory + "\\Newtonsoft.Json.dll", FileMode.CreateNew);
                    BinaryWriter binaryWriter = new BinaryWriter(file);
                    binaryWriter.Write(Save, 0, Save.Length);
                    binaryWriter.Close();
                }
                if (!File.Exists(Environment.CurrentDirectory + "\\config\\data.json"))
                {
                    byte[] Save = global::AutoMihoyoBBS_1.Properties.Resources.data;
                    FileStream file = new FileStream(Environment.CurrentDirectory + "\\config\\data.json", FileMode.CreateNew);
                    BinaryWriter binaryWriter = new BinaryWriter(file);
                    binaryWriter.Write(Save, 0, Save.Length);
                    binaryWriter.Close();
                }
                if (!File.Exists(path + "\\" + this.config_name))
                {
                    logger.println(Type.Warn, "未找到Config,已自动创建");
                    this.bbs_Signin_multi_list.Add(2);
                    this.bbs_Signin_multi_list.Add(5);
                    save_Config();
                    logger.println(Type.Error, "请填写Cookies后再运行本程序！");
                    Program.exit(false);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public void load_Config()
        {
            try
            {
                if (this.config_name != null && this.path != null)
                {
                    this.mihoyobbs_List = (JObject)JToken.ReadFrom(new JsonTextReader(File.OpenText(path + "\\" + "data.json")));

                    StreamReader streamReader = File.OpenText(path + "\\" + this.config_name);
                    JsonTextReader render = new JsonTextReader(streamReader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(render);

                    this.enable_Config = (bool)jsonObject["enable_Config"];
                    this.config_Version = (int)jsonObject["config_Version"];
                    this.mihoyobbs_Login_ticket = (string)jsonObject["mihoyobbs_Login_ticket"];
                    this.mihoyobbs_Stuid = (string)jsonObject["mihoyobbs_Stuid"];
                    this.mihoyobbs_Stoken = (string)jsonObject["mihoyobbs_Stoken"];
                    this.mihoyobbs_Cookies = (string)jsonObject["mihoyobbs_Cookies"];
                    this.bbs_Global = (bool)jsonObject["mihoyobbs"]["bbs_Global"];
                    this.bbs_Signin = (bool)jsonObject["mihoyobbs"]["bbs_Signin"];
                    this.bbs_Signin_multi = (bool)jsonObject["mihoyobbs"]["bbs_Signin_multi"];
                    foreach (int a in jsonObject["mihoyobbs"]["bbs_Signin_multi_list"])
                    {
                        this.bbs_Signin_multi_list.Add(a);
                    }
                    this.bbs_Read_posts = (bool)jsonObject["mihoyobbs"]["bbs_Read_posts"];
                    this.bbs_Like_posts = (bool)jsonObject["mihoyobbs"]["bbs_Like_posts"];
                    this.bbs_Unlike = (bool)jsonObject["mihoyobbs"]["bbs_Unlike"];
                    this.bbs_Share = (bool)jsonObject["mihoyobbs"]["bbs_Share"];
                    this.genshin_Auto_sign = (bool)jsonObject["genshin_Auto_sign"];
                    this.honkai3rd_Auto_sign = (bool)jsonObject["honkai3rd_Auto_sign"];
                    logger.println(Type.Info, "Config读取完毕");
                    streamReader.Close();
                }
            }
            catch(Exception ex)
            {
                logger.println(Type.Error, $"Config读取错误：{ex}");
                Program.exit(false);
            }
        }

        public void save_Config()
        {
            try
            {
                if (this.config_name != null && this.path != null)
                {
                    JObject jo = new JObject();
                    JObject jo1 = new JObject();
                    JArray ja = new JArray();
                    jo.Add("enable_Config", this.enable_Config);
                    jo.Add("config_Version", this.config_Version);
                    jo.Add("mihoyobbs_Login_ticket", this.mihoyobbs_Login_ticket);
                    jo.Add("mihoyobbs_Stuid", this.mihoyobbs_Stuid);
                    jo.Add("mihoyobbs_Stoken", this.mihoyobbs_Stoken);
                    jo.Add("mihoyobbs_Cookies", this.mihoyobbs_Cookies);

                    jo1.Add("bbs_Global", this.bbs_Global);
                    jo1.Add("bbs_Signin", this.bbs_Signin);
                    jo1.Add("bbs_Signin_multi", this.bbs_Signin_multi);
                    foreach (int s in this.bbs_Signin_multi_list)
                    {
                        ja.Add(s);
                    }
                    jo1.Add("bbs_Signin_multi_list", ja);
                    jo1.Add("bbs_Read_posts", this.bbs_Read_posts);
                    jo1.Add("bbs_Like_posts", this.bbs_Like_posts);
                    jo1.Add("bbs_Unlike", this.bbs_Unlike);
                    jo1.Add("bbs_Share", this.bbs_Share);

                    jo.Add("mihoyobbs", jo1);

                    jo.Add("genshin_Auto_sign", this.genshin_Auto_sign);
                    jo.Add("honkai3rd_Auto_sign", this.honkai3rd_Auto_sign);

                    //Console.WriteLine(jo.ToString());
                    File.WriteAllText(path + "\\" + this.config_name, jo.ToString());
                    logger.println(Type.Info, "Config保存完毕");
                }
            }
            catch(Exception ex)
            {
                logger.println(Type.Error, $"Config保存错误：{ex}");
                Program.exit(false);
            }
        }
    }
}
