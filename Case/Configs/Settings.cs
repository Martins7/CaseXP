namespace Case.Configs
{
    public class ConnectionStrings
    {
        public string DBConnection { get; set; }
    }
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
    }
}
