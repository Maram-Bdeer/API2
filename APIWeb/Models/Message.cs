namespace APIWeb.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public DateTime Time { get; set; }
    }
}