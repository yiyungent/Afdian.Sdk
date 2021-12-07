using System;
using System.Collections.Generic;
using System.Text;

namespace Afdian.Sdk.ResponseModels
{
    public class QueryOrderResponseModel
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
            public OrderModel[] list { get; set; }

            public int total_count { get; set; }

            public int total_page { get; set; }

            public class OrderModel
            {
                /// <summary>
                /// 订单号
                /// </summary>
                public string out_trade_no { get; set; }

                /// <summary>
                /// 下单用户ID
                /// </summary>
                public string user_id { get; set; }

                /// <summary>
                /// 方案ID，如自选，则为空
                /// </summary>
                public string plan_id { get; set; }

                /// <summary>
                /// 订单描述
                /// </summary>
                public string title { get; set; }

                /// <summary>
                /// 赞助月份
                /// </summary>
                public int month { get; set; }

                /// <summary>
                /// 真实付款金额，如有兑换码，则为0.00
                /// </summary>
                public string total_amount { get; set; }

                /// <summary>
                /// 显示金额，如有折扣则为折扣前金额
                /// </summary>
                public string show_amount { get; set; }

                /// <summary>
                /// 2 为交易成功。目前仅会推送此类型
                /// </summary>
                public int status { get; set; }

                /// <summary>
                /// 订单留言
                /// </summary>
                public string remark { get; set; }

                /// <summary>
                /// 兑换码ID
                /// </summary>
                public string redeem_id { get; set; }

                /// <summary>
                /// 0表示常规方案 1表示售卖方案
                /// </summary>
                public int product_type { get; set; }

                /// <summary>
                /// 折扣
                /// </summary>
                public string discount { get; set; }

                /// <summary>
                /// 如果为售卖类型，以数组形式表示具体型号
                /// </summary>
                public SkuDetailItemModel[] sku_detail { get; set; }

                /// <summary>
                /// 收件人
                /// </summary>
                public string address_person { get; set; }

                /// <summary>
                /// 收件人电话
                /// </summary>
                public string address_phone { get; set; }

                /// <summary>
                /// 收件人地址
                /// </summary>
                public string address_address { get; set; }

                public class SkuDetailItemModel
                {
                    public string sku_id { get; set; }

                    public long count { get; set; }

                    public string name { get; set; }

                    public string album_id { get; set; }

                    public string pic { get; set; }

                }


            }

        }

    }
}
