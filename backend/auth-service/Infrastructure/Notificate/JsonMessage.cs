namespace auth_servise.Infrastructure.Notificate
{
    public class JsonMessage
    {
        public string EmailToSend { get; set; }
        public string UrlToComfirmEmail { get; set; }
        public string UrlToBlockEmail { get; set; }
    }
}
