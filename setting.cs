using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMihoyoBBS_1
{
    class setting
    {
        // 米游社的Salt
        public static string mihoyobbs_Salt = "fd3ykrh7o1j54g581upo1tvpam0dsgtf";
        public static string mihoyobbs_Salt_web = "14bmu1mz0yuljprsfgpvjh3ju2ni468r";
        public static string mihoyobbs_Salt_web_old = "h8w582wxwgqvahcdkpvdhbh2w9casgfl";
        //米游社的版本
        public static string mihoyobbs_Version = "2.7.0"; //Slat和Version相互对应
        public static string mihoyobbs_Version_old = "2.3.0";
        //米游社的客户端类型
        public static string mihoyobbs_Client_type = "2"; //1为ios 2为安卓
        public static string mihoyobbs_Client_type_web = "5"; //4为pc web 5为mobile web
                                                              //米游社的分区列表

        //Config Load之后run里面进行列表的选择
        public static ArrayList mihoyobbs_List_Use = new ArrayList();

        //米游社的API列表
        public static string bbs_Cookieurl = "https://webapi.account.mihoyo.com/Api/cookie_accountinfo_by_loginticket?login_ticket={0}";
        public static string bbs_Cookieurl2 = "https://api-takumi.mihoyo.com/auth/api/getMultiTokenByLoginTicket?login_ticket={0}&token_types=3&uid={1}";
        public static string bbs_Taskslist = "https://bbs-api.mihoyo.com/apihub/sapi/getUserMissionsState"; //获取任务列表
        public static string bbs_Signurl = "https://bbs-api.mihoyo.com/apihub/sapi/signIn?gids={0}";  // post
        public static string bbs_Listurl = "https://bbs-api.mihoyo.com/post/api/getForumPostList?forum_id={0}&is_good=false&is_hot=false&page_size=20&sort_type=1";
        public static string bbs_Detailurl = "https://bbs-api.mihoyo.com/post/api/getPostFull?post_id={0}";
        public static string bbs_Shareurl = "https://bbs-api.mihoyo.com/apihub/api/getShareConf?entity_id={0}&entity_type=1";
        public static string bbs_Likeurl = "https://bbs-api.mihoyo.com/apihub/sapi/upvotePost";  // post json 

        //原神自动签到相关的设置
        public static string genshin_Act_id = "e202009291139501";
        public static string genshin_Account_info_url = "https://api-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie?game_biz=hk4e_cn";
        public static string genshin_Signlisturl = "https://api-takumi.mihoyo.com/event/bbs_sign_reward/home?act_id={0}";
        public static string genshin_Is_signurl = "https://api-takumi.mihoyo.com/event/bbs_sign_reward/info?act_id={0}&region={1}&uid={2}";
        public static string genshin_Signurl = "https://api-takumi.mihoyo.com/event/bbs_sign_reward/sign";

        //崩坏3自动签到相关的设置
        public static string honkai3rd_Act_id = "ea20211026151532";
        public static string honkai3rd_Account_info_url = "https://api-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie?game_biz=bh3_cn";
        public static string honkai3rd_Is_signurl = "https://api-takumi.mihoyo.com/common/eutheniav2/index?act_id={0}&region={1}&uid={2}";
        public static string honkai3rd_SignUrl = "https://api-takumi.mihoyo.com/common/eutheniav2/sign";
    }
}
