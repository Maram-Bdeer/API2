namespace APIWeb.Models
{
    public class Participation
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AdminName { get; set; }
        public string AdminID { get; set; }
        public string RoomName { get; set; }
        public int MemberNum { get; set; }
        public DateTime Time { get; set; }
        public int NumCard { get; set; }
        public decimal Amount { get; set; }
    }
}
