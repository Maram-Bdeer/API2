namespace APIWeb.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string ReceiverPhone { get; set; }
        public string SenderID { get; set; }
        public string Email { get; set; }
        public List<Message> Content { get; set; }

       

    }
}
