using System;
using System.Collections.Generic;
using System.Text;

namespace Afdian.Sdk.ResponseModels
{
    public class QuerySponsorResponseModel
    {
        /// <summary>
        /// ec 为 200 时，表示请求正常，否则 异常，同时 em 会提示错误信息
        /// 
        /// 400001  params incomplete
        /// 400002  time was expired
        /// 400003  params was not valid json string
        /// 400004  no valid token found
        /// 400005  sign validation failed
        /// 响应 400005 时，会 data.debug 处返回服务端对参数做拼接的结构
        /// </summary>
        public int ec { get; set; }

        public string em { get; set; }

        public DataModel data { get; set; }

        public class DataModel
        {
            public ListItemModel[] list { get; set; }

            public int total_count { get; set; }

            public int total_page { get; set; }

            public class ListItemModel
            {
                public List<SponsorPlanModel> sponsor_plans { get; set; }

                public CurrentPlanModel current_plan { get; set; }

                public string all_sum_amount { get; set; }

                public long create_time { get; set; }

                public long last_pay_time { get; set; }

                public UserModel user { get; set; }

                public class SponsorPlanModel
                {
                    public string plan_id { get; set; }
                    public int rank { get; set; }
                    public string user_id { get; set; }
                    public int status { get; set; }
                    public string name { get; set; }
                    public string pic { get; set; }
                    public string desc { get; set; }
                    public string price { get; set; }
                    public int update_time { get; set; }
                    public int pay_month { get; set; }
                    public string show_price { get; set; }
                    public int independent { get; set; }
                    public int permanent { get; set; }
                    public int can_buy_hide { get; set; }
                    public int need_address { get; set; }
                    public int product_type { get; set; }
                    public int sale_limit_count { get; set; }
                    public bool need_invite_code { get; set; }
                    public int expire_time { get; set; }
                    public object[] sku_processed { get; set; }
                    public int rankType { get; set; }
                }

                public class CurrentPlanModel
                {
                    public string plan_id { get; set; }
                    public int rank { get; set; }
                    public string user_id { get; set; }
                    public int status { get; set; }
                    public string name { get; set; }
                    public string pic { get; set; }
                    public string desc { get; set; }
                    public string price { get; set; }
                    public int update_time { get; set; }
                    public int pay_month { get; set; }
                    public string show_price { get; set; }
                    public int independent { get; set; }
                    public int permanent { get; set; }
                    public int can_buy_hide { get; set; }
                    public int need_address { get; set; }
                    public int product_type { get; set; }
                    public int sale_limit_count { get; set; }
                    public bool need_invite_code { get; set; }
                    public int expire_time { get; set; }
                    public object[] sku_processed { get; set; }
                    public int rankType { get; set; }
                }

                public class UserModel
                {
                    public string user_id { get; set; }

                    public string name { get; set; }

                    public string avatar { get; set; }
                }
            }

        }
    }
}
