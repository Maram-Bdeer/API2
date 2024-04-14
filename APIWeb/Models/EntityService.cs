namespace APIWeb.Models
{
    public class EntityService
    {
        public string Id { get; set; }
        public string EntityID { get; set; }
        public string RoomID { get; set; }
        public string RoomName { get; set; }
        public DateTime Time { get; set; }
        public string ServiceID { get; set; }

        public virtual Service Service { get; set; }
    }
}
