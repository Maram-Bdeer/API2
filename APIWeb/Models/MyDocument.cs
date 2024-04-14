namespace APIWeb.Models
{
    public class MyDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Signature { get; set; }
        public bool Status { get; set; }
    }
}
