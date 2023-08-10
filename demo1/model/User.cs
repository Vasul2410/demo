namespace demo1.model
{
    public class User
    {

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string EmailId { get; set; } = null!;

        public string? Password { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
