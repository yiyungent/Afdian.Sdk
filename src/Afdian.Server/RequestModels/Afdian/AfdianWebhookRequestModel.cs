namespace Afdian.Server.RequestModels.Afdian
{
    public class AfdianWebhookRequestModel
    {
        public int ec { get; set; }
        public string em { get; set; }
        public DataModel data { get; set; }
    }

    public class DataModel
    {
        public string type { get; set; }
        public OrderModel order { get; set; }
    }

    public class OrderModel
    {
        public string out_trade_no { get; set; }
        public string user_id { get; set; }
        public string plan_id { get; set; }
        public int month { get; set; }
        public string total_amount { get; set; }
        public string show_amount { get; set; }
        public int status { get; set; }
        public string remark { get; set; }
        public string redeem_id { get; set; }
        public int product_type { get; set; }
        public string discount { get; set; }
        public SkuDetailItemModel[] sku_detail { get; set; }
        public string address_person { get; set; }
        public string address_phone { get; set; }
        public string address_address { get; set; }
    }

    public class SkuDetailItemModel
    {
        public string sku_id { get; set; }

        public long count { get; set; }

        public string name { get; set; }

        public string album_id { get; set; }

        public string pic { get; set; }

    }


}


