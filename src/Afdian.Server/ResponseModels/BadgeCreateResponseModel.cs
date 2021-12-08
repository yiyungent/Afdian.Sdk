namespace Afdian.Server.ResponseModels
{
    public class BadgeCreateResponseModel
    {
        public int code { get; set; }

        public string message { get; set; }

        /// <summary>
        /// Badge ID
        /// </summary>
        public int badgeId { get; set; }
    }
}
