namespace SwiggyWebAPI.Models.Customer
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
