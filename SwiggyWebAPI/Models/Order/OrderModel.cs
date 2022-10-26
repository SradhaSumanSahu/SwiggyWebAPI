using System.ComponentModel.DataAnnotations;

namespace SwiggyWebAPI.Models.Order
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
    }
}
