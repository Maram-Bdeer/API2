using APIWeb.Models;

public class EntityService
{
    public string Id { get; set; }
    public string EntityID { get; set; }
    public string RoomID { get; set; }
    public string RoomName { get; set; }
    public DateTime Time { get; set; }
    public string ServiceId { get; set; }  // فقط معرف الخدمة بدلاً من كائن Service
}
