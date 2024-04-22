namespace APIWeb.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
        public string AdminID { get; set; }
        public string Time { get; set; }
        public int NumUsers { get; set; }
        public string Type { get; set; }
    }
}
