namespace APIWeb.Models
{
    public class Service
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Image { get; set; }
        public ICollection<EntityService> entityServices { get; set; }
    }
}
