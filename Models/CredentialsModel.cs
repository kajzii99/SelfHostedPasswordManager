namespace SelfHostedPasswordManager.Models
{
    public class CredentialsModel
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }
    }
}
