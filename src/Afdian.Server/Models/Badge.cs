using System.ComponentModel.DataAnnotations;

namespace Afdian.Server.Models
{
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public string Token { get; set; }

        public DateTime CreateTime { get; set; }

        public string Ip { get; set; }
    }
}
