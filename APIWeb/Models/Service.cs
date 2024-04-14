namespace APIWeb.Models
{
    public class Service
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<EntityService> EntityServices { get; set; }
    }
}
