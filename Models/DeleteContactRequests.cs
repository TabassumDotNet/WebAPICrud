namespace ContactAPI.Models
{
    public class DeleteContactRequests
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long PhonNumber { get; set; }


    }
}
